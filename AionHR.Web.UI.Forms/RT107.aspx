<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT107.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT107" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css?id=4" />
    <link rel="stylesheet" href="CSS/LiveSearch.css??id=3" />
    <link rel="stylesheet" type="text/css" href="CSS/Employees.css?id=16" />
    <link  rel="stylesheet" type="text/css" href="CSS/cropper.css??id=2" />
    <link rel="stylesheet" href="Scripts/HijriCalender/redmond.calendars.picker.css??id=1" />
    <script src="Scripts.js" type="text/javascript?id=5"></script>

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript" src="Scripts/moment.js?id=3"></script>

    <script type="text/javascript" src="Scripts/common.js?id=2"></script>
    <script type="text/javascript" src="Scripts/jquery-new.js?id=1"></script>
   
  
    <script src="Scripts/HijriCalender/jquery.plugin.js?id=281" type="text/javascript"></script>

    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.js?id=10"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars-ar.js?id=20"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.picker.js?id=30"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.plus.js?id=40"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.islamic.js?id=50"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.islamic-ar.js?id=60"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.lang.js?id=70"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.picker-ar.js?id=80"></script>
     <script type="text/javascript" src="Scripts/cropper.js?id=2"></script>



<script type="text/javascript" src="Scripts/Employees2.js?id=5"></script>

    <script type="text/javascript">
        
   
     
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
        function GetFieldName(x) {
            return document.getElementById('Field' + x).value;
        }
    </script>
      <script type="text/javascript">
        var cropper = null;

        var hijriSelected = false;
      
        function setInputState(hijri) {

            
            //App.employeeControl1_hijCal.setValue(hijri); 
                           App.employeeControl1_gregCalBirthDate.setHidden(hijri);
                           App.employeeControl1_gregCalBirthDate.allowBlank = hijri;
         


                         
                           //App.employeeControl1_gregCal.setValue(!hijri);
                           App.employeeControl1_hijCalBirthDate.setHidden(!hijri)
                           App.employeeControl1_hijCalBirthDate.allowBlank = !hijri;
            


        }
        </script>
    
