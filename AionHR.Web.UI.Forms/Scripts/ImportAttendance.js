
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
};
var errorRender = function () {
    return '<img class="imgAttach" src="Images/Tools/error.png" />';
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
    if (columnId == "ColName")
        return true;


    //forbidden
    return false;
};


var getCellType = function (grid, rowIndex, cellIndex) {

    //var columnId = grid.columns[cellIndex].id; // Get column id
    //return columnId;

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
        if (el.getValue().length > 0)
        { enter = true; }
    }

    if (enter === true) {
        App.Store1.reload();
    }
};
function validateFrom(s) {

    d = s.split(':'); if (d[0] > 23) return false; if (d[1] > 59) return false; return true;
}

function validateTo(curr, prev) {
    if (curr == null || curr == '')
        return true;
    if (!validateFrom(curr))
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
var validateFile = function (id) {

    var input = $("#" + id)[0];
    if (input.files && input.files[0]) {

        //Check the extension and if not ok clear and notify the user

        if (!checkExtension(input.files[0].name)) {
            alert('Please Select a csv file');
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
            if (ext in { 'csv': ''}) { return true; }
        }

        return false;
    }
    catch (e) {
        return false;
    }
}
