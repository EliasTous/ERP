<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeProfileControl.ascx.cs" Inherits="AionHR.Web.UI.Forms.EmployeeProfileControl" %>
<link rel="stylesheet" type="../text/css" href="CSS/Common.css?id=11" />
<script src="Scripts.js" type="../text/javascript"></script>
<link rel="stylesheet" type="text/css" href="../CSS/Employees.css?id=15" />
<link rel="stylesheet" href="../CSS/LiveSearch.css" />
<script src="../Scripts/jquery-new.js"></script>
<link rel="stylesheet" type="text/css" href="../CSS/cropper.css">

<script type="text/javascript" src="../Scripts/cropper.js"></script>

<script type="text/javascript" src="../Scripts/common.js?id=1"></script>

<script type="text/javascript" src="../Scripts/Employees2.js?id=16"></script>
<script type="text/javascript">

    var editRender = function () {
        return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
    };

    var deleteRender = function () {
        return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
    };
    var attachRender = function () {
        return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
    };

    var options;
    var commandName;

    var getCellType = function (grid, rowIndex, cellIndex) {

        if (cellIndex == 0)
            return "";
        if (commandName != "")
            return commandName;
        var columnId = grid.columns[cellIndex].id; // Get column id

        return columnId;
    };

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
        var out = '';
        for (var i in obj) {
            out += i + ": " + obj[i] + "\n";


        }
        return out;
    }
    function FillLeftPanel(departmentName, branchName, positionName, reportToName, balance, lastLeave, paid, leaveBalance, allowedLeaves, esName, serviceDuration) {




        App.employeeControl1_reportsToLbl.setText(reportToName);
        App.employeeControl1_eosBalanceTitle.setText(balance);
        App.employeeControl1_lastLeaveStartDateTitle.setText(lastLeave);
        App.employeeControl1_paidLeavesYTDTitle.setText(paid);
        App.employeeControl1_leavesBalanceTitle.setText(leaveBalance);
        App.employeeControl1_allowedLeaveYtdTitle.setText(allowedLeaves);
        App.employeeControl1_esName = esName;
        App.employeeControl1_serviceDuration = serviceDuration;
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
        var croppedCanvas;
        var roundedCanvas;



        // Crop
        croppedCanvas = $('#image').cropper('getCroppedCanvas');

        // Round
        roundedCanvas = getRoundedCanvas(croppedCanvas);


        var dataURL = roundedCanvas.toDataURL("image/png");
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
                url: 'EmployeePhotoUploaderHandler.ashx?classId=31000&recordId=' + App.employeeControl1_CurrentEmployee.value,
                data: fd,
                processData: false,
                contentType: false,
                error: function (s) { Ext.net.Mask.hide(); alert(dump(s)); }
            }).done(function (data) {
                App.direct.employeeControl1.FillLeftPanelDirect();
                Ext.net.Mask.hide();
                App.employeeControl1_imageSelectionWindow.hide();

            });
        }

        );

        return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
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
        App.employeeControl1_lastLeaveStartDateTitle.el.setStyle('float', alignment);
        App.employeeControl1_lastLeaveStartDateLbl.el.setStyle('float', alignment);
        App.employeeControl1_paidLeavesYTDTitle.el.setStyle('float', alignment);
        App.employeeControl1_paidLeavesYTDLbl.el.setStyle('float', alignment);
        App.employeeControl1_leavesBalanceTitle.el.setStyle('float', alignment);
        App.employeeControl1_leavesBalance.el.setStyle('float', alignment);
        App.employeeControl1_allowedLeaveYtdTitle.el.setStyle('float', alignment);
        App.employeeControl1_paidLeavesYTDLbl.el.setStyle('float', alignment);
        App.employeeControl1_allowedLeaveYtd.el.setStyle('float', alignment);
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
<ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
<ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
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

