<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Legals.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Legals" %>



<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />

    <script type="text/javascript" src="../Scripts/Legals.js?id=10"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>


    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css?id=11" />
    <link rel="stylesheet" type="text/css" href="CSS/Employees.css?id=15" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />

   <link rel="stylesheet" href="../Scripts/HijriCalender/redmond.calendars.picker.css" />

    <script src="../Scripts/HijriCalender/jquery.plugin.js?id=280" type="text/javascript"></script>

    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.js?id=101"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars-ar.js?id=105"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.picker.js?id=205"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.plus.js?id=107"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.islamic.js?id=109"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.islamic-ar.js?id=111"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.lang.js?id=115"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.picker-ar.js?id=120"></script>
    <script type="text/javascript">
        var cropper = null;

        var hijriSelected = false;
        var handleInputRender = function () {
            jQuery(function () {
                var calendar = jQuery.calendars.instance('Islamic', 'ar');
                jQuery('.showCal').calendarsPicker({ calendar: calendar });
            });
        }
        // function handleInputRender() {
             
        //    if (App.hijCal.value==true) {
                
        //        App.gregCal.Checked = false;
        //        jQuery(function () {

        //            var calendar = jQuery.calendars.instance('Islamic', "ar");
        //            jQuery('.showCal').calendarsPicker('destroy');
        //            jQuery('.showCal2').calendarsPicker('destroy');

        //            if (App.issueDateDisabled.value != "True")
        //                jQuery('.showCal').calendarsPicker({ calendar: calendar });
        //            if (App.expiryDateDisabled.value != "True")
        //                jQuery('.showCal2').calendarsPicker({ calendar: calendar });
        //        });
        //    }
        //    else {
        //        alert("gerg");
        //        App.hijCal.Checked = false;
        //        jQuery(function () {

        //            var calendar = jQuery.calendars.instance('Gregorian', document.getElementById("CurrentLang").value);
        //            jQuery('.showCal').calendarsPicker('destroy');
        //            jQuery('.showCal2').calendarsPicker('destroy');

        //            if (App.issueDateDisabled.value != "True")
        //                jQuery('.showCal').calendarsPicker({ calendar: calendar });
        //            if (App.expiryDateDisabled.value != "True")
        //                jQuery('.showCal2').calendarsPicker({ calendar: calendar });
        //        });
        //    }
            
        //}
       
        function setInputState(hijri) {
            
         
            App.rwIssueDateMulti.setHidden(!hijri);
           // App.rwIssueDateMulti.allowBlank = !hijri;
            App.rwExpiryDateMulti.setHidden(!hijri);
            App.rwExpiryDateMulti.allowBlank = !hijri;


            App.rwExpiryDate.setHidden(hijri);
            App.rwExpiryDate.allowBlank = hijri;

            App.rwIssueDate.setHidden(hijri);
            
          //  App.rwIssueDate.allowBlank = hijri;
            App.hijriSelected.Text = hijri;
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
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="CurrentLang" runat="server" />
        
        <ext:Hidden ID="hijriSelected" runat="server" />
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
            <ext:Hidden ID="issueDateDisabled" runat="server" />
        <ext:Hidden ID="expiryDateDisabled" runat="server" />
        <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>

          

            <Items>

                <ext:GridPanel AutoUpdateLayout="true"
                    ID="rightToWorkGrid" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="true"
                    Title="<%$ Resources: RWGridTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical" Flex="1"
                    Border="false"
                    Icon="User" DefaultAnchor="100%"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="rightToWorkStore"
                            runat="server"
                            RemoteSort="False"
                            RemoteFilter="true"
                            OnReadData="rightToWork_RefreshData"
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
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="issueDate" />
                                        <ext:ModelField Name="expiryDate" />
                                         <ext:ModelField Name="issueDateFormatted" />
                                        <ext:ModelField Name="expireDateFormatted" />
                                        <ext:ModelField Name="dtId" />
                                        <ext:ModelField Name="remarks" />
                                        <ext:ModelField Name="documentRef" />
                                        <ext:ModelField Name="dtName" />
                                        <ext:ModelField Name="fileUrl" />
                                        <ext:ModelField Name="employeeName"  />

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRW">
                                            <EventMask ShowMask="true" CustomTarget="={#{rightToWorkGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{rightToWorkStore}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <%--                            <ext:Column ID="ColName" DataIndex="employeeName.fullName" Text="<%$ Resources: FieldRWEmployeeName%>" runat="server" Width="240">
                                <Renderer Handler=" return '<u>'+ record.data['employeeName'].fullName+'</u>'" />
                            </ext:Column>--%>
                            <ext:Column ID="dtName" DataIndex="dtName" Text="<%$ Resources: FieldRWDtName%>" runat="server" Flex="4" />
                            <ext:Column ID="documentRef1" DataIndex="documentRef" Text="<%$ Resources: FieldRWDocumentRef%>" runat="server" Flex="2" />
                            <ext:Column   ID="validFrom" DataIndex="issueDate" Text="<%$ Resources: FieldRWIssueDate%>" runat="server" Width="100" >
                                <Renderer Handler="return record.data['issueDateFormatted'];" />
                                </ext:Column>
                            <ext:Column   ID="validTo" DataIndex="expiryDate" Text="<%$ Resources: FieldRWExpiryDate%>" runat="server" Width="100" >
                                <Renderer Handler="return record.data['expireDateFormatted'];" />
                                </ext:Column>
                            <ext:Column ID="remarksCol" DataIndex="remarks" Text="<%$ Resources: FieldRWRemarks%>" runat="server" Flex="2" Visible="false" />



                          
                            <ext:Column runat="server"
                                ID="colDelete" Flex="1" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="deleteRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colAttach" Visible="false"
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

                                <Renderer Handler="var d =(App.EmployeeTerminated.value=='0')?deleteRender():' ';
                                     var att ='&nbsp;';
                                     if(record.data['fileUrl']!=null)
                                    {
                                   
                                     att = attachRender()+'&nbsp;&nbsp;'+deleteAttachRender(); 
                                    }
                                    return att+'&nbsp;&nbsp;' +editRender()+'&nbsp;&nbsp;' +d;" />

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

                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPRW">

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


                <ext:GridPanel Visible="True"
                    ID="BackgroundCheckGrid" AutoUpdateLayout="true" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="true"
                    Title="<%$ Resources: BCGridTitle %>"
                    Layout="FitLayout" Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="BCStore"
                            runat="server"
                            RemoteSort="False"
                            RemoteFilter="true"
                            OnReadData="BCStore_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="date" />
                                        <ext:ModelField Name="expiryDate" />
                                        <ext:ModelField Name="ctId" />
                                        <ext:ModelField Name="remarks" />
                                        <ext:ModelField Name="fileUrl" />
                                        <ext:ModelField Name="ctName" />
                                        <ext:ModelField Name="employeeName"  />



                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewBC">
                                            <EventMask ShowMask="true" CustomTarget="={#{BackgroundCheckGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                <ext:TextField ID="TextField1" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{BCGrid}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column ID="Column1" Visible="false" DataIndex="recordId" runat="server" />
                            <%--                            <ext:Column ID="Column2" DataIndex="employeeName.fullName" Text="<%$ Resources: FieldBCEmployeeName%>" runat="server" Width="240">
                                <Renderer Handler=" return '<u>'+ record.data['employeeName'].fullName+'</u>'" />
                            </ext:Column>--%>
                            <ext:Column ID="Column3" DataIndex="ctName" Text="<%$ Resources: FieldBCCTName%>" runat="server" Flex="3" />
                            <ext:DateColumn  ID="DateColumn1" DataIndex="date" Text="<%$ Resources: FieldBCIssueDate%>" runat="server" Flex="3" />
                            <ext:DateColumn  ID="DateColumn2" DataIndex="expiryDate" Text="<%$ Resources: FieldBCExpiryDate%>" runat="server" Flex="3" />
                            <%--<ext:Column  Visible="false" ID="Column4" DataIndex="remarks" Text="<%$ Resources: FieldBCRemarks%>" runat="server" Flex="1" />--%>




                           
                            <ext:Column runat="server"
                                ID="ColBCDelete" Flex="1" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="100"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;'+deleteRender();  " />

                            </ext:Column>
                            <ext:Column runat="server" Visible="false"
                                ID="Column6"
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
                                ID="ColBCName" Visible="true"
                                Text=""
                              Flex="2"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <%--<Renderer Handler="var d =(App.EmployeeTerminated.value=='0')?deleteRender():' '; var att ='&nbsp;'; if(record.data['fileUrl']!='') att = attachRender()+'&nbsp;&nbsp;'+ deleteAttachRender(); return att+'&nbsp;&nbsp;'+editRender()+'&nbsp;&nbsp;'+d; " />--%>
                                   <Renderer Handler="var d =(App.EmployeeTerminated.value=='0')?deleteRender():' '; var att ='&nbsp;'; if(record.data['fileUrl']!=null) att = attachRender()+'&nbsp;&nbsp;'+deleteAttachRender(); return att+'&nbsp;&nbsp;' +editRender()+'&nbsp;&nbsp;' +d;" />
                            </ext:Column>



                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar4" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar2" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>

                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPBC">

                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="path" Value="record.data['fileUrl']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView2" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>

            </Items>
        </ext:Viewport>

        <ext:Window
            ID="EditRWwindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditRWWindowTitle %>"
            Width="450"
            Height="380"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="EditRWForm" DefaultButton="SaveRWButton"
                            runat="server"
                            Title="<%$ Resources: EditRWWindowTitle%>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField runat="server" Name="recordId" ID="RWID" Hidden="true" Disabled="true" />
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="dtId" Name="dtId" FieldLabel="<%$ Resources:FieldRWDocumentType%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="RWDocumentTypeStore">
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
                                        <ext:Button ID="Button6" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addDocumentType">
                                                    <ExtraParams>
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:TextField ID="documentRef" runat="server" FieldLabel="<%$ Resources:FieldRWDocumentRef%>" Name="documentRef" AllowBlank="false" />
                                 <ext:RadioGroup runat="server" ID="hijriCal"   FieldLabel="<%$ Resources:ChooseCalendarType %>"    >
                                    <Items>
                                        <ext:Radio ID="gregCal" runat="server" Name="hijriCal" InputValue="false"  InputType="Radio" BoxLabel="<%$ Resources:Common, Gregorian %>" Checked="true" >
                                    <Listeners>
                                        <Change Handler="#{hijriSelected}.setValue('true');">

                                        </Change>
                                    </Listeners>
                                        </ext:Radio>
                                        <ext:Radio ID="hijCal" runat="server" Name="hijriCal" InputValue="true"   InputType="Radio" BoxLabel="<%$ Resources:Common, Hijri %>" >
                                         <Listeners>
                                        <Change Handler="#{hijriSelected}.setValue('false');">

                                        </Change>
                                    </Listeners>
                                        </ext:Radio>
                                    </Items>
                                     <Listeners>
                                         <Change Handler="setInputState(!App.hijriCal.getChecked()[0].getValue());" />
                                     </Listeners>
                                </ext:RadioGroup>
                                <ext:TextField ID="rwIssueDateMulti" Width="250" runat="server" Margin="5" FieldLabel="<%$ Resources:FieldRWIssueDate%>" FieldCls="showCal">
                                 <Listeners>
                                                                              <Render Fn="handleInputRender" />
                                                                                                                        </Listeners>
                                   
                                </ext:TextField>
                                <ext:TextField ID="rwExpiryDateMulti" Width="250" runat="server" Margin="5" FieldLabel="<%$ Resources:FieldRWExpiryDate%>" FieldCls="showCal">
                               
                                    <Listeners>
                                                                              <Render Fn="handleInputRender" />
                                                                                                                        </Listeners>
                                   
                                </ext:TextField>
                                <ext:DateField ID="rwIssueDate" Vtype="daterange" runat="server" Name="issueDate" FieldLabel="<%$ Resources:FieldRWIssueDate%>" AllowBlank="true" >
                                                                           <CustomConfig>
                        <ext:ConfigItem Name="endDateField" Value="rwExpiryDate" Mode="Value" />
                    </CustomConfig>
                                    </ext:DateField>
                               <ext:DateField ID="rwExpiryDate" Vtype="daterange" runat="server" Name="expiryDate" FieldLabel="<%$ Resources:FieldRWExpiryDate%>" AllowBlank="false" >
                                                 <CustomConfig>
                        <ext:ConfigItem Name="startDateField" Value="rwIssueDate" Mode="Value" />
                    </CustomConfig>
                                   </ext:DateField>
                                <ext:TextArea runat="server" Name="remarks" ID="remarks" FieldLabel="<%$ Resources:FieldRWRemarks%>" />
                                <ext:TextField InputType="Password" runat="server" Name="remarks" Visible="false" ID="remarksField" FieldLabel="<%$ Resources:FieldRWRemarks%>" />
                               
                           
                                <ext:FileUploadField runat="server" ID="rwFile" Name="fileUrl" FieldLabel="<%$ Resources:FieldFile%>" AllowBlank="true" />
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveRWButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler=" CheckSession(); if (!#{EditRWForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveRW" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRWWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{RWID}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditRWForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                                <ext:Parameter Name="rwIssueDate" Value="#{rwIssueDate}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="rwExpiryDate" Value="#{rwExpiryDate}.getValue()" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        
           
        <ext:Window
            ID="EditBCWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditBCWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit" 
            >

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" >
                    <Items>
                         
                            
                        <ext:FormPanel
                            ID="EditBCTab" DefaultButton="SaveBCButton"
                            runat="server"
                            Title="<%$ Resources: EditBCWindow %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5"   >
                            <Items>
                                <ext:TextField ID="BCID" Hidden="true" runat="server"  Disabled="true" Name="recordId" />

                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="ctId" Name="ctId" FieldLabel="<%$ Resources:FieldBCCheckType%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="checkTypeStore">
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

                                                <Click OnEvent="addCheckType">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>

                                <ext:DateField ID="DateField1" runat="server" Name="date" FieldLabel="<%$ Resources:FieldBCIssueDate%>" AllowBlank="false" Vtype="daterange" >
                                       <CustomConfig>
                        <ext:ConfigItem Name="endDateField" Value="DateField2" Mode="Value" />
                    </CustomConfig>
                                    </ext:DateField>
                                <ext:DateField ID="DateField2" runat="server" Name="expiryDate" FieldLabel="<%$ Resources:FieldBCExpiryDate%>" AllowBlank="false" Vtype="daterange" >
                                      <CustomConfig>
                        <ext:ConfigItem Name="startDateField" Value="DateField1" Mode="Value" />
                    </CustomConfig>
                                    </ext:DateField>
                                <ext:TextArea runat="server" Name="remarks" ID="bcRemarks" FieldLabel="<%$ Resources:FieldBCRemarks%>" />
                                <ext:TextField Visible="false" InputType="Password" runat="server" Name="remarks" ID="bcRemarksField" FieldLabel="<%$ Resources:FieldBCRemarks%>" />
                                <ext:FileUploadField   ID="bcFile"  runat="server" Name="fileUrl1" FieldLabel="<%$ Resources:FieldFile%>" AllowBlank="true"/>
                              
                            

                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveBCButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditBCTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveBC" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditBCWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{BCID}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="BcFile" Value="#{bcFile}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditBCTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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

