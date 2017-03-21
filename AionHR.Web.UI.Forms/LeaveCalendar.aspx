<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveCalendar.aspx.cs" Inherits="AionHR.Web.UI.Forms.LeaveCalendar" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/LeaveCalendar.js?id=0"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript">
        function setTotal(t) {
            // alert(t);
            // alert(document.getElementById("total"));

            document.getElementById("total").innerHTML = t;
            Ext.defer(function () {
                App.GridPanel1.view.refresh();
            }, 10);
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


        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App" IsPagingStore="true">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server">
                    <Fields>

                        <ext:ModelField Name="dayId" />
                        <ext:ModelField Name="employeeName" IsComplex="true" />
                        <ext:ModelField Name="branchName" />
                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="checkIn" />
                        <ext:ModelField Name="checkOut" />
                        <ext:ModelField Name="workingTime" />
                        <ext:ModelField Name="breaks" />
                        <ext:ModelField Name="OL_A" />
                        <ext:ModelField Name="OL_B" />
                        <ext:ModelField Name="OL_D" />
                        <ext:ModelField Name="OL_N" />
                        <ext:ModelField Name="OL_A_SIGN" />
                        <ext:ModelField Name="OL_B_SIGN" />
                        <ext:ModelField Name="OL_D_SIGN" />
                        <ext:ModelField Name="OL_N_SIGN" />
                        <ext:ModelField Name="netOL" />






                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>



        
                <ext:Panel runat="server">

                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Store>
                                        <ext:Store runat="server" ID="branchStore">
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
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>
                                </ext:ComboBox>

                                <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" FieldLabel="<%$ Resources:FieldDepartment%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
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
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>

                                </ext:ComboBox>
                                   <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="monthCombo" Name="month" FieldLabel="<%$ Resources:FieldMonth%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                       <Items>
                                           <ext:ListItem Text="<%$ Resources:Common,January%>" Value="1" />
                                           <ext:ListItem Text="<%$ Resources:Common,February%>" Value="2" />
                                           <ext:ListItem Text="<%$ Resources:Common,March%>" Value="3" />
                                           <ext:ListItem Text="<%$ Resources:Common,April%>" Value="4" />
                                           <ext:ListItem Text="<%$ Resources:Common,May%>" Value="5" />
                                           <ext:ListItem Text="<%$ Resources:Common,June%>" Value="6" />
                                           <ext:ListItem Text="<%$ Resources:Common,July%>" Value="7" />
                                           <ext:ListItem Text="<%$ Resources:Common,August%>" Value="8" />
                                           <ext:ListItem Text="<%$ Resources:Common,September%>" Value="9" />
                                           <ext:ListItem Text="<%$ Resources:Common,October%>" Value="10" />
                                           <ext:ListItem Text="<%$ Resources:Common,November%>" Value="11" />
                                           <ext:ListItem Text="<%$ Resources:Common,December%>" Value="12" />
                                       </Items>
                                       </ext:ComboBox>
                                  <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="yearCombo" Name="year" FieldLabel="<%$ Resources:FieldYear%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                       <Items>
                                            <ext:ListItem Text="2015" Value="2015" />
                                            <ext:ListItem Text="2016" Value="2016" />
                                            <ext:ListItem Text="2017" Value="2017" />
                                            <ext:ListItem Text="2018" Value="2018" />
                                            <ext:ListItem Text="2019" Value="2019" />
                                            <ext:ListItem Text="2020" Value="2020" />
                                           <ext:ListItem Text="2021" Value="2021" />
                                           
                                       </Items>
                                       </ext:ComboBox>
                                 <ext:Button ID="applyButton" runat="server" Text="<%$ Resources: ApplyFilter%>" >
                                     <Listeners>
                                         <Click Handler="CheckSession" />
                                     </Listeners>
                                     <DirectEvents>
                                         <Click  OnEvent="Unnamed_Event">
                                            
                                         </Click>
                                     </DirectEvents>
                                 </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Content>
                        <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server"
                            HeaderFontSize="8pt" HeaderHeight="20"
                           
                            
                            EventFontSize="11px"
                            CellDuration="1440"
                            
                           
                            EventHeight="25">
                            <Resources>
                                <DayPilot:Resource Name="Room A" Value="A" />

                            </Resources>
                        </DayPilot:DayPilotScheduler>

                    </Content>

                </ext:Panel>
      








    </form>
</body>
</html>
