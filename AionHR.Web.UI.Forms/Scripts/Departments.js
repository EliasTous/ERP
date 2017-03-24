
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="../Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="../Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="../Images/Tools/attach.png" />';
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

function getPaymentTypeString(index) {

    switch (index) {
        case "0":
        case 0: return document.getElementById("PaymentTypeDaily").value; break;
        case "1":
        case 1: return document.getElementById("PaymentTypeWeekly").value; break;
        case "2":
        case 2: return document.getElementById("PaymentTypeMonthly").value; break;
        default: return index;
    }
}
function getPaymentMethodString(index) {
    switch (index) {
        case "0":
        case 0: return document.getElementById("PaymentMethodCash").value; break;
        case "1":
        case 1: return document.getElementById("PaymentMethodBank").value; break;

        default: return index;
    }
}
function TogglePaymentMethod(index) {
    App.accountNumber.setDisabled(index == 0);
    App.bankName.setDisabled(index == 0);

}