<ext:Window
    ID="EditRecordWindow"
    runat="server"
    Icon="PageEdit"
    Title="<%$ Resources:EditWindowsTitle %>"
    Width="900"
    Height="500"
    AutoShow="false"
    Modal="true"
    Hidden="true"
    Maximizable="false"
    Header="true"
    Draggable="false"
    Resizable="false"
    Maximized="false"
    Layout="BorderLayout">
    <Listeners>
        <BeforeClose Handler="#{imgControl}.src ='Images/empPhoto.png';" />
    </Listeners>
    <HeaderConfig runat="server">
        <Items>
            <ext:Button runat="server" Icon="PageGear" ID="gearButton">
                <Menu>
                    <ext:Menu runat="server">
                        <Items>
                            <ext:MenuItem runat="server" ID="resetPasswordGear" Text="<%$ Resources:ResetPassword %>" Icon="Exclamation">
                                <Listeners>
                                    <Click Handler="CheckSession();" />

                                </Listeners>
                                <DirectEvents>
                                    <Click OnEvent="ResetPassword" />
                                </DirectEvents>
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" ID="terminationGear" Text="<%$ Resources:terminationWindowTitle %>" Icon="Stop">
                                <Listeners>
                                    <Click Handler="CheckSession();" />
                                </Listeners>
                                <DirectEvents>
                                    <Click OnEvent="ShowTermination" />
                                </DirectEvents>
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" ID="deleteGear" Text="<%$ Resources:Common,Delete %>" Icon="Cancel">
                                <Listeners>
                                    <Click Handler="CheckSession();" />

                                </Listeners>
                                <DirectEvents>
                                    <Click OnEvent="promptDelete" />
                                </DirectEvents>
                            </ext:MenuItem>
                            <ext:MenuItem ID="historyGear" Text="<%$ Resources:Common,History %>" Icon="Clock">
                                <Listeners>
                                    <Click Handler="CheckSession(); parent.OpenTransactionLog(#{CurrentClassId}.value,#{CurrentEmployee}.value);" />
                                </Listeners>
                            </ext:MenuItem>


                        </Items>
                    </ext:Menu>
                </Menu>
            </ext:Button>
        </Items>
    </HeaderConfig>
    <Items>

        <ext:Panel ID="leftPanel" runat="server" Region="West" PaddingSpec="00 0 0" Padding="0" TitleAlign="Center" DefaultAnchor="100%"
            Header="false" Collapsible="false" BodyPadding="5" Width="150" StyleSpec="border-left:2px solid #2A92D4;border-right:2px solid #2A92D4;"
            Title="<%$ Resources:Common , NavigationPane %>" CollapseToolText="<%$ Resources:Common , CollapsePanel %>" ExpandToolText="<%$ Resources:Common , ExpandPanel %>" BodyBorder="0">

            <Items>
                <ext:Panel runat="server" ID="alignedPanel" Header="false">
                    <Listeners>
                        <AfterLayout Handler="AlignPanel();" />
                    </Listeners>
                    <Items>

                        <ext:Image runat="server" ID="imgControl" Width="100" Height="100" Align="Middle" MarginSpec="15 0 0 20 ">
                            <Listeners>
                                <%--<Click Handler="triggierImageClick(App.employeeControl1_picturePath.fileInputEl.id); " />--%>
                                <Click Handler="if(App.employeeControl1_terminated.value=='0'){InitCropper(App.employeeControl1_CurrentEmployeePhotoName.value); App.employeeControl1_imageSelectionWindow.show()}" />
                            </Listeners>

                        </ext:Image>


                        <ext:FileUploadField ID="picturePath" runat="server" ButtonOnly="true" Hidden="true">

                            <Listeners>
                                <Change Handler="showImagePreview(App.employeeControl1_picturePath.fileInputEl.id);" />
                            </Listeners>
                            <DirectEvents>
                            </DirectEvents>
                        </ext:FileUploadField>
                        <ext:Panel runat="server" ID="img" MarginSpec="20 0 0 0">

                            <Items>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="fullNameLbl" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="departmentLbl" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="branchLbl" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="positionLbl" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="reportsToLbl" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="esName" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="serviceDuration" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="eosBalanceTitle" Text="<%$ Resources:eosBalanceTitle %>" runat="server" />
                                        <ext:Label ID="eosBalanceLbl" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="lastLeaveStartDateTitle" Text="<%$ Resources:lastLeaveStartDateTitle %>" runat="server" />
                                        <ext:Label ID="lastLeaveStartDateLbl" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="paidLeavesYTDTitle" Text="<%$ Resources:paidLeavesYTDTitle %>" runat="server" />
                                        <ext:Label ID="paidLeavesYTDLbl" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="leavesBalanceTitle" Text="<%$ Resources:leavesBalanceTitle %>" runat="server" />
                                        <ext:Label ID="leavesBalance" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Label ID="allowedLeaveYtdTitle" Text="<%$ Resources:allowedLeaveYtdTitle %>" runat="server" />
                                        <ext:Label ID="allowedLeaveYtd" runat="server" />
                                    </Items>
                                </ext:Panel>
                                <ext:HyperlinkButton runat="server" Text="<%$ Resources:DisplayTeamLink %>">
                                    <Listeners>
                                        <Click Handler="CheckSession()" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="DisplayTeam" />
                                    </DirectEvents>
                                </ext:HyperlinkButton>

                            </Items>
                        </ext:Panel>



                        <%--<ext:Label runat="server" ID="employeeName" />--%>
                    </Items>

                </ext:Panel>

            </Items>

        </ext:Panel>




        <ext:TabPanel ID="panelRecordDetails" Layout="FitLayout" DefaultAnchor="100%" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Region="Center">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarFill runat="server">
                        </ext:ToolbarFill>

                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Items>
                <ext:FormPanel DefaultButton="SaveButton"
                    ID="BasicInfoTab" PaddingSpec="40 0 0 0"
                    runat="server"
                    Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5" Layout="TableLayout">


                    <Items>
                        <ext:Panel runat="server" Margin="20">
                            <Items>
                                <ext:TextField ID="recordId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Name="recordId" />
                                <ext:TextField ID="reference" runat="server" FieldLabel="<%$ Resources:FieldReference%>" Name="reference" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="firstName" runat="server" FieldLabel="<%$ Resources:FieldFirstName%>" Name="firstName" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>">
                                </ext:TextField>
                                <ext:TextField ID="middleName" runat="server" FieldLabel="<%$ Resources:FieldMiddleName%>" Name="middleName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="lastName" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:FieldLastName%>" Name="lastName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="familyName" runat="server" FieldLabel="<%$ Resources:FieldFamilyName%>" Name="familyName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="idRef" runat="server" AllowBlank="true" FieldLabel="<%$ Resources:FieldIdRef%>" Name="idRef" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="homeEmail" runat="server" FieldLabel="<%$ Resources:FieldHomeEmail%>" Name="homeMail" Vtype="email" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="workEmail" runat="server" FieldLabel="<%$ Resources:FieldWorkEmail%>" Name="workMail" Vtype="email" BlankText="<%$ Resources:Common, MandatoryField%>" />



                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" MarginSpec="0 0 0 100">
                            <Items>
                                <ext:RadioGroup ID="gender" AllowBlank="true" runat="server" GroupName="gender" FieldLabel="<%$ Resources:FieldGender%>">
                                    <Items>
                                        <ext:Radio runat="server" ID="gender0" Name="gender" InputValue="0" BoxLabel="<%$ Resources:Common ,Male%>" />
                                        <ext:Radio runat="server" ID="gender1" Name="gender" InputValue="1" BoxLabel="<%$ Resources:Common ,Female%>" />
                                    </Items>
                                </ext:RadioGroup>
                                <ext:TextField ID="mobile" AllowBlank="true" MinLength="6" MaxLength="18" runat="server" FieldLabel="<%$ Resources:FieldMobile%>" Name="mobile" BlankText="<%$ Resources:Common, MandatoryField%>">
                                    <Validator Handler="return !isNaN(this.value);" />
                                </ext:TextField>
                                <ext:ComboBox ID="religionCombo" runat="server" FieldLabel="<%$ Resources:FieldReligion%>" Name="religion" IDMode="Static" SubmitValue="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion0%>" Value="0"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion1%>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion2%>" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion3%>" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion4%>" Value="4"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion5%>" Value="5"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion6%>" Value="6"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                                <ext:DateField
                                    runat="server" ID="birthDate"
                                    Name="birthDate"
                                    FieldLabel="<%$ Resources:FieldDateOfBirth%>"
                                    MsgTarget="Side"
                                    AllowBlank="true" />
                                <ext:ComboBox runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="true" DisplayField="name" ID="nationalityId" Name="nationalityId" FieldLabel="<%$ Resources:FieldNationality%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="NationalityStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <RightButtons>
                                        <ext:Button ID="Button7" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();" />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addNationality">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:FieldContainer runat="server" Border="true" Visible="false">
                                    <Items>
                                        <ext:ComboBox Enabled="false" runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="departmentId" Name="departmentId" FieldLabel="<%$ Resources:FieldDepartment%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="departmentStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="recordId" />
                                                                <ext:ModelField Name="name" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <RightButtons>
                                                <ext:Button ID="Button2" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addDepartment">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler="  if(!this.readOnly) this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox Enabled="false" runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="BranchStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="recordId" />
                                                                <ext:ModelField Name="name" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <RightButtons>
                                                <ext:Button ID="Button3" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();" />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addBranch">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>



                                        <ext:ComboBox Enabled="false" runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="divisionId" Name="divisionId" FieldLabel="<%$ Resources:FieldDivision%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="divisionStore" IDMode="Explicit">
                                                    <Model>
                                                        <ext:Model runat="server" IDProperty="recordId">
                                                            <Fields>
                                                                <ext:ModelField Name="recordId" />
                                                                <ext:ModelField Name="name" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <RightButtons>
                                                <ext:Button ID="Button4" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addDivision">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox Enabled="false" ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="positionId" Name="positionId" FieldLabel="<%$ Resources:FieldPosition%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="positionStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="recordId" />
                                                                <ext:ModelField Name="name" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <RightButtons>
                                                <ext:Button ID="Button1" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addPosition">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:FieldContainer>


                                <ext:ComboBox runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="vsId" Name="vsId" FieldLabel="<%$ Resources:FieldVacationSchedule%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="VacationScheduleStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>

                                </ext:ComboBox>
                                <ext:ComboBox runat="server" ID="caId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" Name="caId" FieldLabel="<%$ Resources:FieldWorkingCalendar%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="CalendarStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>

                                </ext:ComboBox>
                                <ext:TextField ID="birthPlace" runat="server" FieldLabel="<%$ Resources:FieldBirthPlace%>" Name="placeOfBirth" AllowBlank="true" />



                                <ext:DateField
                                    runat="server"
                                    Name="hireDate" ID="hireDate"
                                    FieldLabel="<%$ Resources:FieldHireDate%>"
                                    MsgTarget="Side"
                                    AllowBlank="false" />


                            </Items>
                        </ext:Panel>
                        <%-- <ext:Panel runat="server" Margin="20" Visible="false">
                                    <Items>
                                        <ext:Image runat="server" ID="imgControl" Width="200" Height="200">
                                            <Listeners>
                                                <Click Handler="triggierImageClick(App.employeeControl1_picturePath.fileInputEl.id); " />
                                            </Listeners>
                                        </ext:Image>
                                        <ext:FileUploadField ID="picturePath" runat="server" ButtonOnly="true" Hidden="true">

                                            <Listeners>
                                                <Change Handler="showImagePreview(App.employeeControl1_picturePath.fileInputEl.id);" />
                                            </Listeners>
                                        </ext:FileUploadField>

                                    </Items>
                                </ext:Panel>--%>
                    </Items>
                    <BottomBar>
                        <ext:Toolbar runat="server" ClassicButtonStyle="false" Cls="tlb-BackGround">

                            <Items>
                                <ext:Button Cls="x-btn-left" ID="DeleteButton" Visible="false" Text="Delete" DefaultAlign="Left" AlignTarget="Left" Icon="Delete" Region="West" runat="server">
                                    <Listeners>
                                        <Click Handler="CheckSession();  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="DeleteRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>

                                <ext:ToolbarFill runat="server" />
                                <ext:Button Cls="x-btn-left" ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {  return false;} " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button Visible="false" Cls="x-btn-right" ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                                    <Listeners>
                                        <Click Handler="this.up('window').hide();" />
                                    </Listeners>
                                </ext:Button>

                            </Items>
                        </ext:Toolbar>
                    </BottomBar>

                </ext:FormPanel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: Hiring %>" ID="Panel7" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Hire.aspx" Mode="Frame" ID="Loader7" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: JobInformationTab %>" ID="profilePanel" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/JobInformation.aspx" Mode="Frame" ID="profileLoader" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: PayrollTab %>" ID="Panel1" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Payroll.aspx" Mode="Frame" ID="Loader1" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: NotesTab %>" ID="Panel2" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Notes.aspx" Mode="Frame" ID="Loader2" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: DocumentsTab %>" ID="Panel3" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Documents.aspx" Mode="Frame" ID="Loader3" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: SkillsTab %>" ID="Panel4" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Skills.aspx" Mode="Frame" ID="Loader4" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>

                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: LegalsTab %>" ID="Panel5" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Legals.aspx" Mode="Frame" ID="Loader5" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>

                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: ContactsTab %>" ID="Panel6" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Contacts.aspx" Mode="Frame" ID="Loader6" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: Dependants %>" ID="Panel8" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Dependants.aspx" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>

            </Items>

        </ext:TabPanel>

    </Items>