<script type="text/javascript">

   
    var options;
    var commandName;



    var triggierImageClick = function (id) {
        $("#" + id).click();
    }

  
    var imageData;

    var showImagePreview = function (id) {

        var input = $("#" + id)[0];
        if (input.files && input.files[0]) {

            //Check the extension and if not ok clear and notify the user

            if (checkExtension(input.files[0].name)) {

                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $("#" + $('#imgControl')[0].firstChild.id).attr('src', e.target.result);

                }
                filerdr.readAsDataURL(input.files[0]);
            }
            else {
                alert('File Format is not allowed');
                $("#" + $('#imgControl')).attr('src', '');

                App.employeeControl1_picturePath.reset();
                //Alert the user and clear the input file
            }
        }
        else {
            $("#" + $('#imgControl')[0].firstChild.id).attr('src', '');
            App.employeeControl1_picturePath.reset();
            App.employeeControl1_picturePath.Clear();
        }
    }
    var showImagePreview2 = function (id) {

        var input = $("#" + id)[0];
        if (input.files && input.files[0]) {

            //Check the extension and if not ok clear and notify the user

            if (checkExtension(input.files[0].name)) {

                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    //$("#" + $('#employeePhoto')[0].firstChild.id).attr('src', e.target.result);
                    //$('#image').attr('src', e.target.result);
                    InitCropper(e.target.result);
                    //$('#image').attr('src', e.target.result);
                    //options.imgSrc = e.target.result;


                }
                filerdr.readAsDataURL(input.files[0]);
                App.employeeControl1_uploadPhotoButton.setDisabled(false);
            }
            else {
                alert('File Format is not allowed');
                //$("#" + $('#employeePhoto')[0].firstChild.id).attr('src', '');
                App.employeeControl1_FileUploadField1.reset();

                //Alert the user and clear the input file
            }
        }
        else {
            //$("#" + $('#employeePhoto')[0].firstChild.id).attr('src', '');
            App.employeeControl1_FileUploadField1.reset();
            App.employeeControl1_FileUploadField1.Clear();
        }
    }
    var ClearImage = function () {
        App.employeeControl1_picturePath.reset();
        $("#" + $('#imgControl')[0].firstChild.id).attr('src', '');

    }
    var ClearImage2 = function () {
        App.employeeControl1_FileUploadField1.reset();
        // $("#" + $('#employeePhoto')[0].firstChild.id).attr('src', 'images/empPhoto.jpg');
        // $('#image').attr('src', 'images/empPhoto.jpg');
        //InitCropper('images/empPhoto.jpg');
        ClearCropper();
        //App.employeeControl1_uploadPhotoButton.setDisabled(true);
    }


    var checkExtension = function (file) {

        try {

            if (file == null || file == '') {
                return true;
            }
            var dot = file.lastIndexOf('.');
            if (dot >= 0) {
                var ext = file.substr(dot + 1, file.length).toLowerCase();
                if (ext in { 'jpg': '', 'png': '', 'jpeg': '' }) { return true; }
            }

            return false;
        }
        catch (e) {
            return false;
        }
    }

    var onEmployeeTreeItemClick = function (record, e) {

        if (record.data) {
            if (record.data.click != "1") {
                // record[record.isExpanded() ? 'collapse' : 'expand']();
                e.stopEvent();

            } else {

                openNewTabEmployee(record.data.idTab, record.data.url, record.data.title, record.data.css);
            }
        }


    };
    var openNewTabEmployee = function (id, url, title, iconCls) {


        var tab = App.employeeControl1_employeesTabPanel.getComponent(id);
        // if (id != 'dashboard') {
        //alert(interval);
        //  clearInterval(interval);
        //alert('cleared');
        // }



        if (!tab) {


            tab = App.employeeControl1_employeesTabPanel.add({
                id: id,
                title: title,
                iconCls: iconCls,
                closable: false,
                loader: {
                    url: url + "?employeeId=" + document.getElementById("CurrentEmployee").value,
                    renderer: "frame",
                    loadMask: {
                        showMask: true,
                        msg: App.employeeControl1_lblLoading.getValue()
                    }
                }
            });

        }
        else {
            App.employeeControl1_employeesTabPanel.closeTab(tab);
            tab = App.employeeControl1_employeesTabPanel.add({
                id: id,
                title: title,
                iconCls: iconCls,
                closable: false,
                loader: {
                    url: url,
                    renderer: "frame",
                    loadMask: {
                        showMask: true,
                        msg: App.employeeControl1_lblLoading.getValue()
                    }
                }
            });
        }
        App.employeeControl1_employeesTabPanel.setActiveTab(tab);
    }
    function dump(obj) {
        alert(obj);
        for (var i in obj) {

            out += i + ": " + obj[i] + "\n";
            alert(out);


        }
        return out;
    }
    function FillLeftPanel(departmentName, branchName, positionName, reportToName, balance, lastLeave, paid, leaveBalance, allowedLeaves, esName, usedLeaves, paidLeaves, salary, serviceDuration, TerminationDate, unpaidLeaves) {



      
        App.employeeControl1_reportsToLbl.setText(reportToName);
        App.employeeControl1_eosBalanceTitle.setText(balance);
        //App.employeeControl1_lastLeaveStartDateTitle.setText(lastLeave);
        App.employeeControl1_paidLeavesYTDTitle.setText(paid);
        App.employeeControl1_leavesBalanceTitle.setText(leaveBalance);
        App.employeeControl1_allowedLeaveYtdTitle.setText(allowedLeaves);

        App.employeeControl1_usedLeavesTitle.setText(usedLeaves);
        App.employeeControl1_paidLeavesTitle.setText(paidLeaves);
        App.employeeControl1_unpaidLeavesTitle.setText(unpaidLeaves);

        App.employeeControl1_salaryTitle.setText(salary);

        App.employeeControl1_esName = esName;
        App.employeeControl1_serviceDuration = serviceDuration;
        App.employeeControl1_TerminationDateLbl = TerminationDate;
       
        FillLeftPanel(dep, branchName, positionName);

    }
    function FillLeftPanel(departmentName, branchName, positionName, reportToName) {




        App.employeeControl1_reportsToLbl.setText(reportToName);
        FillLeftPanel(dep, branchName, positionName);

    }

    function FillFullName(fullName) {
        App.employeeControl1_fullNameLbl.Html = fullName;
    }
    function FillLeftPanel(departmentName, branchName, positionName) {



        App.employeeControl1_departmentLbl.setText(departmentName, false);
        App.employeeControl1_branchLbl.setText(branchName, false);
        App.employeeControl1_positionLbl.setText(positionName, false);


    }
    function SelectJICombos(deptId, bId, pId, dId) {
        App.employeeControl1_departmentId.select(deptId);
        App.employeeControl1_branchId.select(bId);
        App.employeeControl1_positionId.select(pId);
        App.employeeControl1_divisionId.select(dId);
    }


    var enterKeyPressSearchHandler = function (el, event) {

        var enter = false;
        if (event.getKey() == event.ENTER) {
            if (el.getValue().length > 0)
            { enter = true; }
        }

        if (enter === true) {
            App.employeeControl1_Store1.reload();
        }
    };
    function initCropper(path) {
        //options =
        //    {
        //        imageBox: '.imageBox',
        //        thumbBox: '.thumbBox',
        //        spinner: '.spinner',
        //        imgSrc: path
        //    };
        //cropper = new cropbox(options);
        // alert(path);
        $('#image').attr('src', path);
    }

    function refreshQV() {
        App.direct.employeeControl1.FillLeftPanel(true);
    }
