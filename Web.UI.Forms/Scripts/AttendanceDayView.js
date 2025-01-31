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
var LinkRender = function (val, metaData, record, rowIndex, colIndex, store,apstatusString) {
  
    return '<a href="#" class="LinkRender"  style="cursor:pointer;"  >' + apstatusString +'</a>';
};

var rejectRender = function () {
    return '<img class="imgReject"  style="cursor:pointer;" src="Images/logoff.png" />';
};




var commandName;
var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

    CheckSession();



    var t = e.getTarget(),
        columnId = this.columns[columnIndex].id; // Get column id

    if (t.className == "imgEdit") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
    if (t.className == "imgReject") {
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
    if (t.className == "LinkRender") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }




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


var enterKeyPressSearchHandler = function (el, event) {

    var enter = false;
    if (event.getKey() == event.ENTER) {
        
         enter = true; 
    }

    if (enter === true) {
        App.Store1.reload();
    }
};
function validateFrom(s) {
   
   
        
    d = s.split(':');
    if (s.length != 5 )
        return false;
   
    if (/\s/.test(s))
        return false;
       
    if (s.split(':').length != 2) return false;
    
    if (d[0] == '')
        return false;
    if (d[1] == '')
        return false;

    if (isNaN(d[0]))
        return false;
    if (isNaN(d[1]))
        return false;

    if (d[0] > 23) return false;
    if (d[1] > 59) return false; return true;
    
}

function validateTo(curr, prev) {
    if (curr == null || curr == '')
        return true;
    if (!validateFrom(curr))
        return false;
    if (curr.length != 5)
        return false;
    if (/\s/.test(curr))
        return false;
    var currHours = curr.split(':')[0];
    var currMins = curr.split(':')[1];
    var prevHours = prev.split(':')[0];
    var prevMins = prev.split(':')[1];

    if (currHours < prevHours)
        return false;
    if (currHours == prevHours && currMins <= prevMins)
        return false;
    return true;
}