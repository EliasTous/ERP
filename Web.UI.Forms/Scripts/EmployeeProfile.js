﻿
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
};


function getTimeZone()
{
   
    var d = new Date();
    
    
    var n = d.getTimezoneOffset();
    
    document.getElementById("timeZoneOffset").setAttribute("text", n / 60);
    s = n / 60;
    App.direct.StoreTimeZone(s);
   

}
var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

    CheckSession();

    
 
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
   
    var columnId = grid.columns[cellIndex].id; // Get column id
    return columnId;
};


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

var setNameFields = function (firstName, lastName, middleName) {
    
    App.firstName.setValue(firstName);
    App.middleName.setValue(middleName);
    App.lastName.setValue(lastName);
};