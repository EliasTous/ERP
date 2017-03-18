
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="../Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="../Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="../Images/Tools/attach.png" />';
};



var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

    CheckSession();



    var t = e.getTarget(),
        columnId = this.columns[columnIndex].id; // Get column id

    if (t.className == "imgEdit" && columnId == "colEdit") {
        //the ajax call is allowed

        return true;
    }

    if (t.className == "imgDelete" && (columnId == "ColSADelete"|| columnId=="ColBODelete")) {
        //the ajax call is allowed
        return true;
    }
    if (t.className == "imgAttach" && columnId == "colAttach") {
        //the ajax call is allowed
        return true;
    }
    if ( columnId=="ColBOName"|| columnId=="ColSAName")
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

function getPaymentTypeString(index)
{
    
    switch(index)
    {
        case"0":
        case 0: return document.getElementById("PaymentTypeDaily").value; break;
            case"1":
        case 1: return document.getElementById("PaymentTypeWeekly").value; break;
            case"2":
        case 2: return document.getElementById("PaymentTypeMonthly").value; break;
        default: return index;
    }
}
function getPaymentMethodString(index)
{
    switch (index) {
        case"0":
        case 0: return document.getElementById("PaymentMethodCash").value; break;
            case"1":
        case 1: return document.getElementById("PaymentMethodBank").value; break;
       
        default: return index;
    }
}
function TogglePaymentMethod(index)
{
    App.accountNumber.setDisabled(index == 0);
    App.bankName.setDisabled(index == 0);

}

function addEntitlement() {
    var entitlementsGrid = App.entitlementsGrid,
        store = entitlementsGrid.getStore();

    entitlementsGrid.editingPlugin.cancelEdit();

    store.getSorters().removeAll();
    entitlementsGrid.getView().headerCt.setSortState(); // To update columns sort UI

    store.insert(0, {
        from: '0',
        to: '1',
        days: 2

    });

    entitlementsGrid.editingPlugin.startEdit(0, 0);
}
function addDeduction() {
    var deductionGrid = App.deductionGrid,
        store = deductionGrid.getStore();

    deductionGrid.editingPlugin.cancelEdit();

    store.getSorters().removeAll();
    deductionGrid.getView().headerCt.setSortState(); // To update columns sort UI

    store.insert(0, {
        from: '0',
        to: '1',
        days: 2

    });

    deductionGrid.editingPlugin.startEdit(0, 0);
}
function dump(obj) {
    var out = '';
    for (var i in obj) {
        out += i + ": " + obj[i] + "\n";


    }
    return out;
}
function removeEntitlement() {
   
    var entitlementsGrid = App.entitlementsGrid,
        sm = entitlementsGrid.getSelectionModel(),
        store = entitlementsGrid.getStore();
    
   
    
    entitlementsGrid.editingPlugin.cancelEdit();
    store.remove(sm.getSelection());

    if (store.getCount() > 0) {
        sm.select(0);
    }
}

function removeDeduction() {

    var deductionGrid = App.deductionGrid,
        sm = deductionGrid.getSelectionModel(),
        store = deductionGrid.getStore();



    deductionGrid.editingPlugin.cancelEdit();
    store.remove(sm.getSelection());

    if (store.getCount() > 0) {
        sm.select(0);
    }
}

var nameRenderer = function (value) {
   
    var r = App.edStore.getById(value);

    if (Ext.isEmpty(r)) {
        return "";
    }

    return r.data.name;
};

