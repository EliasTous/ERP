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
    <script src="../Scripts/jquery-new.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>

    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/ImportAttendance.js?id=5"></script>
    <script type="text/javascript">
        function validateRecord(rec) {
            //if (rec.data['employeeId'] == null || rec.data['employeeId'] == '')
            //    return false;
            //if (!validateFrom(rec.data['checkIn']))
            //    return false;
            //if (!validateFrom(rec.data['checkOut']))
            //    return false;
            //if (!validateTo(rec.data['checkOut'], rec.data['checkIn']))
            //    return false;
            //if (!moment(rec.data['dayId'], 'YYYYMMDD', true).isValid())
            //    return false;
            return true;
        }
        function setDisable() {
            //App.UploadAttendancesButton.setDisabled(false);
            //App.attendanceShiftGrid.getStore().each(function (record) {

            //    if (!validateRecord(record)) {
                 
            //        App.UploadAttendancesButton.setDisabled(true);
            //        return;
            //    }

            //});
           
        }
    </script>

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
                    Icon="ApplicationSideList" Header="false" Layout="HBoxLayout"
                    >
                    <Items>
                        <ext:Panel runat="server" flex="1"/>
                        <ext:Panel runat="server" Flex="2">
                            <Items>
                                 <ext:Label MarginSpec="0 0 0 0" runat="server" Text="<%$ Resources: Pick %>" Width="400" />
                        <ext:FileUploadField runat="server" ID="fileUpload" Width="400" >
                            <Listeners>
                                <Change Handler="validateFile(App.fileUpload.fileInputEl.id);" />
                            </Listeners>
                            </ext:FileUploadField>
                            </Items>
                        </ext:Panel>
                       

                    </Items>
                    <Buttons>
                        <ext:Button ID="SubmitFileButton" runat="server" Text="<%$ Resources:Import %>" Icon="Disk">

                            <Listeners>
                                <Click Handler="CheckSession(); if (!#{uploadFileForm}.getForm().isValid()) {return false;} " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SubmitFile" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true" />
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
                    <Store>
                        <ext:Store runat="server" ID="attendanceShiftStore" PageSize="50">

                            <Model>
                                <ext:Model runat="server">
                                    <Fields>

                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="employeeRef" />
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

                            <ext:Column runat="server" DataIndex="checkIn" Text="<%$ Resources: FieldCheckIn %>" Flex="1">

                                <Renderer Handler="var d = record.data['checkIn']; var err =!validateFrom(d)?errorRender():''; return d+err; " />
                            </ext:Column>
                            <ext:Column runat="server" DataIndex="checkOut" Text="<%$ Resources: FieldCheckOut %>" Flex="1">
                                <Renderer Handler="var d = record.data['checkOut']; var err =!validateFrom(d)?errorRender():''; return d+err; " />
                            </ext:Column>
                            <ext:Column runat="server" DataIndex="employeeRef" Text="<%$ Resources: FieldEmployee %>" Flex="1">
                                <Renderer Handler="var d= record.data['employeeId'];  var err= (d==null||d=='')? errorRender():'' ;return record.data['employeeRef']+err;" />
                            </ext:Column>
                            <ext:Column runat="server" DataIndex="dayId" Flex="1" Text="<%$ Resources: FieldDay %>">
                                <Renderer Handler="var d = record.data['dayId']; var err = moment(d, 'YYYYMMDD', true).isValid()?'':errorRender();  return d+ err;" />
                            </ext:Column>
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

                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;' +deleteRender()+'&nbsp;&nbsp;'" />
                            </ext:Column>
                        </Columns>

                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar runat="server" />
                    </BottomBar>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPShift">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                <ext:Parameter Name="checkOut" Value="record.data['checkOut']" Mode="Raw">
                                </ext:Parameter>
                                <ext:Parameter Name="employeeId" Value="record.data['employeeRef']" Mode="Raw" />
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="checkIn" Value="record.data['checkIn']" Mode="Raw" />

                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <Buttons>
                        <ext:Button ID="UploadAttendancesButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                <Click Handler="CheckSession();  App.loadingWindow.show();" />
                                
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="UploadAttendances" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="attendances" Value="Ext.encode(#{attendanceShiftGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>

                    </Buttons>
                    
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
        <ext:Window
            ID="EditShiftWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="300"
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
                        <ext:TextField ID="dayId" runat="server" Name="dayId" FieldLabel="<%$ Resources: FieldDay %>" />
                        <ext:TextField ID="employeeRef" runat="server" Name="employeeRef" FieldLabel="<%$ Resources:FieldEmployee%>" />
                        <ext:TextField ID="checkIn" runat="server" FieldLabel="<%$ Resources:FieldCheckIn%>" Name="checkIn" AllowBlank="false">
                            <Plugins>
                                <ext:InputMask Mask="99:99" />

                            </Plugins>
                            <Validator Handler="return validateFrom(this.getValue());" />
                        </ext:TextField>

                        <ext:TextField ID="checkOut" runat="server" FieldLabel="<%$ Resources:FieldCheckOut%>" Name="checkOut" AllowBlank="true">
                            <Plugins>
                                <ext:InputMask Mask="99:99" AllowInvalid="true" />
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
                <ext:Window
            ID="loadingWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="300"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Draggable="false"
            Maximizable="false"
            Resizable="false" Header="false"
            Layout="Fit">

            <Items>

             <ext:ProgressBar runat="server" ID="progressBar" />


            </Items>
            
        </ext:Window>


    </form>
</body>
</html>


