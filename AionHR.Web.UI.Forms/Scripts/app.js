Ext.ns('Aion');

//var interval;
var keyUp = function (el, e) {

    var tree = App.commonTree;
    text = this.getRawValue();

    if (e.getKey() === 40) {
        tree.getRootNode().select();
    }

    if (Ext.isEmpty(text, false)) {
        clearFilter(el);
    }

    if (text.length < 3) {
        return;
    }

    tree.clearFilter();

    if (Ext.isEmpty(text, false)) {
        return;
    }

    el.triggers[0].show();

    if (e.getKey() === Ext.EventObject.ESC) {
        clearFilter(el);
    } else {
        var re = new RegExp(".*" + text + ".*", "i");

        tree.getRootNode().collapse(true, false);

        tree.filterBy(function (node) {
            var match = re.test(node.text.replace(/<span>&nbsp;<\/span>/g, "")),
                pn = node.parentNode;

            if (match && node.isLeaf()) {
                pn.hasMatchNode = true;
            }

            if (pn != null && pn.fixed) {
                if (node.isLeaf() === false) {
                    node.fixed = true;
                }
                return true;
            }

            if (node.isLeaf() === false) {
                node.fixed = match;
                return match;
            }

            return (pn != null && pn.fixed) || match;
        }, { expandNodes: false });

        tree.getRootNode().cascade(function (node) {
            if (node.isRoot) {
                return;
            }

            if ((node.getDepth() === 1) ||
               (node.getDepth() === 2 && node.hasMatchNode)) {
                node.expand(false, false);
            }

            delete node.fixed;
            delete node.hasMatchNode;
        }, tree);
    }
}
var filterSpecialKey = function (field, e) {
    var tree = App.commonTree,
        view = tree.getView();

    if (e.getKey() === e.DOWN) {
        var n = tree.getRootNode().findChildBy(function (node) {
            return node.isLeaf() && !node.data.hidden;
        }, tree, true);

        if (n) {
            tree.expandPath(n.getPath(), null, null, function () {
                tree.getSelectionModel().select(n);
                view.focusRow(n);
            });
        }
    }
};
var filterCommonTree = function (tf, e) {
    var tree = App.commonTree,
        text = tf.getRawValue();

    tree.clearFilter();

    if (Ext.isEmpty(text, false)) {
        return;
    }

    if (e.getKey() === e.ESC) {
        clearFilter();
    } else {
        var re = new RegExp(".*" + text + ".*", "i");

        tree.filterBy(function (node) {
            return re.test(node.data.text);
        });
    }
};
var clearFilter = function () {
    var field = App.filerTreeTrigger,
        tree = App.commonTree;

    field.setValue("");
    tree.clearFilter(true);
    tree.getView().focus();
};
var onTreeItemClick = function (record, e) {

    if (record.data) {
        if (record.data.click != "1") {
            // record[record.isExpanded() ? 'collapse' : 'expand']();
            e.stopEvent();

        } else {
            openNewTab(record.data.idTab, record.data.url, record.data.title, record.data.css);
        }
    }


};

function setGlobalInterval(s)
{
    interval = s;
}
var openNewTab = function (id, url, title, iconCls) {

    var tab = App.tabPanel.getComponent(id);
   // if (id != 'dashboard') {
        //alert(interval);
      //  clearInterval(interval);
        //alert('cleared');
   // }
    if (!tab) {
        tab = App.tabPanel.add({
            id: id,
            title: title,
            iconCls: iconCls,
            closable: true,
            loader: {
                url: url,
                renderer: "frame",
                loadMask: {
                    showMask: true,
                    msg: App.lblLoading.getValue()
                }
            }
        });
    }
    else {
        App.tabPanel.closeTab(tab);
        tab = App.tabPanel.add({
            id: id,
            title: title,
            iconCls: iconCls,
            closable: true,
            loader: {
                url: url,
                renderer: "frame",
                loadMask: {
                    showMask: true,
                    msg: App.lblLoading.getValue()
                }
            }
        });
    }
    App.tabPanel.setActiveTab(tab);
}
var openModule = function (id) {
    App.activeModule.value = id;
    Ext.net.Mask.show(App.lblLoading.getValue());
    var node = null;
    var tree = App.commonTree;
    tree.expand();
    if (tree.getSelectionModel().getSelection()[0]) {

        node = tree.getSelectionModel().getSelection()[0];
    }
    App.direct.BuildTree(id, {
        success: function (result) {
            Ext.net.Mask.hide();
            if (result == "Stop") {
                return;
            }

            
        //    App.tabPanel.items.each( function(item){
        //    if (item.closable) {
        //    App.tabPanel.remove(item);
        //}});
    var nodes = eval(result);

    if (nodes.length > 0) {
        // tree.initChildren(nodes);
        tree.setRootNode(nodes[0]);
    
        if (node != null) {

            if (node.data.id != "root") {


                var nodeToExpand = '';
                var arr = node.data.id.split('_');
                var nodeSelected = null;
                for (var i = 0; i < arr.length; i++) {
                    var nameNodeID = arr[i];
                    if (nodeToExpand == '')
                        nodeToExpand = nameNodeID;
                    else
                        nodeToExpand = nodeToExpand + '_' + nameNodeID;


                    nodeSelected = tree.getStore().getNodeById(nodeToExpand);
                    if (nodeSelected) {

                        if (!nodeSelected.isLeaf())
                            nodeSelected.expand();
                    }
                }
                tree.getSelectionModel().select(nodeSelected);  //to highlight
            }
        }
    }
    else {
        tree.getRootNode().removeChildren();
    }
},
    failure: function (errorMsg) {
        Ext.net.Mask.hide();
        Ext.Msg.buttonText.ok = App.lblOk.getValue();
        Ext.Msg.alert(App.lblError.getValue(), errorMsg, function () {
        });
    }
});













}



