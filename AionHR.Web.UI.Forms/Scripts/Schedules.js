
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/application_edit.png" />';
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

    var breaksGrid = App.breaksGrid,
        store = breaksGrid.getStore();

    breaksGrid.editingPlugin.cancelEdit();

    store.getSorters().removeAll();
    breaksGrid.getView().headerCt.setSortState(); // To update columns sort UI

    store.insert(0, {
        name: 'new break',
        start: '9:00',
        end: '10:00',
        isBenifit: false

    });

    breaksGrid.editingPlugin.startEdit(0, 0);
}
function addBreak() {
    if (App.editDisabled.value == '1')
        return;
    var periodsGrid = App.periodsGrid,
        store = periodsGrid.getStore();

    periodsGrid.editingPlugin.cancelEdit();

    store.getSorters().removeAll();
    periodsGrid.getView().headerCt.setSortState(); // To update columns sort UI

    store.insert(0, {
        name: 'new break',
        start: null,
        end: null,
        isBenifit: false

    });

    periodsGrid.editingPlugin.startEdit(0, 0);
}

function removeBreak() {

    if (App.editDisabled.value == '1')
        return;
    var periodsGrid = App.periodsGrid,
        sm = periodsGrid.getSelectionModel(),
        store = periodsGrid.getStore();

    periodsGrid.editingPlugin.cancelEdit();
    store.remove(sm.getSelection());

    if (store.getCount() > 0) {
        sm.select(0);
    }
}
function removeEmployee() {

    var breaksGrid = App.breaksGrid,
        sm = breaksGrid.getSelectionModel(),
        store = breaksGrid.getStore();

    breaksGrid.editingPlugin.cancelEdit();
    store.remove(sm.getSelection());

    if (store.getCount() > 0) {
        sm.select(0);
    }
}


var commandName;
var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e)
{

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
    commandName = "";

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
function getDay(dow) {

    switch (dow) {
        case 1: return document.getElementById('MondayText').value;
        case 2: return document.getElementById('TuesdayText').value;
        case 3: return document.getElementById('WednesdayText').value;
        case 4: return document.getElementById('ThursdayText').value;
        case 5: return document.getElementById('FridayText').value;
        case 6: return document.getElementById('SaturdayText').value;
        case 7: return document.getElementById('SundayText').value;
    }
}
function validateFrom(s) {
    if (s.length != 5)
        return false;
    d = s.split(':'); if (d[0] > 23) return false; if (d[1] > 59) return false; return true ;
}

function validateTo(curr,prev)

{
    if (curr.length != 5)
        return false;
    if (!validateFrom(curr))
        return false;
    var currHours = curr.split(':')[0];
    var currMins = curr.split(':')[1];
    var prevHours = prev.split(':')[0];
    var prevMins = prev.split(':')[1];

    //if (currHours < prevHours)
    //    return false;
    //if (currHours == prevHours && currMins <= prevMins)
    //if (currHours == prevHours)
    //    return false;
    return true;
}