

   

      var editRender = function () {
        return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
    };

    var deleteRender = function () {
        return '<img class="imgDelete" style="cursor:pointer;" src="Images/Tools/delete.png" />';
    };
    var attachRender = function () {
        return '<img class="imgAttach" style="cursor:pointer;" src="Images/Tools/attach.png" />';
    };

    var options;
    var commandName;

   

var triggierImageClick = function (id) {
    
            $("#" + id).click();
        }
    
     
        var imageData;
    
    var showImagePreview = function (id) {

        var input = $("#" + id)[0];
        if (input.files && input.files[0]) {

            //Check the extension and if not ok clear and notify the user

            if (checkExtension(input.files[0].name)) {

                var filerdr = new FileReader();
                filerdr.onload = function (e) {
            $("#" + $('#imgControl')[0].firstChild.id).attr('src', e.target.result);

        }
        filerdr.readAsDataURL(input.files[0]);
    }
            else {
            alert('File Format is not allowed');
        $("#" + $('#imgControl')).attr('src', '');

        App.picturePath.reset();
        //Alert the user and clear the input file
    }
}
        else {
            $("#" + $('#imgControl')[0].firstChild.id).attr('src', '');
        App.picturePath.reset();
        App.picturePath.Clear();
    }
}
    var showImagePreview2 = function (id) {

        var input = $("#" + id)[0];
        if (input.files && input.files[0]) {

            //Check the extension and if not ok clear and notify the user

            if (checkExtension(input.files[0].name)) {

                var filerdr = new FileReader();
                filerdr.onload = function (e) {
            //$("#" + $('#employeePhoto')[0].firstChild.id).attr('src', e.target.result);
            //$('#image').attr('src', e.target.result);
            InitCropper(e.target.result);
        //$('#image').attr('src', e.target.result);
        //options.imgSrc = e.target.result;


        }
        filerdr.readAsDataURL(input.files[0]);
        App.uploadPhotoButton.setDisabled(false);
    }
            else {
            alert('File Format is not allowed');
        //$("#" + $('#employeePhoto')[0].firstChild.id).attr('src', '');
        App.FileUploadField1.reset();

        //Alert the user and clear the input file
    }
}
        else {
            //$("#" + $('#employeePhoto')[0].firstChild.id).attr('src', '');
            App.FileUploadField1.reset();
        App.FileUploadField1.Clear();
    }
}
    var ClearImage = function () {
            App.picturePath.reset();
        $("#" + $('#imgControl')[0].firstChild.id).attr('src', '');

    }
    var ClearImage2 = function () {
            App.FileUploadField1.reset();
        // $("#" + $('#employeePhoto')[0].firstChild.id).attr('src', 'images/empPhoto.jpg');
        // $('#image').attr('src', 'images/empPhoto.jpg');
        //InitCropper('images/empPhoto.jpg');
        ClearCropper();
        //App.uploadPhotoButton.setDisabled(true);
    }


    var checkExtension = function (file) {

        try {

            if (file === null || file === '') {
                return true;
    }
    var dot = file.lastIndexOf('.');
            if (dot >= 0) {
                var ext = file.substr(dot + 1, file.length).toLowerCase();
                if (ext in {'jpg': '', 'png': '', 'jpeg': '' }) { return true; }
    }

    return false;
}
        catch (e) {
            return false;
    }
}

    var onEmployeeTreeItemClick = function (record, e) {

        if (record.data) {
            if (record.data.click !== "1") {
            // record[record.isExpanded() ? 'collapse' : 'expand']();
            e.stopEvent();

        } else {

            openNewTabEmployee(record.data.idTab, record.data.url, record.data.title, record.data.css);
        }
    }


};
    var openNewTabEmployee = function (id, url, title, iconCls) {


        var tab = App.employeesTabPanel.getComponent(id);
        // if (id != 'dashboard') {
        //alert(interval);
        //  clearInterval(interval);
        //alert('cleared');
        // }



        if (!tab) {


            tab = App.employeesTabPanel.add({
                id: id,
                title: title,
                iconCls: iconCls,
                closable: false,
                loader: {
                    url: url + "?employeeId=" + 1,
                    renderer: "frame",
                    loadMask: {
                        showMask: true,
                        msg: App.lblLoading.getValue()
                    }
                }
            });

        }
        else {
            App.employeesTabPanel.closeTab(tab);
        tab = App.employeesTabPanel.add({
            id: id,
        title: title,
        iconCls: iconCls,
        closable: false,
                loader: {
            url: url,
        renderer: "frame",
                    loadMask: {
            showMask: true,
        msg: App.lblLoading.getValue()
    }
}
});
}
App.employeesTabPanel.setActiveTab(tab);
}
    function dump(obj) {
        var out = '';
        for (var i in obj) {
            out += i + ": " + obj[i] + "\n";


        }
        return out;
    }
   

   
