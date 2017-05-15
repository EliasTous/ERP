
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
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
        if (el.getValue().length > 0)
        { enter = true; }
    }

    if (enter === true) {
        App.Store1.reload();
    }
};

function GetStatusName(index) {
    switch (index) {
        case "1": case 1:
            return document.getElementById("StatusNew").value;
            break;
        case "2": case 2:
            return document.getElementById("StatusInProcess").value;
            break;
        case "3": case 3:
            return document.getElementById("StatusApproved").value;
            break;
        case "-1": case -1:
            return document.getElementById("StatusRejected").value;
            break;

        default: break;
    }
}

var validateSave = function () {
    var plugin = this.editingPlugin;

    if (this.getForm().isValid()) { // local validation
        App.direct.ValidateSave(plugin.context.record.phantom, Ext.encode(App.caseCommentGrid.getRowsValues({ selectedOnly: true })), this.getValues(false, false, false, true), {
            success: function (result) {
                if (!result.valid) {
                    Ext.Msg.alert("Error", result.msg);
                    return;
                }

                plugin.completeEdit();
            }
        });
    }
};

function ClearNoteText() {
    App.newNoteText.setValue("");
}