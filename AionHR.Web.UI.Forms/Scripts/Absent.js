
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
};
var LinkRender = function (val, metaData, record, rowIndex, colIndex, store, apstatusString) {

    return '<a href="#" class="LinkRender"  style="cursor:pointer;"  >' + apstatusString + '</a>';
};


var rejectRender = function () {
    return '<img class="imgReject"  style="cursor:pointer;" src="Images/logoff.png" />';
};


var historeRender = function () {
    return '<img class="imgHistory"  style="cursor:pointer;"  src="Images/Tools/error.png" />';
};



var commandName;
var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

    CheckSession();



    var t = e.getTarget(),
        columnId = this.columns[columnIndex].id; // Get column id

    if (t.className === "imgEdit") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
    if (t.className === "imgReject") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }

    if (t.className === "imgDelete") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
    if (t.className === "imgAttach") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
    if (t.className === "LinkRender") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }
    if (t.className === "imgHistory") {
        //the ajax call is allowed
        commandName = t.className;
        return true;
    }





    //forbidden
    return false;
};


var getCellType = function (grid, rowIndex, cellIndex) {

    if (cellIndex === 0)
        return "";
    if (commandName !== "")
        return commandName;
    var columnId = grid.columns[cellIndex].id; // Get column id

    return columnId;
}