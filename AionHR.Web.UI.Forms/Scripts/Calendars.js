
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
    if (columnId == "ColName" || columnId == "colDetails" || columnId == "colDayName" || columnId == "colYearDetails") {

        return true;
    }



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

var colorify = function (tdID, color) {
    $("#" + tdID).attr("style", "background:" + color);
};
var init = function () {
    $('#tbCalendar td').each(function () {
        $(this).css('backgroundColor', '#ffffff');
    });
};
var setLeapDay = function () {
    
    
   
    $("#td0229").addClass("notexist");
        $("#td0229").html("X");
        
    

}
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
$(document).ready(function () {
    $("#tbCalendar td").click(function () {

        if (!$(this).hasClass('notexist')) {
            currentDayId = $(this).find('.hidden:first').html();

            App.direct.OpenDayConfig(currentDayId);
        }
    });
});
