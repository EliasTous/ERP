
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