</script>


<script type="text/javascript">
    var cropper = null;
    function getRoundedCanvas(sourceCanvas) {
        var canvas = document.createElement('canvas');
        var context = canvas.getContext('2d');
        var width = sourceCanvas.width;
        var height = sourceCanvas.height;

        canvas.width = width;
        canvas.height = height;
        context.beginPath();
        context.arc(width / 2, height / 2, Math.min(width, height) / 2, 0, 2 * Math.PI);
        context.strokeStyle = 'rgba(0,0,0,0)';
        context.stroke();
        context.clip();
        context.drawImage(sourceCanvas, 0, 0, width, height);

        return canvas;
    }
    function ClearCropper() {
        $('#image').cropper('destroy');
    }
    function InitCropper(path) {

        var $image = $('#image');
        var $button = $('#button');
        var $result = $('#result');
        var croppable = false;

        $('#image').attr('src', path);
        $('#image').cropper('destroy');
        $image.cropper({
            aspectRatio: 'd',
            viewMode: 1,
            ready: function () {
                croppable = true;
            }

        });
        $button.on('click', function () {
            var croppedCanvas;
            var roundedCanvas;

            if (!croppable) {
                return;
            }

            // Crop
            croppedCanvas = $image.cropper('getCroppedCanvas');

            // Round
            roundedCanvas = getRoundedCanvas(croppedCanvas);

            // Show  image.crossOrigin = "Anonymous";
            //$result.html('<img image.crossOrigin = "Anonymous" src="' + roundedCanvas.toDataURL() + '">');
        });

    }
    var theBlob;
    function GetCroppedImage() {

        try {
            var croppedCanvas;
            var roundedCanvas;



            // Crop
            croppedCanvas = $('#image').cropper('getCroppedCanvas');

            // Round
            roundedCanvas = getRoundedCanvas(croppedCanvas);
           
          

            var dataURL = roundedCanvas.toDataURL("image/png");
            var head = 'data:image/png;base64,';
            var imgFileSize = Math.round((dataURL.length - head.length) * 3 / 4);
            if (imgFileSize > 1580796)
            {
                Ext.MessageBox.alert(' ', App.employeeControl1_uploadImageError.getValue());
                return;
            }
            var b;
            roundedCanvas.toBlob(function (blob) {

                App.employeeControl1_imageData.value = blob; var fd = new FormData();
                fd.append('fname', App.employeeControl1_FileUploadField1.value);
                fd.append('id', null);

                Ext.net.Mask.show({ msg: App.employeeControl1_lblLoading.getValue(), el: App.employeeControl1_imageSelectionWindow.id });
                var fileName = App.employeeControl1_FileUploadField1.value;
                if (fileName == '')
                    fileName = App.employeeControl1_FileName.value;

                fd.append('data', App.employeeControl1_imageData.value, fileName);

                if (App.employeeControl1_FileUploadField1.value == '')
                    fd.append('oldUrl', App.employeeControl1_CurrentEmployeePhotoName.value);

                $.ajax({
                    type: 'POST',
                    url: 'EmployeePhotoUploaderHandler.ashx?classId=31201&recordId=' + App.employeeControl1_CurrentEmployee.value,
                    data: fd,
                    processData: false,
                    contentType: false,
                    error: function (s) { Ext.net.Mask.hide(); Ext.MessageBox.alert(' ', App.employeeControl1_uploadImageError.getValue()); }
                }).done(function (data) {
                    App.direct.employeeControl1.FillLeftPanelDirect();
                    Ext.net.Mask.hide();
                    App.employeeControl1_imageSelectionWindow.hide();

                    });

                  return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");

            }
                
            );
        }
        catch{  }
      
    }
    var handleInputRender = function () {
        jQuery(function () {
            var calendar = jQuery.calendars.instance('Islamic', 'ar');
            jQuery('.showCal').calendarsPicker({ calendar: calendar });
        });
    }
    function getBase64Image(img) {
        var canvas = document.createElement("canvas");
        canvas.width = img.width;
        canvas.height = img.height;

        var ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0);

        var dataURL = canvas.toDataURL("image/png");
        var b;
        canvas.toBlob(function (blob) { b = blob; });
        return b;

        return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
    }

    function AlignPanel() {
        
        var alignment = App.employeeControl1_pRTL.value == 'True' ? 'right' : 'left';
        
        App.employeeControl1_fullNameLbl.el.setStyle('float', alignment);
        App.employeeControl1_departmentLbl.el.setStyle('float', alignment);
        App.employeeControl1_branchLbl.el.setStyle('float', alignment);
        App.employeeControl1_positionLbl.el.setStyle('float', alignment);
        App.employeeControl1_reportsToLbl.el.setStyle('float', alignment);
        App.employeeControl1_esName.el.setStyle('float', alignment);
        App.employeeControl1_serviceDuration.el.setStyle('float', alignment);
        App.employeeControl1_eosBalanceTitle.el.setStyle('float', alignment);
        App.employeeControl1_eosBalanceLbl.el.setStyle('float', alignment);
        //App.employeeControl1_lastLeaveStartDateTitle.el.setStyle('float', alignment);
        //App.employeeControl1_lastLeaveStartDateLbl.el.setStyle('float', alignment);
        App.employeeControl1_paidLeavesYTDTitle.el.setStyle('float', alignment);
        App.employeeControl1_paidLeavesYTDLbl.el.setStyle('float', alignment);
        App.employeeControl1_leavesBalanceTitle.el.setStyle('float', alignment);
        App.employeeControl1_leavesBalance.el.setStyle('float', alignment);
        App.employeeControl1_allowedLeaveYtdTitle.el.setStyle('float', alignment);
        App.employeeControl1_paidLeavesYTDLbl.el.setStyle('float', alignment);
        App.employeeControl1_allowedLeaveYtd.el.setStyle('float', alignment);

        App.employeeControl1_usedLeavesTitle.el.setStyle('float', alignment);
        App.employeeControl1_usedLeavesLbl.el.setStyle('float', alignment);
        App.employeeControl1_paidLeavesTitle.el.setStyle('float', alignment);
        App.employeeControl1_paidLeavesLbl.el.setStyle('float', alignment);
        App.employeeControl1_salaryTitle.el.setStyle('float', alignment);
        App.employeeControl1_salaryLbl.el.setStyle('float', alignment);
        App.employeeControl1_TerminatedLbl.el.setStyle('float', alignment);
        App.employeeControl1_TerminationDateLbl.el.setStyle('float', alignment);
        App.employeeControl1_unpaidLeavesTitle.el.setStyle('float', alignment);
        App.employeeControl1_unpaidLeavesLbl.el.setStyle('float', alignment);
      
        
    }
