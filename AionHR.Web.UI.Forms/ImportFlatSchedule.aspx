<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportFlatSchedule.aspx.cs" Inherits="Web.UI.Forms.ImportFlatSchedule" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script src="Scripts/jquery-new.js"></script>
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
        function importButton(f) {
            App.beginOperation.setDisabled(false);
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
        <ext:Hidden ID="CurrentPath" runat="server" />





        <ext:Viewport id="Viewport1" runat="server" layout="CardLayout" activeindex="0">

            <Items>
                <ext:FormPanel runat="server" ID="uploadFileForm"
                    Icon="ApplicationSideList" Header="false" Layout="HBoxLayout">
                    <Items>
                        <ext:Panel runat="server" Flex="1" />
                        <ext:Panel runat="server" Flex="2">
                            <Items>
                                <ext:Panel  runat="server" Flex="1" Height="200" />
                                <ext:Label MarginSpec="0 0 0 0" runat="server" Text="<%$ Resources: Pick %>" Width="400" />
                                <ext:FileUploadField runat="server" ID="fileUpload" Width="400" ButtonText="<%$ Resources: Pick %>">
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
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>

                <ext:Panel runat="server" Layout="HBoxLayout">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>

                                <ext:Button runat="server" Icon="Delete">
                                    <DirectEvents>
                                        <Click OnEvent="Unnamed_Event"></Click>
                                    </DirectEvents>
                                </ext:Button>


                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" Flex="1" />
                        <ext:Panel runat="server" Flex="2">
                            <Items>
                                 <ext:Panel  runat="server" Flex="1" Height="200" />
                                <ext:ProgressBar ID="Progress1" runat="server" Width="300" Visible="true" />
                                <ext:Button runat="server" Text="<%$Resources:Import %>" ID="beginOperation" Disabled="true" MarginSpec="0 0 0 120">
                                    <Listeners>
                                        <Click Handler="this.setDisabled(true);" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ProcessData" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel runat="server" Layout="HBoxLayout">

                    <Items>
                        <ext:Panel runat="server" Flex="1" />
                        <ext:Panel runat="server" Flex="2" Layout="VBoxLayout">
                            <Items>
                                <ext:Panel  runat="server" Flex="1" Height="200" />
                                <ext:Panel  runat="server" Flex="2" ><Items>
                                     <ext:Label runat="server" Text="<%$Resources:ResultReady %>" />
                                    <ext:Panel runat="server" Height="20" />
                                    <ext:Button runat="server" Icon="DiskDownload" Text="<%$Resources:Download %>"  MarginSpec="0 0 0 90" >
                                    <DirectEvents>
                                        <Click OnEvent="DownloadResult" />
                                    </DirectEvents>
                                </ext:Button></Items></ext:Panel>
                               
                                
                                
                            </Items>
                        </ext:Panel>

                    </Items>
                </ext:Panel>
              </Items>
        </ext:Viewport>

                


        <ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task
                    TaskID="longactionprogress"
                    Interval="1000"
                    AutoRun="false"
                    OnStart="
                        #{Progress1}.setVisible(true);"
                    OnStop="
                        #{Progress1}.setVisible(false);">
                    <DirectEvents>
                        <Update OnEvent="RefreshProgress" />
                    </DirectEvents>
                </ext:Task>
            </Tasks>
        </ext:TaskManager>
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