</ext:Window>
<ext:Window
    ID="terminationWindow"
    runat="server"
    Icon="PageEdit"
    Title="<%$ Resources:terminationWindowTitle %>"
    Width="450"
    Height="330"
    AutoShow="false"
    Modal="true"
    Hidden="true"
    Layout="Fit">

    <Items>
        <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
            <Items>
                <ext:FormPanel
                    ID="terminationForm"
                    runat="server" DefaultButton="SaveButton"
                    Title="<%$ Resources: terminationWindowTitle %>"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        <ext:TextField ID="TextField1" Hidden="true" runat="server" Disabled="true" DataIndex="recordId" />
                        <ext:DateField runat="server" ID="date" Name="date" AllowBlank="false" FieldLabel="<%$ Resources: FieldTerminationDate %>" Format="MM/dd/yyyy" />
                        <ext:ComboBox ID="ttId" runat="server" FieldLabel="<%$ Resources:FieldTerminationType%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" Name="ttId" AllowBlank="false">
                            <Items>
                                <ext:ListItem Text="<%$ Resources:Worker%>" Value="0"></ext:ListItem>
                                <ext:ListItem Text="<%$ Resources:Company%>" Value="1"></ext:ListItem>
                                <ext:ListItem Text="<%$ Resources:Other%>" Value="2"></ext:ListItem>
                            </Items>
                        </ext:ComboBox>

                        <ext:ComboBox Enabled="false" ValueField="recordId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="trId" Name="trId" FieldLabel="<%$ Resources:FieldTerminationReason%>" SimpleSubmit="true">
                            <Store>
                                <ext:Store runat="server" ID="trStore">
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="name" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>

                                </ext:Store>
                            </Store>
                            <RightButtons>
                                <ext:Button ID="Button5" runat="server" Icon="Add" Hidden="true">
                                    <Listeners>
                                        <Click Handler="CheckSession(); 
                                                     App.direct.employeeControl1.addTR( {
                    success: function (result) { 
                       if(result!=null)
                                                    #{trStore}.insert(0,result);
                                                    
                    }
                  
                });
                                                      " />
                                    </Listeners>
                                    <%--            <DirectEvents>

                                                <Click OnEvent="addTR" >
                                                </Click>
                                            </DirectEvents>--%>
                                </ext:Button>
                            </RightButtons>
                            <Listeners>
                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:ComboBox ID="rehire" runat="server" FieldLabel="<%$ Resources:RehireEligibilty%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" Name="rehire" AllowBlank="false">
                            <Items>
                                <ext:ListItem Text="<%$ Resources:No%>" Value="0"></ext:ListItem>
                                <ext:ListItem Text="<%$ Resources:Yes%>" Value="1"></ext:ListItem>
                                <ext:ListItem Text="<%$ Resources:NotYetKnown%>" Value="2"></ext:ListItem>
                            </Items>
                        </ext:ComboBox>
                    </Items>

                </ext:FormPanel>

            </Items>
        </ext:TabPanel>
    </Items>
    <Buttons>
        <ext:Button ID="Button6" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

            <Listeners>
                <Click Handler="CheckSession(); if (!#{terminationForm}.getForm().isValid()) {return false;} " />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="SaveTermination" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{terminationWindow}.body}" />
                    <ExtraParams>
                        <ext:Parameter Name="employeeId" Value="#{recordId}.getValue()" Mode="Raw" />
                        <ext:Parameter Name="values" Value="#{terminationForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button9" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
            <Listeners>
                <Click Handler="this.up('window').hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>

