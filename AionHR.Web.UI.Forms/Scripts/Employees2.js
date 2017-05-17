
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
};

var options;
var commandName;
var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

    commandName = "";
    CheckSession();


    // in case 

    if (columnIndex == 0)
        return false;
    var t = e.getTarget();
    // columnId = App.employeeControl1_GridPanel1.columns[columnIndex].id; // Get column id

    if (t.className == "imgEdit") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }

    if (t.className == "imgDelete") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
    if (t.className == "imgAttach") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }

    App.employeeControl1_RowExpander1.toggleRow(rowIndex, record);

    //forbidden
    return false;
};


var getCellType = function (grid, rowIndex, cellIndex) {

    if (cellIndex == 0)
        return "";
    if (commandName != "")
        return commandName;
    var columnId = grid.columns[cellIndex].id; // Get column id

    return columnId;
};

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

            App.employeeControl1_picturePath.reset();
            //Alert the user and clear the input file
        }
    }
    else {
        $("#" + $('#imgControl')[0].firstChild.id).attr('src', '');
        App.employeeControl1_picturePath.reset();
        App.employeeControl1_picturePath.Clear();
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
            App.employeeControl1_employeeControl1_uploadPhotoButton.setDisabled(false);
        }
        else {
            alert('File Format is not allowed');
            //$("#" + $('#employeePhoto')[0].firstChild.id).attr('src', '');
            App.employeeControl1_FileUploadField1.reset();
           
            //Alert the user and clear the input file
        }
    }
    else {
        //$("#" + $('#employeePhoto')[0].firstChild.id).attr('src', '');
        App.employeeControl1_FileUploadField1.reset();
        App.employeeControl1_FileUploadField1.Clear();
    }
}
var ClearImage = function () {
    App.employeeControl1_picturePath.reset();
    $("#" + $('#imgControl')[0].firstChild.id).attr('src', '');

}
var ClearImage2 = function () {
    App.employeeControl1_FileUploadField1.reset();
   // $("#" + $('#employeePhoto')[0].firstChild.id).attr('src', 'images/empPhoto.jpg');
   // $('#image').attr('src', 'images/empPhoto.jpg');
    //InitCropper('images/empPhoto.jpg');
    ClearCropper();
    //App.employeeControl1_uploadPhotoButton.setDisabled(true);
}


var checkExtension = function (file) {

    try {

        if (file == null || file == '') {
            return true;
        }
        var dot = file.lastIndexOf('.');
        if (dot >= 0) {
            var ext = file.substr(dot + 1, file.length).toLowerCase();
            if (ext in { 'jpg': '', 'png': '', 'jpeg': '' }) { return true; }
        }

        return false;
    }
    catch (e) {
        return false;
    }
}

var onEmployeeTreeItemClick = function (record, e) {

    if (record.data) {
        if (record.data.click != "1") {
            // record[record.isExpanded() ? 'collapse' : 'expand']();
            e.stopEvent();

        } else {
            
            openNewTabEmployee(record.data.idTab, record.data.url, record.data.title, record.data.css);
        }
    }


};
var openNewTabEmployee = function (id, url, title, iconCls) {


    var tab = App.employeeControl1_employeesTabPanel.getComponent(id);
    // if (id != 'dashboard') {
    //alert(interval);
    //  clearInterval(interval);
    //alert('cleared');
    // }



    if (!tab) {


        tab = App.employeeControl1_employeesTabPanel.add({
            id: id,
            title: title,
            iconCls: iconCls,
            closable: false,
            loader: {
                url: url + "?employeeId=" + document.getElementById("CurrentEmployee").value,
                renderer: "frame",
                loadMask: {
                    showMask: true,
                    msg: App.employeeControl1_lblLoading.getValue()
                }
            }
        });

    }
    else {
        App.employeeControl1_employeesTabPanel.closeTab(tab);
        tab = App.employeeControl1_employeesTabPanel.add({
            id: id,
            title: title,
            iconCls: iconCls,
            closable: false,
            loader: {
                url: url,
                renderer: "frame",
                loadMask: {
                    showMask: true,
                    msg: App.employeeControl1_lblLoading.getValue()
                }
            }
        });
    }
    App.employeeControl1_employeesTabPanel.setActiveTab(tab);
}
function dump(obj) {
    var out = '';
    for (var i in obj) {
        out += i + ": " + obj[i] + "\n";


    }
    return out;
}
function FillLeftPanel(departmentName, branchName, positionName, reportToName, balance, lastLeave, paid, leaveBalance, allowedLeaves, esName, serviceDuration) {




    App.employeeControl1_reportsToLbl.setText(reportToName);
    App.employeeControl1_eosBalanceTitle.setText(balance);
    App.employeeControl1_lastLeaveStartDateTitle.setText(lastLeave);
    App.employeeControl1_paidLeavesYTDTitle.setText(paid);
    App.employeeControl1_leavesBalanceTitle.setText(leaveBalance);
    App.employeeControl1_allowedLeaveYtdTitle.setText(allowedLeaves);
    App.employeeControl1_esName = esName;
    App.employeeControl1_serviceDuration = serviceDuration;
    FillLeftPanel(dep, branchName, positionName);

}
function FillLeftPanel(departmentName, branchName, positionName, reportToName) {




    App.employeeControl1_reportsToLbl.setText(reportToName);
    FillLeftPanel(dep, branchName, positionName);

}

function FillFullName(fullName) {
    App.employeeControl1_fullNameLbl.Html = fullName;
}
function FillLeftPanel(departmentName, branchName, positionName) {



    App.employeeControl1_departmentLbl.setText(departmentName, false);
    App.employeeControl1_branchLbl.setText(branchName, false);
    App.employeeControl1_positionLbl.setText(positionName, false);


}
function SelectJICombos(deptId, bId, pId, dId) {
    App.employeeControl1_departmentId.select(deptId);
    App.employeeControl1_branchId.select(bId);
    App.employeeControl1_positionId.select(pId);
    App.employeeControl1_divisionId.select(dId);
}


var enterKeyPressSearchHandler = function (el, event) {

    var enter = false;
    if (event.getKey() == event.ENTER) {
        if (el.getValue().length > 0)
        { enter = true; }
    }

    if (enter === true) {
        App.employeeControl1_Store1.reload();
    }
};
function initCropper(path) {
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

function refreshQV() {
    App.employeeControl1_direct.FillLeftPanel(true);
}