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

function OpenTransactionLog(classref,id)
{
    App.direct.FillTransactionLogWindow(classref, id);
}

function GetChangeTypeString(index)
{
    return document.getElementById("TrType" + index).value;
}

(function () {
    $(function () {


        $('#btnChangeLanguage').click(function () {

            Ext.net.Mask.show(App.lblLoading.getValue());
            App.direct.Localise({
                success: function (result) {

                    switch (result) {

                        case "ok":
                            location.reload();
                            break;

                        default:

                            Ext.net.Mask.hide();
                            Ext.Msg.buttonText.ok = App.lblOk.getValue();
                            Ext.Msg.alert(App.lblError.getValue(), App.lblErrorOperation.getValue(), function () {
                            });
                            break;
                    }
                },
                failure: function (errorMsg) {
                    Ext.net.Mask.hide();
                    Ext.Msg.buttonText.ok = App.lblOk.getValue();
                    Ext.Msg.alert(App.lblError.getValue(), App.lblErrorOperation.getValue(), function () {
                    });
                }

            });


        });


        $('#btnLogout').click(function () {

            Ext.net.Mask.show(App.lblLoading.getValue());
            App.direct.Logout({
                success: function (result) {
                    window.location.href = result;


                },
                failure: function (errorMsg) {
                    Ext.net.Mask.hide();
                    Ext.Msg.buttonText.ok = App.lblOk.getValue();
                    Ext.Msg.alert(App.lblError.getValue(), App.lblErrorOperation.getValue(), function () {
                    });
                }

            });
        });




    });
})();

function setCompanyName(companyName) {
    App.CompanyNameLiteral.setText(companyName);
}