<ext:Window
    ID="confirmWindow"
    runat="server"
    Icon="PageEdit"
    Title="<%$ Resources:deleteConfirmation %>"
    Width="450"
    Height="330"
    AutoShow="false"
    Modal="true"
    Hidden="true"
    Layout="Fit">

    <Items>
        <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
            <Items>
                <ext:FormPanel
                    ID="confirmForm"
                    runat="server" DefaultButton="SaveButton"
                    Title="<%$ Resources: deleteConfirmation %>"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        <ext:TextField ID="delText" FieldLabel="<%$ Resources: confirmDelete %>" AllowBlank="true" runat="server" LabelAlign="Top" DataIndex="recordId" />

                    </Items>

                </ext:FormPanel>

            </Items>
        </ext:TabPanel>
    </Items>
    <Buttons>
        <ext:Button ID="Button11" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

            <Listeners>
                <Click Handler="CheckSession(); if (!#{confirmForm}.getForm().isValid()) {return false;} " />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="CompleteDelete" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{confirmWindow}.body}" />
                    <ExtraParams>
                        <ext:Parameter Name="delText" Value="#{delText}.getValue()" Mode="Raw" />

                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button12" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
            <Listeners>
                <Click Handler="this.up('window').hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window
    ID="imageSelectionWindow"
    runat="server"
    Icon="PageEdit"
    Title="<%$ Resources:ImageSelectionWindowTitle %>"
    Width="300"
    Height="400"
    AutoShow="false"
    Modal="true"
    Hidden="true"
    Resizable="false"
    Maximizable="false"
    Layout="Fit">

    <Items>

        <ext:FormPanel
            ID="imageUploadForm"
            runat="server" DefaultButton="SaveButton"
            Title="<%$ Resources:ImageSelectionWindowTitle %>"
            Icon="ApplicationSideList"
            Header="false"
            DefaultAnchor="100%"
            BodyPadding="5">
            <Content>
                <%--    <div class="imageBox" style="width: 290px; height: 270px;display:none;">
                            <div class="spinner" style="display: none"></div>
                            <div class="thumbBox" style="width: 290px; height: 270px; border: 3px solid black;display:none;" onclick="App.employeeControl1_uploadPhotoButton.setDisabled(false);"></div>
                            <input type="button" id="btnZoomIn" value="+" style="float: right;display:none;">
                            <input type="button" id="btnZoomOut" value="-" style="float: right;display:none;">
                        </div>--%>
                <div>
                    <img width="200" height="300" src="" id="image" crossorigin="Anonymous" />
                    <input type="button" id="button" value="press me" style="display: none;" />

                </div>
            </Content>
            <Items>
                <ext:Image runat="server" Width="150" Height="300" ID="employeePhoto" Hidden="true" Visible="false">
                </ext:Image>
                <%--<ext:Hidden runat="server" ID="imageData" Name="imageData" Visible="false" />--%>
            </Items>
            <BottomBar>
                <ext:Toolbar runat="server">
                    <Items>

                        <ext:ToolbarFill runat="server" />

                        <ext:Button runat="server" Icon="PictureAdd" Text="<%$ Resources:BrowsePicture %>">
                            <Listeners>
                                <Click Handler="triggierImageClick(App.employeeControl1_FileUploadField1.fileInputEl.id); "></Click>
                            </Listeners>
                        </ext:Button>
                        <ext:Button runat="server" ID="uploadPhotoButton" Icon="DatabaseSave" Text="<%$ Resources:UploadPicture %>">
                            <Listeners>

                                <Click Handler="CheckSession();   if (!#{imageUploadForm}.getForm().isValid() ) {  return false;} if(#{CurrentEmployee}.value==''){#{imageSelectionWindow}.hide(); $('#' + $('#imgControl')[0].firstChild.id).attr('src', getRoundedCanvas($('#image').cropper('getCroppedCanvas')).toDataURL());   return false;  }  GetCroppedImage(); return; #{imageData}.value = theBlob; alert(theBlob); GetCroppedImage();  var fd = new FormData();
        fd.append('fname', #{FileUploadField1}.value);
                                   fd.append('id',null);          
                                            alert(#{imageData}.value);
                                            Ext.net.Mask.show({msg:App.employeeControl1_lblLoading.getValue(),el:#{imageSelectionWindow}.id});  
                                            if(#{FileUploadField1}.value!='')
        fd.append('data', #{imageData}.value,#{FileUploadField1}.value);
                                            else
                                            fd.append('oldUrl',#{CurrentEmployeePhotoName}.value );
                                             $.ajax({
            type: 'POST',
            url: 'EmployeePhotoUploaderHandler.ashx?classId=31000&recordId='+#{CurrentEmployee}.value,
            data: fd,
            processData: false,
            contentType: false,
                                            error:function(s){Ext.net.Mask.hide(); alert(dump(s));}
        }).done(function(data) {
            App.direct.employeeControl1.FillLeftPanelDirect();
                                            Ext.net.Mask.hide();
            App.employeeControl1_imageSelectionWindow.hide();
                                            
    }); " />

                            </Listeners>
                            <%--      <DirectEvents>
                                        <Click OnEvent="UploadImage" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{imageSelectionWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="values" Value="#{imageUploadForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                                                
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>--%>
                        </ext:Button>
                        <ext:Button runat="server" Icon="Cancel" Text="<%$ Resources:RemovePicture %>">
                            <Listeners>
                                <Click Handler="ClearImage2(); InitCropper('Images/empPhoto.jpg'); App.employeeControl1_uploadPhotoButton.setDisabled(false); " />
                            </Listeners>
                        </ext:Button>
                        <ext:FileUploadField ID="FileUploadField1" runat="server" ButtonOnly="true" Hidden="true">
                            <Listeners>
                                <Change Handler="if(document.getElementById('CurrentEmployee').value == '')showImagePreview(App.employeeControl1_FileUploadField1.fileInputEl.id); showImagePreview2(App.employeeControl1_FileUploadField1.fileInputEl.id); " />
                            </Listeners>

                        </ext:FileUploadField>
                        <ext:ToolbarFill runat="server" />
                    </Items>

                </ext:Toolbar>

            </BottomBar>



            <Listeners>

                <AfterLayout Handler="CheckSession();  
                   
                            " />
            </Listeners>
            <DirectEvents>
                <AfterLayout OnEvent="DisplayImage">
                </AfterLayout>
            </DirectEvents>
        </ext:FormPanel>


    </Items>

</ext:Window>
<ext:Window
    ID="TeamWindow"
    runat="server"
    Icon="PageEdit"
    Title="<%$ Resources:TeamWindow %>"
    Width="450"
    Height="400"
    AutoShow="false"
    Modal="true"
    Hidden="true"
    Layout="Fit">

    <Items>

        <ext:GridPanel ID="TeamGrid" runat="server" Width="200" Scroll="Vertical" HideHeaders="true" EmptyText="<%$ Resources:NoTeamMembersFound %>">

            <Store>
                <ext:Store runat="server" ID="TeamStore">
                    <Model>
                        <ext:Model runat="server" IDProperty="recordId">
                            <Fields>
                                <ext:ModelField Name="recordId" />
                                <ext:ModelField Name="pictureUrl" />
                                <ext:ModelField Name="name" IsComplex="true" />
                                <ext:ModelField Name="positionName" />


                            </Fields>
                        </ext:Model>

                    </Model>
                </ext:Store>

            </Store>
            <ColumnModel>
                <Columns>
                    <ext:Column runat="server" DataIndex="recordId" Visible="false" />
                    <ext:ComponentColumn runat="server" DataIndex="pictureUrl">
                        <Component>
                            <ext:Image runat="server" Height="100" Width="50" ImageUrl="Images/empPhoto.jpg">
                            </ext:Image>

                        </Component>
                        <Listeners>
                            <Bind Handler="if(record.get('pictureUrl')!='') cmp.setImageUrl(record.get('pictureUrl')); " />
                        </Listeners>
                    </ext:ComponentColumn>
                    <ext:Column runat="server" DataIndex="name.fullName" Flex="1">
                        <Renderer Handler="return record.data['name'].fullName +' ,'+ record.data['positionName'];" />
                    </ext:Column>

                </Columns>
            </ColumnModel>
        </ext:GridPanel>
    </Items>
</ext:Window>
