
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
};



var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

    CheckSession();


    // in case 
    if (columnIndex == 0)
        return false;
    var t = e.getTarget(),
        columnId = this.columns[columnIndex].id; // Get column id

    if (t.className == "imgEdit" && columnId == "colEdit") {
        //the ajax call is allowed

        return true;
    }

    if (t.className == "imgDelete" && columnId == "colDelete") {
        //the ajax call is allowed
        return true;
    }
    if (t.className == "imgAttach" && columnId == "colAttach") {
        //the ajax call is allowed
        return true;
    }
    if (columnId == "ColName")
        return true;


    //forbidden
    return false;
};


var getCellType = function (grid, rowIndex, cellIndex) {
    if (cellIndex == 0)
        return "";
    var columnId = grid.columns[cellIndex].id; // Get column id
    return columnId;
};

var triggierImageClick = function (id) {
    $("#" + id).click();
}




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
                url: url+"?employeeId="+document.getElementById("CurrentEmployee").value,
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