</script>
<style type="text/css">
    .tlb-BackGround {
        background: #fff;
    }

    .calendars-popup {
        z-index: 80000 !important;
    }
</style>

</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />
        <uc:employeeControl ID="employeeControl1" runat="server" />
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="Field1" runat="server" Text="<%$ Resources:Field1 %>" />
        <ext:Hidden ID="Field2" runat="server" Text="<%$ Resources:Field2 %>" />
        <ext:Hidden ID="Field3" runat="server" Text="<%$ Resources:Field3 %>" />
        <ext:Hidden ID="Field4" runat="server" Text="<%$ Resources:Field4 %>" />
        <ext:Hidden ID="Field5" runat="server" Text="<%$ Resources:Field5 %>" />
        <ext:Hidden ID="Field6" runat="server" Text="<%$ Resources:Field6 %>" />
        <ext:Hidden ID="Field7" runat="server" Text="<%$ Resources:Field7 %>" />
        <ext:Hidden ID="Field8" runat="server" Text="<%$ Resources:Field8 %>" />
        <ext:Hidden ID="Field9" runat="server" Text="<%$ Resources:Field9 %>" />
        <ext:Hidden ID="Field10" runat="server" Text="<%$ Resources:Field10 %>" />
        <ext:Hidden ID="Field11" runat="server" Text="<%$ Resources:Field11 %>" />
        <ext:Hidden ID="Field12" runat="server" Text="<%$ Resources:Field12 %>" />
        <ext:Hidden ID="Field13" runat="server" Text="<%$ Resources:Field13 %>" />
        <ext:Hidden ID="Field14" runat="server" Text="<%$ Resources:Field14 %>" />
        <ext:Hidden ID="Field15" runat="server" Text="<%$ Resources:Field15 %>" />
        <ext:Hidden ID="timeZoneOffset" runat="server" EnableViewState="true" />
