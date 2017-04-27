
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
       // columnId = App.GridPanel1.columns[columnIndex].id; // Get column id
    
    if (t.className == "imgEdit" ) {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }

    if (t.className == "imgDelete" ) {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
    if (t.className == "imgAttach") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
  
    App.RowExpander1.toggleRow(rowIndex,record);
  
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
            $("#" + $('#imgControl')[0].firstChild.id).attr('src', '');
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
                $("#" + $('#employeePhoto')[0].firstChild.id).attr('src', e.target.result);
                
                options.imgSrc = e.target.result;
                cropper = new cropbox(options);

            }
            filerdr.readAsDataURL(input.files[0]);
            App.uploadPhotoButton.setDisabled(false);
        }
        else {
            alert('File Format is not allowed');
            $("#" + $('#employeePhoto')[0].firstChild.id).attr('src', '');
            App.FileUploadField1.reset();
            
            //Alert the user and clear the input file
        }
    }
    else {
        $("#" + $('#employeePhoto')[0].firstChild.id).attr('src', '');
        App.FileUploadField1.reset();
        App.FileUploadField1.Clear();
    }
}
var ClearImage = function()
{
    App.picturePath.reset();  
    $("#" + $('#imgControl')[0].firstChild.id).attr('src', '');
 
}
var ClearImage2 = function () {
    App.FileUploadField1.reset();
    $("#" + $('#employeePhoto')[0].firstChild.id).attr('src', 'images/empPhoto.jpg');
    
    initCropper('Images/empPhoto.jpg');
    //App.uploadPhotoButton.setDisabled(true);
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
            alert(record.data.url);
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
                url: url + "?employeeId=" + document.getElementById("CurrentEmployee").value,
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
function FillLeftPanel(departmentName, branchName, positionName, reportToName,balance,lastLeave,paid,leaveBalance,allowedLeaves,esName,serviceDuration) {




    App.reportsToLbl.setText(reportToName);
    App.eosBalanceTitle.setText(balance);
    App.lastLeaveStartDateTitle.setText(lastLeave);
    App.paidLeavesYTDTitle.setText(paid);
    App.leavesBalanceTitle.setText(leaveBalance);
    App.allowedLeaveYtdTitle.setText(allowedLeaves);
    App.esName = esName;
    App.serviceDuration = serviceDuration;
    FillLeftPanel( dep, branchName, positionName);

}
function FillLeftPanel( departmentName, branchName, positionName,reportToName) {


    

    App.reportsToLbl.setText(reportToName);
    FillLeftPanel(dep, branchName, positionName);

}

function FillFullName(fullName)
{
    App.fullNameLbl.Html = fullName;
}
function FillLeftPanel(departmentName, branchName, positionName) {


 
    App.departmentLbl.setText(departmentName, false);
    App.branchLbl.setText(branchName, false);
    App.positionLbl.setText(positionName, false);
   

}
function SelectJICombos(deptId,bId,pId,dId)
{
    App.departmentId.select(deptId);
    App.branchId.select(bId);
    App.positionId.select(pId);
    App.divisionId.select(dId);
}


var enterKeyPressSearchHandler = function (el, event) {

    var enter = false;
    if (event.getKey() == event.ENTER) {
        if (el.getValue().length > 0)
        { enter = true; }
    }

    if (enter === true) {
        App.Store1.reload();
    }
};
function initCropper(path)
{
    options =
        {
            imageBox: '.imageBox',
            thumbBox: '.thumbBox',
            spinner: '.spinner',
            imgSrc:path
        };
    cropper = new cropbox(options);
}

function refreshQV()
{
    App.direct.FillLeftPanel(true);
}