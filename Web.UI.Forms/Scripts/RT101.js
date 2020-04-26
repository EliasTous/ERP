function PrintElem(elem) {
    var elem = "<img id='imgout'></img>";
    $('#toPrint').width($(window).width());
    $('#toPrint').height($(window).height());
    $('#toPrint').css('height', $(window).height());
    html2canvas($("#toPrint"), {
        onrendered: function (canvas) {
            var img = canvas.toDataURL("image/png");

            var mywindow = window.open('', 'PRINT', 'height=400,width=600');

            var s = '<html><head><title>' + document.title + '</title>';
            s += '</head><body >';
            s += '<h1>' + document.title + '</h1>';
            s += '<img src="' + img + '"/>';
            s += '</body></html>';

        
            mywindow.document.write(s);
            window.open("mailto:issa@example.com?cc=jjjj@ff.com&subject=subject&body=" + s);
            //mywindow.document.close(); // necessary for IE >= 10
            //mywindow.focus(); // necessary for IE >= 10*/

            //mywindow.print();
            //mywindow.close();
        }
    });


    return true;
}
function PrintElem2(elem) {
    var mywindow = window.open('', 'new div', 'height=400,width=600');
    mywindow.document.write('<html><head><title>my div</title>');
    /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
    mywindow.document.write('</head><body >');
    alert($("#Center").html());
    mywindow.document.write($("#Center").html());
    mywindow.document.write('</body></html>');

    mywindow.print();
    mywindow.close();

    return true;


    return true;
}