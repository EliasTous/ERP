
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

function getPaymentTypeString(index) {

    switch (index) {
        case "1":
        case 1: return document.getElementById("PaymentTypeDaily").value; break;
        case "2":
        case 2: return document.getElementById("PaymentTypeWeekly").value; break;
        case "3":
        case 3: return document.getElementById("PaymentTypeBiWeekly").value; break;
        case "4": case 4:
            return document.getElementById("PaymentTypeFourWeekly").value; break;
        case "5": case 5:
            return document.getElementById("PaymentTypeMonthly").value; break;
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

function TogglePerc(isPercentage) {
    App.enPCT.setDisabled(!isPercentage);
    App.enFixedAmount.setDisabled(isPercentage);

}

function DETogglePerc(isPercentage) {
    App.dePCT.setDisabled(!isPercentage);
    App.deFixedAmount.setDisabled(isPercentage);

}

function addEntitlement() {
    var entitlementsGrid = App.entitlementsGrid,
        store = entitlementsGrid.getStore();

    entitlementsGrid.editingPlugin.cancelEdit();

    store.getSorters().removeAll();
    entitlementsGrid.getView().headerCt.setSortState(); // To update columns sort UI

    store.insert(0, {
        edId: 1,
        pct: 0,
        fixedAmount: 0,
        comments: ""

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
        edId: 1,
        pct: 0,
        fixedAmount: 0,
        comments: ""

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

var entnameRenderer = function (value) {

    var r = App.entsStore.getById(value);

    if (Ext.isEmpty(r)) {
        return "";
    }

    return r.data.name;
};

var dednameRenderer = function (value) {


    var r = App.dedsStore.getById(value);

    if (Ext.isEmpty(r)) {
        return "";
    }

    return r.data.name;
};

function CalculateFixed(pct) {

    var x = (pct / 100) * document.getElementById("BasicSalary").value;
    return x;
}
function CalculatePct(fixed) {
    var x = (fixed / document.getElementById("BasicSalary").value) * 100;
    return x;
}
function ChangeFinalAmount(amountOffset) {
    App.finalAmount.setValue(Number(App.finalAmount.value) + Number(amountOffset));
}
