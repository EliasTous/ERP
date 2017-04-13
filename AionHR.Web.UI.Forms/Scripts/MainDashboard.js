
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



    if (columnIndex == 1)
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

function GetItemTitle(id)
{
    return document.getElementById("item" + id + "Title").value;
}

