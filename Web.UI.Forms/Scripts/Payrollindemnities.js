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
//Ext.apply(Ext.form.VTypes, {
//    numberrange: function (val, field) {
//        if (!val) {
//            return;
//        }

//        if (field.startNumberField && (!field.numberRangeMax || (val != field.numberRangeMax))) {
//            var start = Ext.getCmp(field.startNumberField);

//            if (start) {
//                start.setMaxValue(val);
//                field.numberRangeMax = val;
//                start.validate();
//            }
//        } else if (field.endNumberField && (!field.numberRangeMin || (val != field.numberRangeMin))) {
//            var end = Ext.getCmp(field.endNumberField);

//            if (end) {
//                end.setMinValue(val);
//                field.numberRangeMin = val;
//                end.validate();
//            }
//        }

//        return true;
//    }
//});
function addEmployee() {
    var periodsGrid = App.periodsGrid,store = periodsGrid.getStore();

    periodsGrid.editingPlugin.cancelEdit();

    store.getSorters().removeAll();
    periodsGrid.getView().headerCt.setSortState(); // To update columns sort UI

    store.insert(0, {

        from: '0',
        to: '0',
        pct: '0'

    });

    periodsGrid.editingPlugin.startEdit(0, 0);
}

function removeEmployee() {
    var periodsGrid = App.periodsGrid,
        sm = periodsGrid.getSelectionModel(),
        store = periodsGrid.getStore();

    periodsGrid.editingPlugin.cancelEdit();
    store.remove(sm.getSelection());

    if (store.getCount() > 0) {
        sm.select(0);
    }
}
function addResignation() {
   
    var IndemnityRecognitionGrid = App.IndemnityRecognitionGrid,
        store = IndemnityRecognitionGrid.getStore();

    IndemnityRecognitionGrid.editingPlugin.cancelEdit();

    store.getSorters().removeAll();
    IndemnityRecognitionGrid.getView().headerCt.setSortState(); // To update columns sort UI

    store.insert(0, {

        from: '0',
        to: '0',
        pct: '0'

    });

    IndemnityRecognitionGrid.editingPlugin.startEdit(0, 0);
}

function removeResignation() {
    
    var IndemnityRecognitionGrid = App.IndemnityRecognitionGrid,
        sm = IndemnityRecognitionGrid.getSelectionModel(),
        store = IndemnityRecognitionGrid.getStore();

    IndemnityRecognitionGrid.editingPlugin.cancelEdit();
    store.remove(sm.getSelection());

    if (store.getCount() > 0) {
        sm.select(0);
    }
}
function getTimeZone() {

    var d = new Date();


    var n = d.getTimezoneOffset();

    document.getElementById("timeZoneOffset").setAttribute("text", n / 60);
    s = n / 60;
    App.direct.StoreTimeZone(s);


}
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