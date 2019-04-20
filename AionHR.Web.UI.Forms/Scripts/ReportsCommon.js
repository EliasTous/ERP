function alertNow(s, e) {

    Ext.MessageBox.alert(App.Error.getValue(), e.message);
    e.handled = true;
}

function setVals(pms) {
    //alert('vals are ' + pms);
    App.direct.SetVals(pms);
    App.Panel8.loader.url = App.loaderUrl.value + pms;

}
function setTexts(pms) {
    //alert('texts are ' + pms);
    App.direct.SetTexts(pms);
    App.reportsParams.hide();




}
function showFriendlyText(txt) {

    var parts = txt.split('$');
    var newHtml = "";
    App.labelbar.setHeight(30);
    for (i = 0; i < parts.length; i++) {
        var pair = parts[i].split(':');
        if (i > 0 && i % 6 == 0) {
            newHtml += "<br />";
            App.labelbar.setHeight(App.labelbar.height + 30);
        }

        newHtml += "[&nbsp&nbsp<b>" + pair[0] + ":</b>" + pair[1] + "&nbsp&nbsp]&nbsp&nbsp";

    }

    App.selectedFilters.setHtml(newHtml);

}
function setLabels(labels) {
    //alert('captions are'+labels);

    App.direct.SetLabels(labels);
    var s = labels.split('^');

    App.reportsParams.setHeight((150 + (s.length * 25)));
}