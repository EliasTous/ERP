<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportAttendance.aspx.cs" Inherits="AionHR.Web.UI.Forms.ImportAttendance" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />

    <script type="text/javascript" src="Scripts/moment.js"></script>

    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/ImportAttendance.js"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />


     



        <ext:Viewport ID="Viewport1" runat="server" Layout="CardLayout" ActiveIndex="0">
            <Items>
                <ext:FormPanel runat="server" ID="uploadFileForm"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%">
                    <Items>
                        <ext:FileUploadField runat="server" ID="fileUpload" />

                    </Items>
                    <Buttons>
                        <ext:Button ID="SubmitFileButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                <Click Handler="CheckSession(); if (!#{uploadFileForm}.getForm().isValid()) {return false;} " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SubmitFile" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true"  />
                                    <ExtraParams>

                                        <ext:Parameter Name="file" Value="#{uploadFileForm}.getForm().getValues()" Mode="Raw" Encode="true" />

                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>

             <ext:GridPanel runat="server"
                    ID="attendanceShiftGrid"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Flex="1"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                         
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store runat="server" ID="attendanceShiftStore">

                            <Model>
                                <ext:Model runat="server" IDProperty="recordId">
                                    <Fields>
                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="dayId" />
                                        <ext:ModelField Name="checkIn" />
                                        <ext:ModelField Name="checkOut" />
                                        <ext:ModelField Name="duration" />


                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                             <ext:Column runat="server" DataIndex="recordId"  Visible="false" />
                            <ext:Column runat="server" DataIndex="dayId"  Visible="false" />
                            <ext:Column runat="server" DataIndex="employeeId"  Visible="false" />
                            <ext:Column runat="server" DataIndex="checkIn" Text="<%$ Resources: FieldCheckIn %>" Flex="1" />
                            <ext:Column runat="server" DataIndex="checkOut" Text="<%$ Resources: FieldCheckOut %>" Flex="1" />
                            <ext:Column runat="server" DataIndex="duration" Text="<%$ Resources: FieldHoursWorked %>" Flex="1" />
                            <ext:Column runat="server"
                                ID="Column3" Visible="true"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />
                                </ext:Column>
                        </Columns>

                    </ColumnModel>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPShift">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                <ext:Parameter Name="checkedOut" Value="record.data['checkOut']" Mode="Raw" />
                                <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
                                <ext:Parameter Name="shiftId" Value="record.data['recordId']" Mode="Raw" />
                                <ext:Parameter Name="checkedIn" Value="record.data['checkIn']" Mode="Raw" />
                                
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
        <ext:Window
            ID="EditShiftWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="150"
            AutoShow="false"
            Modal="true"
            Hidden="true"
             Draggable="false"
             Maximizable="false"
            Resizable="false" Header="false"
            Layout="Fit">

            <Items>
               
                        <ext:FormPanel
                            ID="EditShiftForm" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField ID="shiftDayId" runat="server" Name="shiftDayId" Hidden="true" />
                                <ext:TextField ID="shiftEmpId" runat="server" Name="shiftEmpId" Hidden="true" />
                                <ext:TextField ID="checkIn" runat="server" FieldLabel="<%$ Resources:FieldCheckIn%>" Name="checkIn" AllowBlank="false">
                                    <Plugins>
                                        <ext:InputMask Mask="99:99" />

                                    </Plugins>
                                    <Validator Handler="return validateFrom(this.getValue());" />
                                </ext:TextField>

                                <ext:TextField ID="checkOut" runat="server" FieldLabel="<%$ Resources:FieldCheckOut%>" Name="checkOut" AllowBlank="true">
                                    <Plugins>
                                        <ext:InputMask Mask="99:99" AllowInvalid="true"  />
                                    </Plugins>
                                    <Validator Handler="return validateTo(this.getValue(),this.prev().getValue());" />
                                </ext:TextField>
                            </Items>

                        </ext:FormPanel>

                  
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditShiftForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveShift" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditShiftWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="recordId" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="dayId" Value="#{shiftDayId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="EmployeeId" Value="#{shiftEmpId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditShiftForm}.getForm().getValues()" Mode="Raw" Encode="true" />
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


