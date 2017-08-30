<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyRightToWorks.aspx.cs" Inherits="AionHR.Web.UI.Forms.CompanyRightToWorks" %>




<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/MediaItems.js?id=0"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="Scripts/HijriCalender/redmond.calendars.picker.css" />

    <script src="Scripts/HijriCalender/jquery.plugin.js" type="text/javascript"></script>

    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.js"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars-ar.js"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.picker.js"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.plus.js"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.islamic.js"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.islamic-ar.js"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.lang.js"></script>
    <script type="text/javascript" src="Scripts/HijriCalender/jquery.calendars.picker-ar.js"></script>
    <script type="text/javascript">
        var cropper = null;


        var handleInputRender = function () {

            if (App.hijCal.value == true) {

                jQuery(function () {

                    var calendar = jQuery.calendars.instance('Islamic', "ar");

                    jQuery('.showCal').calendarsPicker('destroy');
                    jQuery('.showCal2').calendarsPicker('destroy');
                    if (App.issueDateDisabled.value != "True")
                        jQuery('.showCal').calendarsPicker({ calendar: calendar });
                    if (App.expiryDateDisabled.value != "True")
                        jQuery('.showCal2').calendarsPicker({ calendar: calendar });
                });
            }
            else {

                jQuery(function () {

                    var calendar = jQuery.calendars.instance('Gregorian', document.getElementById("CurrentLanguage").value);

                    jQuery('.showCal').calendarsPicker('destroy');
                    jQuery('.showCal2').calendarsPicker('destroy');

                    if (App.issueDateDisabled.value != "True")
                        jQuery('.showCal').calendarsPicker({ calendar: calendar });
                    if (App.expiryDateDisabled.value != "True")
                        jQuery('.showCal2').calendarsPicker({ calendar: calendar });
                });
            }

        }

        function setInputState(hijri) {

            App.hijriCal.setHidden(!hijri);

            App.issueDateMulti.setHidden(!hijri);
            App.issueDateMulti.allowBlank = !hijri;
            App.expiryDateMulti.setHidden(!hijri);
            App.expiryDateMulti.allowBlank = !hijri;


            App.expiryDate.setHidden(hijri);
            App.expiryDate.allowBlank = hijri;

            App.issueDate.setHidden(hijri);
            App.issueDate.allowBlank = hijri;


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

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />

        <ext:Hidden ID="CurrentLanguage" runat="server" />
        <ext:Hidden ID="hijriSelected" runat="server" />
        <ext:Hidden ID="issueDateDisabled" runat="server" />
        <ext:Hidden ID="expiryDateDisabled" runat="server" />


        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="branchId" />
                        <ext:ModelField Name="dtId" />
                        <ext:ModelField Name="documentRef" />
                        <ext:ModelField Name="remarks" />
                        <ext:ModelField Name="issueDateFormatted" />
                        <ext:ModelField Name="expireDateFormatted" />
                        <ext:ModelField Name="dtName" />
                        <ext:ModelField Name="branchName" />
                        <ext:ModelField Name="fileUrl" />
                        <ext:ModelField Name="issueDate" />
                        <ext:ModelField Name="expiryDate" />

                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    StoreID="Store1"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button Visible="false" ID="btnDeleteSelected" runat="server" Text="<%$ Resources:Common , DeleteAll %>" Icon="Delete">
                                    <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
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

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column ID="Column5" Visible="true" DataIndex="dtName" Text="<%$ Resources: FieldDTName%>" runat="server" Flex="1" />
                            <ext:Column ID="Column1" Visible="true" DataIndex="branchName" Text="<%$ Resources: FieldBRName%>" runat="server" Flex="2" />

                            <ext:Column ID="Column2" Visible="true" DataIndex="documentRef" Text="<%$ Resources: FieldDocumentRef%>" runat="server" Flex="1" />
                            <ext:Column ID="Column3" Visible="true" DataIndex="remarks" Text="<%$ Resources: FieldRemarks%>" runat="server" Flex="1" />
                            <ext:Column Visible="true" ID="DateColumn1" DataIndex="issueDate" Text="<%$ Resources: FieldIssueDate%>" runat="server" Width="100" >
                                <Renderer Handler="return record.data['issueDateFormatted'];" />
                                </ext:Column>
                            <ext:Column Visible="true" ID="DateColumn2" DataIndex="expiryDate" Text="<%$ Resources: FieldExpiryDate%>" runat="server" Width="100" >
                                <Renderer Handler="return record.data['expireDateFormatted'];" />
                                </ext:Column>



                            <ext:Column runat="server"
                                ID="colDelete" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="80"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="deleteRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colAttach"
                                Text="<%$ Resources:Common, Attach %>"
                                Hideable="false"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="attachRender" />
                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text=""
                                Width="120"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="var att ='&nbsp;'; if(record.data['fileUrl']!='') att = attachRender(); return att+'&nbsp;&nbsp;' +editRender()+'&nbsp;&nbsp;' +deleteRender();" />

                            </ext:Column>



                        </Columns>
                    </ColumnModel>
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
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP" IsUpload="true" FormID="form1">

                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="path" Value="record.data['fileUrl']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="360"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="BasicInfoTab" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField ID="url" runat="server" Name="url" Hidden="true" />
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="branchId" Name="branchId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldBRName %>">
                                    <Store>
                                        <ext:Store runat="server" ID="brStore">
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

                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" ID="dtId" Name="dtId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldDTName %>">
                                    <Store>
                                        <ext:Store runat="server" ID="dtStore">
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
                                        <ext:Button ID="Button9" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addDocumentType">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>


                                <ext:TextField ID="documentRef" runat="server" FieldLabel="<%$ Resources:FieldDocumentRef%>" Name="documentRef" AllowBlank="false" />
                                <ext:TextField ID="remarks" runat="server" FieldLabel="<%$ Resources:FieldRemarks%>" Name="remarks" AllowBlank="true" />
                                <ext:RadioGroup runat="server" ID="hijriCal" GroupName="hijriCal" FieldLabel="<%$ Resources:ChooseCalendarType %>">
                                    <Items>
                                        <ext:Radio ID="gregCal" runat="server" Name="hijriCal" InputValue="false" InputType="Checkbox" BoxLabel="<%$ Resources:Common, Gregorian %>" Checked="true">
                                            <%--     <Listeners>
                                                <Change Handler="if(this.checked){InitGregorian();handleInputRender();}"  />
                                            </Listeners>--%>
                                        </ext:Radio>
                                        <ext:Radio ID="hijCal" runat="server" Name="hijriCal" InputValue="true" InputType="Checkbox" BoxLabel="<%$ Resources:Common, Hijri %>">
                                            <%--   <Listeners>
                                                <Change Handler="if(this.checked){InitHijri();handleInputRender();}" />
                                            </Listeners>--%>
                                        </ext:Radio>
                                    </Items>
                                    <Listeners>
                                        <Change Handler="handleInputRender();"></Change>
                                    </Listeners>
                                </ext:RadioGroup>
                                <ext:TextField ID="issueDateMulti" Width="250" runat="server" Margin="5" FieldLabel="<%$ Resources:FieldIssueDate%>" FieldCls="showCal">
                                </ext:TextField>
                                <ext:TextField ID="expiryDateMulti" Width="250" runat="server" Margin="5" FieldLabel="<%$ Resources:FieldExpiryDate%>" FieldCls="showCal2">
                                </ext:TextField>

                                <ext:DateField ID="issueDate" runat="server" Vtype="daterange" FieldLabel="<%$ Resources:FieldIssueDate%>" Name="issueDate" >
                                     <CustomConfig>
                        <ext:ConfigItem Name="endDateField" Value="expiryDate" Mode="Value" />
                    </CustomConfig>
                                    </ext:DateField>
                                <ext:DateField ID="expiryDate" runat="server" Vtype="daterange" FieldLabel="<%$ Resources:FieldExpiryDate%>" Name="expiryDate" >
                                    <CustomConfig>
                        <ext:ConfigItem Name="startDateField" Value="issueDate" Mode="Value" />
                    </CustomConfig>
                                    </ext:DateField>



                                <ext:FileUploadField runat="server" ID="rwFile" FieldLabel="<%$ Resources:FieldFile%>" AllowBlank="true" Name="fileUrl" />


                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler=" alert('hi');CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} if(#{issueDateMulti}.value =='' && #{issueDate}.value=='') return false;  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', #{titleSavingErrorMessage}.value);">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="url" Value="#{url}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>



    </form>
</body>
</html>