function initCropper(path) {
    alert(path);
            //options =
            //    {
            //        imageBox: '.imageBox',
            //        thumbBox: '.thumbBox',
            //        spinner: '.spinner',
            //        imgSrc: path
            //    };
            //cropper = new cropbox(options);
            // alert(path);
            $('#image').attr('src', path);
        }
    
   



  
        var cropper = null;
    function getRoundedCanvas(sourceCanvas) {
        var canvas = document.createElement('canvas');
        var context = canvas.getContext('2d');
        var width = sourceCanvas.width;
        var height = sourceCanvas.height;

        canvas.width = width;
        canvas.height = height;
        context.beginPath();
        context.arc(width / 2, height / 2, Math.min(width, height) / 2, 0, 2 * Math.PI);
        context.strokeStyle = 'rgba(0,0,0,0)';
        context.stroke();
        context.clip();
        context.drawImage(sourceCanvas, 0, 0, width, height);

        return canvas;
    }
    function ClearCropper() {
            $('#image').cropper('destroy');
        }
    function InitCropper(path) {

        var $image = $('#image');
        var $button = $('#button');
        var $result = $('#result');
        var croppable = false;

        $('#image').attr('src', path);
        $('#image').cropper('destroy');
        $image.cropper({
            aspectRatio: 'd',
        viewMode: 1,
            ready: function () {
            croppable = true;
        }

    });
        $button.on('click', function () {
            var croppedCanvas;
        var roundedCanvas;

            if (!croppable) {
                return ;
    }

    // Crop
    croppedCanvas = $image.cropper('getCroppedCanvas');

    // Round
    roundedCanvas = getRoundedCanvas(croppedCanvas);

    // Show  image.crossOrigin = "Anonymous";
            //$result.html('<img image.crossOrigin = "Anonymous" src="' + roundedCanvas.toDataURL() + '">');
    });

}
var theBlob;
    


function GetCroppedImage() {
    var croppedCanvas;
    var roundedCanvas;



    // Crop
    croppedCanvas = $('#image').cropper('getCroppedCanvas');

    // Round
    roundedCanvas = getRoundedCanvas(croppedCanvas);


    var dataURL = roundedCanvas.toDataURL("image/png");
    var b;
    roundedCanvas.toBlob(function (blob) {

        App.imageData.value = blob; var fd = new FormData();
        fd.append('fname', 'companyLogo');
        fd.append('id', null);

        Ext.net.Mask.show({ msg: App.lblLoading.getValue(), el: App.imageSelectionWindow.id });
        var fileName = 'companyLogo';
       

        fd.append('data', App.imageData.value, fileName);
        if (App.FileUploadField1.value == '')
            fd.append('oldUrl', App.CurrentEmployeePhotoName.value);
        $.ajax({
            type: 'POST',
            url: 'EmployeePhotoUploaderHandler.ashx?classId=20030' ,
            data: fd,
            processData: false,
            contentType: false,
            error: function (s) { Ext.net.Mask.hide(); alert(dump(s)); }
        }).done(function (data) {
            App.direct.FillCompanyLogo();
            Ext.net.Mask.hide();
            App.imageSelectionWindow.hide();

        });
    }

    );

    return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
}
   
   

   