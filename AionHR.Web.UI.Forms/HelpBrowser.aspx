<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpBrowser.aspx.cs" Inherits="AionHR.Web.UI.Forms.HelpBrowser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript"  src="https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.0.203/pdf.js"></script>
    <script type="text/javascript"  src="https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.0.203/pdf_viewer.js"></script>
    <script type="text/javascript">
        var url;
        function InitFile(file) {
            file = 'http://bridge.aionhr.net/Site/pdfs/vaio.pdf';
             url = file;

            // The workerSrc property shall be specified.
            PDFJS.workerSrc = '//mozilla.github.io/pdf.js/build/pdf.worker.js';

            var pdfDoc = null,
                pageNum = 1,
                pageRendering = false,
                pageNumPending = null,
                scale = 0.8,
                canvas = document.getElementById('the-canvas'),
                ctx = canvas.getContext('2d');
        }
        /**
         * Get page info from document, resize canvas accordingly, and render page.
         * @param num Page number.
         */
        function renderPage(num) {
            pageRendering = true;
            // Using promise to fetch the page
            pdfDoc.getPage(num).then(function (page) {
                var viewport = page.getViewport(scale);
                canvas.height = viewport.height;
                canvas.width = viewport.width;

                // Render PDF page into canvas context
                var renderContext = {
                    canvasContext: ctx,
                    viewport: viewport
                };
                var renderTask = page.render(renderContext);

                // Wait for rendering to finish
                renderTask.promise.then(function () {
                    pageRendering = false;
                    if (pageNumPending !== null) {
                        // New page rendering is pending
                        renderPage(pageNumPending);
                        pageNumPending = null;
                    }
                });
            });

            // Update page counters
            document.getElementById('page_num').textContent = pageNum;
        }

        /**
         * If another page rendering in progress, waits until the rendering is
         * finised. Otherwise, executes rendering immediately.
         */
        function queueRenderPage(num) {
            if (pageRendering) {
                pageNumPending = num;
            } else {
                renderPage(num);
            }
        }

        /**
         * Displays previous page.
         */
        function onPrevPage() {
            if (pageNum <= 1) {
                return;
            }
            pageNum--;
            queueRenderPage(pageNum);
        }
       // alert(document.getElementById('prev'));
       // document.getElementById('prev').addEventListener('click', onPrevPage);
//
        /**
         * Displays next page.
         */
        function onNextPage() {
            if (pageNum >= pdfDoc.numPages) {
                return;
            }
            pageNum++;
            queueRenderPage(pageNum);
        }
       // document.getElementById('next').addEventListener('click', onNextPage);

        /**
         * Asynchronously downloads PDF.
         */
        PDFJS.getDocument(url).then(function (pdfDoc_) {
            pdfDoc = pdfDoc_;
            document.getElementById('page_count').textContent = pdfDoc.numPages;

            // Initial/first page rendering
            renderPage(pageNum);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />
    <div>
    <div>
  <button id="prev">Previous</button>
  <button id="next">Next</button>
  &nbsp; &nbsp;
  <span>Page: <span id="page_num"></span> / <span id="page_count"></span></span>
</div>

<canvas id="the-canvas"></canvas>
    </div>
    </form>
</body>
</html>