<ext:Hidden ID="CurrentEmployee" runat="server" EnableViewState="true" />
<ext:Hidden ID="CurrentClassId" runat="server" EnableViewState="true" />

<ext:Hidden ID="reportTo" runat="server" EnableViewState="true" Text="reports" />
<ext:Hidden ID="CurrentEmployeePhotoName" runat="server" EnableViewState="true" />
<ext:Hidden ID="FileName" runat="server" EnableViewState="true" />
<ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />
<ext:Hidden runat="server" ID="pRTL" Text="False" />
<ext:Hidden runat="server" ID="imageData" />
<ext:Hidden runat="server" ID="terminated" />
<ext:Hidden runat="server" ID="photoReadOnly" Text="False" />
<ext:Hidden runat="server" ID="workEmailHF" />
<ext:Hidden runat="server" ID="CurrentEmployeeFullName" />
<ext:Hidden runat="server" ID="bdHijriCal" />

        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server" IDProperty="fieldId">
                    <Fields>

                        <ext:ModelField Name="fieldId" />
                        <ext:ModelField Name="count" />






                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="CardLayout" ActiveIndex="0">
            <Items>
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    StoreID="Store1"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical" MinHeight="500"
                    Border="false" SortableColumns="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                               <ext:ComboBox runat="server" ID="inactivePref" Editable="false" FieldLabel="<%$ Resources: Status %>">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
                                    </Items>
                                    <Listeners>
                                        <Change Handler="App.Store1.reload()" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180" >
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                            <TriggerClick Handler="#{Store1}.reload();" />
                                        </Listeners>
                                    </ext:TextField>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>


                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>


                            <ext:Column ID="fff" MenuDisabled="true" runat="server" Text="<%$ Resources: MissingField%>" DataIndex="fieldId" Hideable="false" Flex="1">
                                <Renderer Handler="return GetFieldName(record.data['fieldId']);" />
                            </ext:Column>
                            <ext:Column ID="cc" runat="server" Text="<%$ Resources: Count%>" DataIndex="count" Hideable="false" Width="200" />


                           
                            <ext:Column runat="server"
                                
                                Visible="true"
                                
                                Width="100"
                                Align="Center"
                                Text="<%$ Resources: Details%>"
                                
                                
                                Resizable="false">
                                <Renderer handler="if(record.data['count']>0) return  '<img class=imgAttach  style=cursor:pointer; src=../Images/Tools/application_edit.png />';" />
                            </ext:Column>


                        </Columns>
                    </ColumnModel>
                 
                    <BottomBar>
                    </BottomBar>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>
                     <DockedItems>

                        <ext:Toolbar ID="Toolbar2" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar1" runat="server" />
                                <ext:ToolbarFill />
                                
                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    <BottomBar>

                        <ext:PagingToolbar ID="PagingToolbar1"
                            runat="server"
                            FirstText="<%$ Resources:Common , FirstText %>"
                            NextText="<%$ Resources:Common , NextText %>"
                            PrevText="<%$ Resources:Common , PrevText %>"
                            LastText="<%$ Resources:Common , LastText %>"
                            RefreshText="<%$ Resources:Common ,RefreshText  %>"
                            BeforePageText="<%$ Resources:Common ,BeforePageText  %>"
                            AfterPageText="<%$ Resources:Common , AfterPageText %>"
                            DisplayInfo="true"
                            DisplayMsg="<%$ Resources:Common , DisplayMsg %>"
                            Border="true"
                            EmptyMsg="<%$ Resources:Common , EmptyMsg %>">
                            <Items>
                               
                            </Items>
                            <Listeners>
                                <BeforeRender Handler="this.items.removeAt(this.items.length - 2);" />
                            </Listeners>
                        </ext:PagingToolbar>

                    </BottomBar>

                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>

                <ext:GridPanel runat="server"  Header="false" ID="employeesGrid" Title="<%$ Resources: EmployeesGrid%>">
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPEM">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <Store>
                        <ext:Store ID="employeesStore" runat="server">
                            <Model>
                                <ext:Model runat="server" IDProperty="recordId">
                                    <Fields>
                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="pictureUrl" />
                                        <ext:ModelField Name="name" IsComplex="true" />
                                        <ext:ModelField Name="reference" />
                                        <ext:ModelField Name="departmentName" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="divisionName" />
                                        <ext:ModelField Name="hireDate" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="Prev_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="index" Value="#{viewport1}.items.indexOf(#{viewport1}.layout.activeItem)" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>


                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <ColumnModel>
                        <Columns>
                          <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server"  DataIndex="recordId" Hideable="false" Width="75" Align="Center" />

                            <ext:ComponentColumn runat="server" DataIndex="pictureUrl" Sortable="false">
                                <Component>
                                    <ext:Image runat="server" Height="100" Width="50">
                                    </ext:Image>

                                </Component>
                                <Listeners>
                                    <Bind Handler=" cmp.setImageUrl(record.get('pictureUrl')+'?id='+new Date().getTime()); " />
                                </Listeners>
                            </ext:ComponentColumn>
                            <ext:Column ID="ColReference" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldRef%>" DataIndex="name.reference" Width="60" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].reference ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name.fullName" Flex="4" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].fullName ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false">
                            </ext:Column>
                            <ext:Column ID="ColPositionName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <ext:Column ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDivision%>" DataIndex="divisionName" Flex="3" Hideable="false" />
                            <ext:DateColumn ID="ColHireDate" Format="dd-MMM-yyyy" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldHireDate%>" DataIndex="hireDate" Width="120" Hideable="false">
                            </ext:DateColumn> 
                             <ext:Column runat="server"
                                ID="Column1" Visible="true"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return '<img class=imgEdit style=cursor:pointer; src=../Images/Tools/edit.png />';" />
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
      



    </form>
</body>
</html>


