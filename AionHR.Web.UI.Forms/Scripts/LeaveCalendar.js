
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
function GetStatusName(index)
{
    switch(index){
        case "1" :case 1:
            return document.getElementById("StatusPending").value;
            break;
        case "2" :case 2:
            return document.getElementById("StatusApproved").value;
            break;
        case"3" :case 3:
            return document.getElementById("StatusRefused").value;
            break;
        case "4": case 4:
            return document.getElementById("StatusUsed").value;
            break;
         default:break;
    }
}

function getDay(dow) {

    switch (dow) {
        case 1: return document.getElementById('MondayText').value;
        case 2: return document.getElementById('TuesdayText').value;
        case 3: return document.getElementById('WednesdayText').value;
        case 4: return document.getElementById('ThursdayText').value;
        case 5: return document.getElementById('FridayText').value;
        case 6: return document.getElementById('SaturdayText').value;
        case 0: return document.getElementById('SundayText').value;
    }
}