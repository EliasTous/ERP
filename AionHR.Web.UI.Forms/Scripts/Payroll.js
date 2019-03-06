
var editRender = function () {
    return '<img class="imgEdit" style="cursor:pointer;" src="../Images/Tools/edit.png" />';
};

var deleteRender = function () {
    return '<img class="imgDelete"  style="cursor:pointer;" src="../Images/Tools/delete.png" />';
};
var attachRender = function () {
    return '<img class="imgAttach"  style="cursor:pointer;" src="../Images/Tools/attach.png" />';
};

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
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
        case "1":
        case 1: return document.getElementById("PaymentMethodCash").value; break;
        case "2":
        case 2: return document.getElementById("PaymentMethodBank").value; break;

        default: return index;
    }
}
function TogglePaymentMethod(index) {
    App.accountNumber.setDisabled(index == 1);
    App.bankId.setDisabled(index == 1);
    //App.bankName.setDisabled(index == 1);
 

}

function TogglePerc(isPercentage) {
    App.enPCT.setDisabled(!isPercentage);
    App.enFixedAmount.setDisabled(isPercentage);
    if (isPercentage)
        App.enFixedAmount.setValue(0);
    else
        App.enPCT.setValue(0);

}

function DETogglePerc(isPercentage) {
    App.dePCT.setDisabled(!isPercentage);
    App.deFixedAmount.setDisabled(isPercentage);
    App.pctOf.setDisabled(!isPercentage);
    App.pctOf.setValue(1);
    if (isPercentage)
        App.deFixedAmount.setValue(0);
    else
        App.dePCT.setValue(0);

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

function CalculateFixed(pct, pctOf) {
   
    
    var x=0;
    if(pctOf==1)
        x = (pct / 100) * parseFloat(App.BasicSalary.getValue());
    else
        x = (pct / 100) * (parseInt(App.BasicSalary.getValue()) + parseFloat(App.eAmount.value));
  
    return x;
}
function CalculatePct(fixed) {
    var x = (fixed /parseFloat(App.BasicSalary.getValue())) * 100;
    return x;
}
function ChangeFinalAmount(amountOffset) {
 
    App.finalAmount.setValue(parseFloat(App.finalAmount.getValue()) + amountOffset);
   
}
function ReCalculateFinal()

{
   
    
    var x = parseFloat(App.eAmount.getValue().replace(/,/g, '')) - parseFloat(App.dAmount.getValue().replace(/,/g, '')) + parseFloat(App.BasicSalary.getValue().replace(/,/g, ''));
   //alert("final is" + x);
    
    App.finalAmount.setValue(numberWithCommas(parseFloat(x).toFixed(2)));
   
}
function ChangeEntitlementsAmount(amountOffset) {
   
   
    var sum = 0;
    var x;
    App.entitlementsGrid.getStore().each(function (record) {
       
        if (record.data['pct'] == '0')
            sum += parseFloat(record.data['fixedAmount']);
        else {

            x = (record.data['pct'] / 100) * parseFloat(App.BasicSalary.getValue().replace(/,/g, ''));
           
            record.set('fixedAmount', x);
            record.commit();
            sum += x;
        }
    });
    //alert("entitlements are " + sum);
    App.eAmount.setValue(sum);
    ChangeDeductionsAmount(0);
    ReCalculateFinal();
}


function ChangeDeductionsAmount(amountOffset) {
   
    var sum = 0;
    var x;
    App.deductionGrid.getStore().each(function (record) {
       
        if (record.data['pct'] == '0')
          
            sum += record.data['fixedAmount'];
        else {
            if (record.data['pctOf'] == '1')
                x = (record.data['pct'] / 100) * parseFloat(App.BasicSalary.getValue().replace(/,/g, ''));
            else
                x = (record.data['pct'] / 100) * (parseFloat(App.BasicSalary.getValue().replace(/,/g, '')) + parseFloat(App.eAmount.value.replace(/,/g, '')));
            record.set('fixedAmount', x);
            record.commit();
            sum += x;

        }
    });
    //alert("deductions are " + sum);
    App.dAmount.setValue(sum);
    ReCalculateFinal();
}
