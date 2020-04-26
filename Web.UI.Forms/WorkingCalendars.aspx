<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkingCalendars.aspx.cs" Inherits="Web.UI.Forms.WorkingCalendars" %>



<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />

    <script type="text/javascript" src="Scripts/common.js?id=2"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script  type="text/javascript"  src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="CSS/Calendars.css?id=1" />
    <script type="text/javascript" src="Scripts/Calendars.js?id=3"></script>

    <script type="text/javascript">

        function SetVisible()
        {
            App.setDefaultBtn.setHidden(false);
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
        <ext:Hidden ID="CurrentCalendar" runat="server" />
        <ext:Hidden ID="dayId" runat="server" />
        <ext:Hidden ID="CurrentYear" runat="server" />
        <ext:Hidden ID="CurrentCalenderLabel" runat="server"/>

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
                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="name" />






                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
                <ext:DataSorter Property="name" Direction="ASC" />
                <ext:DataSorter Property="reference" Direction="ASC" />
            </Sorters>
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

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" Flex="1" />

                            <ext:Column CellCls="cellLink" Sortable="true" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                                <Renderer Handler="return  record.data['name'];">
                                </Renderer>
                            </ext:Column>



                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colDelete" Visible="false"
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
                                ID="colDetails"
                                Text="<%$ Resources:EditYears %>"
                                Hideable="false"
                                Width="110"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;'+attachRender()+'&nbsp;&nbsp;' +deleteRender();" />
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
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="calenderNamePar" Value ="record.data['name']" Mode="Raw"/>
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

                <ext:GridPanel runat="server" Title="<%$ Resources: CalendarYearsTitle %>" Header="false" ID="calendarYears">
                    <View>
                        <ext:GridView ID="GridView2" runat="server" />
                    </View>
                    <SelectionModel>
                        <ext:RowSelectionModel runat="server"></ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPYear">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="CalenderYear" Value="record.data['year']" Mode="Raw"/>
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <Store>
                        <ext:Store ID="scheduleStore" runat="server">
                            <Model>
                                <ext:Model runat="server" IDProperty="year">
                                    <Fields>
                                        <ext:ModelField Name="year" />
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
                                <ext:Button ID="Button5" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="AddNewDay">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                               <ext:ToolbarSeparator runat="server" />
                                <ext:Label runat="server"  ID="calendarName" Disabled="false" Visible="true" ></ext:Label>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <ColumnModel>
                        <Columns>
                            <ext:Column runat="server" ID="colCaId" Visible="false" DataIndex="caId">
                            </ext:Column>
                            <ext:Column runat="server" ID="colYear" Text="<%$ Resources: Year %>" DataIndex="year" Flex="2" />
                            <ext:Column runat="server"
                                ID="colYearDetails"
                                Text="<%$ Resources:EditCalendar %>"
                                Hideable="false"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="attachRender" />
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>

                <ext:Panel runat="server" Title="<%$ Resources: CalendarDays %>" Header="false">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar4" runat="server" ClassicButtonStyle="false">
                            <Items>
                                
                                <ext:Button ID="Button7" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
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
                                <ext:Button ID="legendsButton" runat="server" Text="<%$ Resources:Legends %>" Icon="Help">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="viewLegent_click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="patternButton" runat="server" Text="<%$ Resources:ApplyPattern %>" Icon="Help">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="selectPattern_click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                 <ext:Button ID="alternateButton" runat="server" Text="<%$ Resources:AlternateDaysWindow %>" Icon="Help">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="alternateDayWidnow_click">
                                        </Click>
                                    </DirectEvents>

                                </ext:Button>
                                <ext:Button ID="Button17" runat="server" Text="<%$ Resources:AlternateWeeksWindow %>" Icon="Help">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="alternateWeekWidnow_click">
                                        </Click>
                                    </DirectEvents>

                                </ext:Button>
                                  <ext:Button ID="Button12" runat="server" Text="<%$ Resources:ImportCalendar %>" Icon="DatabaseCopy">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="openCopyCalendar">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                
                                  <ext:label ID="lbCalenderName" runat="server"/>
                                <ext:ToolbarSeparator runat="server" />
                                  <ext:Label ID="lbCalender"  runat="server"/>
                              
                              


                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <Content>
                        <div style="margin-top: 0px; margin: auto; width: 90%; height: 90%">
                            <table runat="server" id="tbCalendar" cellpadding="5" cellspacing="0" clientidmode="Static">
                                <tr>
                                    <th>
                                        <asp:Label runat="server" Text="<%$ Resources:MonthDay %>" />
                                    </th>
                                    <th>01
                                    </th>
                                    <th>02
                                    </th>
                                    <th>03
                                    </th>
                                    <th>04
                                    </th>
                                    <th>05
                                    </th>
                                    <th>06
                                    </th>
                                    <th>07
                                    </th>
                                    <th>08
                                    </th>
                                    <th>09
                                    </th>
                                    <th>10
                                    </th>
                                    <th>11
                                    </th>
                                    <th>12
                                    </th>
                                    <th>13
                                    </th>
                                    <th>14
                                    </th>
                                    <th>15
                                    </th>
                                    <th>16
                                    </th>
                                    <th>17
                                    </th>
                                    <th>18
                                    </th>
                                    <th>19
                                    </th>
                                    <th>20
                                    </th>
                                    <th>21
                                    </th>
                                    <th>22
                                    </th>
                                    <th>23
                                    </th>
                                    <th>24
                                    </th>
                                    <th>25
                                    </th>
                                    <th>26
                                    </th>
                                    <th>27
                                    </th>
                                    <th>28
                                    </th>
                                    <th>29
                                    </th>
                                    <th>30
                                    </th>
                                    <th>31
                                    </th>
                                </tr>
                                <tr>
                                    <th>01
                                    </th>
                                    <td id="td0101">
                                        <span class="hidden">0101</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                    <td id="td0102">
                                        <span class="hidden">0102</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                    <td id="td0103">
                                        <span class="hidden">0103</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                    <td id="td0104">
                                        <span class="hidden">0104</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0105">
                                        <span class="hidden">0105</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0106">
                                        <span class="hidden">0106</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0107">
                                        <span class="hidden">0107</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0108">
                                        <span class="hidden">0108</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0109">
                                        <span class="hidden">0109</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0110">
                                        <span class="hidden">0110</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0111">
                                        <span class="hidden">0111</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0112">
                                        <span class="hidden">0112</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0113">
                                        <span class="hidden">0113</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0114">
                                        <span class="hidden">0114</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0115">
                                        <span class="hidden">0115</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0116">
                                        <span class="hidden">0116</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0117">
                                        <span class="hidden">0117</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0118">
                                        <span class="hidden">0118</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0119">
                                        <span class="hidden">0119</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0120">
                                        <span class="hidden">0120</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                    <td id="td0121">
                                        <span class="hidden">0121</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0122">
                                        <span class="hidden">0122</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0123">
                                        <span class="hidden">0123</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0124">
                                        <span class="hidden">0124</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0125">
                                        <span class="hidden">0125</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0126">
                                        <span class="hidden">0126</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0127">
                                        <span class="hidden">0127</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0128">
                                        <span class="hidden">0128</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0129">
                                        <span class="hidden">0129</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0130">
                                        <span class="hidden">0130</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0131">
                                        <span class="hidden">0131</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th>02
                                    </th>
                                    <td id="td0201">
                                        <span class="hidden">0201</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                    <td id="td0202">
                                        <span class="hidden">0202</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                    <td id="td0203">
                                        <span class="hidden">0203</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                    <td id="td0204">
                                        <span class="hidden">0204</span> <span class="scheduleid"></span><span class="daytypeid"></span>
                                    </td>
                                    <td id="td0205">
                                        <span class="hidden">0205</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0206">
                                        <span class="hidden">0206</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0207">
                                        <span class="hidden">0207</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0208">
                                        <span class="hidden">0208</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0209">
                                        <span class="hidden">0209</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0210">
                                        <span class="hidden">0210</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0211">
                                        <span class="hidden">0211</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0212">
                                        <span class="hidden">0212</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0213">
                                        <span class="hidden">0213</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0214">
                                        <span class="hidden">0214</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0215">
                                        <span class="hidden">0215</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0216">
                                        <span class="hidden">0216</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0217">
                                        <span class="hidden">0217</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0218">
                                        <span class="hidden">0218</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0219">
                                        <span class="hidden">0219</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0220">
                                        <span class="hidden">0220</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0221">
                                        <span class="hidden">0221</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0222">
                                        <span class="hidden">0222</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0223">
                                        <span class="hidden">0223</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0224">
                                        <span class="hidden">0224</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0225">
                                        <span class="hidden">0225</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0226">
                                        <span class="hidden">0226</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0227">
                                        <span class="hidden">0227</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0228">
                                        <span class="hidden">0228</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0229">
                                        <span class="hidden">0229</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td class='notexist'>X
                                    </td>
                                    <td class='notexist'>X
                                    </td>
                                </tr>
                                <tr>
                                    <th>03
                                    </th>
                                    <td id="td0301">
                                        <span class="hidden">0301</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0302">
                                        <span class="hidden">0302</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0303">
                                        <span class="hidden">0303</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0304">
                                        <span class="hidden">0304</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0305">
                                        <span class="hidden">0305</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0306">
                                        <span class="hidden">0306</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0307">
                                        <span class="hidden">0307</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0308">
                                        <span class="hidden">0308</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0309">
                                        <span class="hidden">0309</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0310">
                                        <span class="hidden">0310</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0311">
                                        <span class="hidden">0311</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0312">
                                        <span class="hidden">0312</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0313">
                                        <span class="hidden">0313</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0314">
                                        <span class="hidden">0314</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0315">
                                        <span class="hidden">0315</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0316">
                                        <span class="hidden">0316</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0317">
                                        <span class="hidden">0317</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0318">
                                        <span class="hidden">0318</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0319">
                                        <span class="hidden">0319</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0320">
                                        <span class="hidden">0320</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0321">
                                        <span class="hidden">0321</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0322">
                                        <span class="hidden">0322</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0323">
                                        <span class="hidden">0323</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0324">
                                        <span class="hidden">0324</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0325">
                                        <span class="hidden">0325</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0326">
                                        <span class="hidden">0326</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0327">
                                        <span class="hidden">0327</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0328">
                                        <span class="hidden">0328</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0329">
                                        <span class="hidden">0329</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0330">
                                        <span class="hidden">0330</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0331">
                                        <span class="hidden">0331</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th>04
                                    </th>
                                    <td id="td0401">
                                        <span class="hidden">0401</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0402">
                                        <span class="hidden">0402</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0403">
                                        <span class="hidden">0403</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0404">
                                        <span class="hidden">0404</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0405">
                                        <span class="hidden">0405</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0406">
                                        <span class="hidden">0406</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0407">
                                        <span class="hidden">0407</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0408">
                                        <span class="hidden">0408</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0409">
                                        <span class="hidden">0409</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0410">
                                        <span class="hidden">0410</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0411">
                                        <span class="hidden">0411</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0412">
                                        <span class="hidden">0412</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0413">
                                        <span class="hidden">0413</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0414">
                                        <span class="hidden">0414</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0415">
                                        <span class="hidden">0415</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0416">
                                        <span class="hidden">0416</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0417">
                                        <span class="hidden">0417</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0418">
                                        <span class="hidden">0418</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0419">
                                        <span class="hidden">0419</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0420">
                                        <span class="hidden">0420</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0421">
                                        <span class="hidden">0421</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0422">
                                        <span class="hidden">0422</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0423">
                                        <span class="hidden">0423</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0424">
                                        <span class="hidden">0424</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0425">
                                        <span class="hidden">0425</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0426">
                                        <span class="hidden">0426</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0427">
                                        <span class="hidden">0427</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0428">
                                        <span class="hidden">0428</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0429">
                                        <span class="hidden">0429</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0430">
                                        <span class="hidden">0430</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td class='notexist'>X
                                    </td>
                                </tr>
                                <tr>
                                    <th>05
                                    </th>
                                    <td id="td0501">
                                        <span class="hidden">0501</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0502">
                                        <span class="hidden">0502</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0503">
                                        <span class="hidden">0503</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0504">
                                        <span class="hidden">0504</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0505">
                                        <span class="hidden">0505</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0506">
                                        <span class="hidden">0506</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0507">
                                        <span class="hidden">0507</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0508">
                                        <span class="hidden">0508</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0509">
                                        <span class="hidden">0509</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0510">
                                        <span class="hidden">0510</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0511">
                                        <span class="hidden">0511</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0512">
                                        <span class="hidden">0512</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0513">
                                        <span class="hidden">0513</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0514">
                                        <span class="hidden">0514</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0515">
                                        <span class="hidden">0515</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0516">
                                        <span class="hidden">0516</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0517">
                                        <span class="hidden">0517</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0518">
                                        <span class="hidden">0518</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0519">
                                        <span class="hidden">0519</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0520">
                                        <span class="hidden">0520</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0521">
                                        <span class="hidden">0521</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0522">
                                        <span class="hidden">0522</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0523">
                                        <span class="hidden">0523</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0524">
                                        <span class="hidden">0524</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0525">
                                        <span class="hidden">0525</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0526">
                                        <span class="hidden">0526</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0527">
                                        <span class="hidden">0527</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0528">
                                        <span class="hidden">0528</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0529">
                                        <span class="hidden">0529</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0530">
                                        <span class="hidden">0530</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0531">
                                        <span class="hidden">0531</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th>06
                                    </th>
                                    <td id="td0601">
                                        <span class="hidden">0601</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0602">
                                        <span class="hidden">0602</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0603">
                                        <span class="hidden">0603</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0604">
                                        <span class="hidden">0604</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0605">
                                        <span class="hidden">0605</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0606">
                                        <span class="hidden">0606</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0607">
                                        <span class="hidden">0607</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0608">
                                        <span class="hidden">0608</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0609">
                                        <span class="hidden">0609</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0610">
                                        <span class="hidden">0610</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0611">
                                        <span class="hidden">0611</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0612">
                                        <span class="hidden">0612</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0613">
                                        <span class="hidden">0613</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0614">
                                        <span class="hidden">0614</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0615">
                                        <span class="hidden">0615</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0616">
                                        <span class="hidden">0616</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0617">
                                        <span class="hidden">0617</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0618">
                                        <span class="hidden">0618</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0619">
                                        <span class="hidden">0619</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0620">
                                        <span class="hidden">0620</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0621">
                                        <span class="hidden">0621</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0622">
                                        <span class="hidden">0622</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0623">
                                        <span class="hidden">0623</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0624">
                                        <span class="hidden">0624</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0625">
                                        <span class="hidden">0625</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0626">
                                        <span class="hidden">0626</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0627">
                                        <span class="hidden">0627</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0628">
                                        <span class="hidden">0628</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0629">
                                        <span class="hidden">0629</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0630">
                                        <span class="hidden">0630</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td class='notexist'>X
                                    </td>
                                </tr>
                                <tr>
                                    <th>07
                                    </th>
                                    <td id="td0701">
                                        <span class="hidden">0701</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0702">
                                        <span class="hidden">0702</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0703">
                                        <span class="hidden">0703</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0704">
                                        <span class="hidden">0704</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0705">
                                        <span class="hidden">0705</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0706">
                                        <span class="hidden">0706</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0707">
                                        <span class="hidden">0707</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0708">
                                        <span class="hidden">0708</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0709">
                                        <span class="hidden">0709</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0710">
                                        <span class="hidden">0710</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0711">
                                        <span class="hidden">0711</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0712">
                                        <span class="hidden">0712</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0713">
                                        <span class="hidden">0713</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0714">
                                        <span class="hidden">0714</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0715">
                                        <span class="hidden">0715</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0716">
                                        <span class="hidden">0716</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0717">
                                        <span class="hidden">0717</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0718">
                                        <span class="hidden">0718</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0719">
                                        <span class="hidden">0719</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0720">
                                        <span class="hidden">0720</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0721">
                                        <span class="hidden">0721</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0722">
                                        <span class="hidden">0722</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0723">
                                        <span class="hidden">0723</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0724">
                                        <span class="hidden">0724</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0725">
                                        <span class="hidden">0725</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0726">
                                        <span class="hidden">0726</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0727">
                                        <span class="hidden">0727</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0728">
                                        <span class="hidden">0728</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0729">
                                        <span class="hidden">0729</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0730">
                                        <span class="hidden">0730</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0731">
                                        <span class="hidden">0731</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th>08
                                    </th>
                                    <td id="td0801">
                                        <span class="hidden">0801</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0802">
                                        <span class="hidden">0802</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0803">
                                        <span class="hidden">0803</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0804">
                                        <span class="hidden">0804</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0805">
                                        <span class="hidden">0805</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0806">
                                        <span class="hidden">0806</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0807">
                                        <span class="hidden">0807</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0808">
                                        <span class="hidden">0808</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0809">
                                        <span class="hidden">0809</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0810">
                                        <span class="hidden">0810</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0811">
                                        <span class="hidden">0811</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0812">
                                        <span class="hidden">0812</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0813">
                                        <span class="hidden">0813</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0814">
                                        <span class="hidden">0814</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0815">
                                        <span class="hidden">0815</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0816">
                                        <span class="hidden">0816</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0817">
                                        <span class="hidden">0817</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0818">
                                        <span class="hidden">0818</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0819">
                                        <span class="hidden">0819</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0820">
                                        <span class="hidden">0820</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0821">
                                        <span class="hidden">0821</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0822">
                                        <span class="hidden">0822</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0823">
                                        <span class="hidden">0823</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0824">
                                        <span class="hidden">0824</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0825">
                                        <span class="hidden">0825</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0826">
                                        <span class="hidden">0826</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0827">
                                        <span class="hidden">0827</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0828">
                                        <span class="hidden">0828</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0829">
                                        <span class="hidden">0829</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0830">
                                        <span class="hidden">0830</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0831">
                                        <span class="hidden">0831</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th>09
                                    </th>
                                    <td id="td0901">
                                        <span class="hidden">0901</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0902">
                                        <span class="hidden">0902</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0903">
                                        <span class="hidden">0903</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0904">
                                        <span class="hidden">0904</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0905">
                                        <span class="hidden">0905</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0906">
                                        <span class="hidden">0906</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0907">
                                        <span class="hidden">0907</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0908">
                                        <span class="hidden">0908</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0909">
                                        <span class="hidden">0909</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0910">
                                        <span class="hidden">0910</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0911">
                                        <span class="hidden">0911</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0912">
                                        <span class="hidden">0912</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0913">
                                        <span class="hidden">0913</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0914">
                                        <span class="hidden">0914</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0915">
                                        <span class="hidden">0915</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0916">
                                        <span class="hidden">0916</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0917">
                                        <span class="hidden">0917</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0918">
                                        <span class="hidden">0918</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0919">
                                        <span class="hidden">0919</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0920">
                                        <span class="hidden">0920</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0921">
                                        <span class="hidden">0921</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0922">
                                        <span class="hidden">0922</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0923">
                                        <span class="hidden">0923</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0924">
                                        <span class="hidden">0924</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0925">
                                        <span class="hidden">0925</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0926">
                                        <span class="hidden">0926</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0927">
                                        <span class="hidden">0927</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0928">
                                        <span class="hidden">0928</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0929">
                                        <span class="hidden">0929</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td0930">
                                        <span class="hidden">0930</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td class='notexist'>X
                                    </td>
                                </tr>
                                <tr>
                                    <th>10
                                    </th>
                                    <td id="td1001">
                                        <span class="hidden">1001</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1002">
                                        <span class="hidden">1002</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1003">
                                        <span class="hidden">1003</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1004">
                                        <span class="hidden">1004</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1005">
                                        <span class="hidden">1005</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1006">
                                        <span class="hidden">1006</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1007">
                                        <span class="hidden">1007</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1008">
                                        <span class="hidden">1008</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1009">
                                        <span class="hidden">1009</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1010">
                                        <span class="hidden">1010</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1011">
                                        <span class="hidden">1011</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1012">
                                        <span class="hidden">1012</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1013">
                                        <span class="hidden">1013</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1014">
                                        <span class="hidden">1014</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1015">
                                        <span class="hidden">1015</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1016">
                                        <span class="hidden">1016</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1017">
                                        <span class="hidden">1017</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1018">
                                        <span class="hidden">1018</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1019">
                                        <span class="hidden">1019</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1020">
                                        <span class="hidden">1020</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1021">
                                        <span class="hidden">1021</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1022">
                                        <span class="hidden">1022</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1023">
                                        <span class="hidden">1023</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1024">
                                        <span class="hidden">1024</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1025">
                                        <span class="hidden">1025</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1026">
                                        <span class="hidden">1026</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1027">
                                        <span class="hidden">1027</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1028">
                                        <span class="hidden">1028</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1029">
                                        <span class="hidden">1029</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1030">
                                        <span class="hidden">1030</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1031">
                                        <span class="hidden">1030</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th>11
                                    </th>
                                    <td id="td1101">
                                        <span class="hidden">1101</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1102">
                                        <span class="hidden">1102</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1103">
                                        <span class="hidden">1103</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1104">
                                        <span class="hidden">1104</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1105">
                                        <span class="hidden">1105</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1106">
                                        <span class="hidden">1106</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1107">
                                        <span class="hidden">1107</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1108">
                                        <span class="hidden">1108</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1109">
                                        <span class="hidden">1109</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1110">
                                        <span class="hidden">1110</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1111">
                                        <span class="hidden">1111</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1112">
                                        <span class="hidden">1112</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1113">
                                        <span class="hidden">1113</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1114">
                                        <span class="hidden">1114</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1115">
                                        <span class="hidden">1115</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1116">
                                        <span class="hidden">1116</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1117">
                                        <span class="hidden">1117</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1118">
                                        <span class="hidden">1118</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1119">
                                        <span class="hidden">1119</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1120">
                                        <span class="hidden">1120</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1121">
                                        <span class="hidden">1121</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1122">
                                        <span class="hidden">1122</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1123">
                                        <span class="hidden">1123</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1124">
                                        <span class="hidden">1124</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1125">
                                        <span class="hidden">1125</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1126">
                                        <span class="hidden">1126</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1127">
                                        <span class="hidden">1127</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1128">
                                        <span class="hidden">1128</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1129">
                                        <span class="hidden">1129</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1130">
                                        <span class="hidden">1130</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td class='notexist'>X
                                    </td>
                                </tr>
                                <tr>
                                    <th>12
                                    </th>
                                    <td id="td1201">
                                        <span class="hidden">1201</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1202">
                                        <span class="hidden">1202</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1203">
                                        <span class="hidden">1203</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1204">
                                        <span class="hidden">1204</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1205">
                                        <span class="hidden">1205</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1206">
                                        <span class="hidden">1206</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1207">
                                        <span class="hidden">1207</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1208">
                                        <span class="hidden">1208</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1209">
                                        <span class="hidden">1209</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1210">
                                        <span class="hidden">1210</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1211">
                                        <span class="hidden">1211</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1212">
                                        <span class="hidden">1212</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1213">
                                        <span class="hidden">1213</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1214">
                                        <span class="hidden">1214</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1215">
                                        <span class="hidden">1215</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1216">
                                        <span class="hidden">1216</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1217">
                                        <span class="hidden">1217</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1218">
                                        <span class="hidden">1218</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1219">
                                        <span class="hidden">1219</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1220">
                                        <span class="hidden">1220</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1221">
                                        <span class="hidden">1221</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1222">
                                        <span class="hidden">1222</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1223">
                                        <span class="hidden">1223</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1224">
                                        <span class="hidden">1224</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1225">
                                        <span class="hidden">1225</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1226">
                                        <span class="hidden">1226</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1227">
                                        <span class="hidden">1227</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1228">
                                        <span class="hidden">1228</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1229">
                                        <span class="hidden">1229</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1230">
                                        <span class="hidden">1230</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                    <td id="td1231">
                                        <span class="hidden">1231</span><span class="scheduleid"></span> <span class="daytypeid"></span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </Content>

                </ext:Panel>
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="EditCalendarWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditCalendarWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">

                    <Items>
                        <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" Hidden="true" runat="server" Disabled="true" DataIndex="recordId" />
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldCalendarName%>" DataIndex="name" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>" />


                            </Items>

                        </ext:FormPanel>


                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditCalendarWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="calendar" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />

                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
                   <ext:Button ID="setDefaultBtn" runat="server" Text="<%$ Resources:SetDefault %>"  Hidden="true">
                       <Listeners  >
                           <Click Handler="CheckSession();"></Click>
                           </Listeners>
               <DirectEvents>
                  
                   <Click OnEvent="SetDefaultClick" >
                         <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                        </ExtraParams>
                       </Click>
               </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>

        <ext:Window
            ID="EditYearDetails"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:CalendarYearFormTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">

                    <Items>
                        <ext:FormPanel DefaultButton="Button3"
                            ID="calendarYearForm"
                            runat="server"
                            Title="<%$ Resources:CalendarYearFormTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>

                                <ext:TextField ID="fieldCaId" Hidden="true" runat="server" Disabled="true" DataIndex="scId" />
                                <ext:NumberField runat="server" FieldLabel="<%$ Resources:Year %>" ID="year" Name="year" MinValue="1900" />

                            </Items>

                        </ext:FormPanel>


                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{calendarYearForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveCalendarYear" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{calendarYearForm}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="caId" Value="#{fieldCaId}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="year" Value="#{calendarYearForm}.getForm().getValues()" Mode="Raw" Encode="true" />

                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button4" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

        <ext:Window
            ID="dayConfigWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:DayConfigTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel DefaultButton="Button2"
                    ID="dayConfigForm"
                    runat="server"
                    Title="<%$ Resources:DayConfigTitle %>"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>

                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="scId" DisplayField="name" AllowBlank="true" Name="scId" ValueField="recordId" FieldLabel="<%$ Resources:Schedule %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="schedulesStore">
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
                        </ext:ComboBox>
                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" DisplayField="name" ValueField="recordId" AllowBlank="false" Name="dayTypeId" ID="dayTypeId" FieldLabel="<%$ Resources:DayType %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="dayTypesStore">
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
                        </ext:ComboBox>
                    </Items>

                </ext:FormPanel>


            </Items>

            <Buttons>
                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{dayConfigForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveDayConfig" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{dayConfigForm}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="caId" Value="#{fieldCaId}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="day" Value="#{dayConfigForm}.getForm().getValues()" Mode="Raw" Encode="true" />

                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button6" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Window
            ID="legendsWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:LegendsWindow %>"
            Width="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:GridPanel ID="GridPanel2" runat="server" Width="200">
                    <Store>
                        <ext:Store runat="server" ID="colorsStore">
                            <Model>
                                <ext:Model runat="server" IDProperty="recordId">
                                    <Fields>
                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="name" />
                                        <ext:ModelField Name="color" />


                                    </Fields>
                                </ext:Model>

                            </Model>
                        </ext:Store>

                    </Store>
                    <ColumnModel>
                        <Columns>
                            <ext:Column runat="server" DataIndex="recordId" Visible="false" />
                            <ext:Column runat="server" DataIndex="name" Text="<%$ Resources:Common, DayTypes %>" Flex="1" />
                            <ext:ComponentColumn runat="server" Text="Color" DataIndex="<%$ Resources: Color%>" Flex="1">
                                <Component>
                                    <ext:ColorField runat="server" ReadOnly="true" />
                                </Component>
                                <Listeners>
                                    <Bind Handler="cmp.setValue(record.get('color'));" />
                                </Listeners>
                            </ext:ComponentColumn>
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>

        <ext:Window
            ID="patternWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:PatternWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel DefaultButton="Button8"
                    ID="patternFormPanel"
                    runat="server"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        <ext:DateField runat="server" AllowBlank="false" ID="dateFrom" Name="dateFrom" FieldLabel="<%$ Resources:From %>" />
                        <ext:DateField runat="server" AllowBlank="false" ID="dateTo" Name="dateTo" FieldLabel="<%$ Resources:To %>" />
                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" Name="scId" ID="scIdCombo" DisplayField="name" ValueField="recordId" FieldLabel="<%$ Resources:Schedule %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="patternScheduleStore">
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
                        </ext:ComboBox>
                        <ext:Checkbox runat="server" Name="applyScId" DataIndex="applyScId" ID="applyScId" InputValue="True" FieldLabel="<%$ Resources:ApplySchedule %>" />
                    </Items>

                </ext:FormPanel>


            </Items>

            <Buttons>
                <ext:Button ID="Button8" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{patternFormPanel}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SavePattern" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{patternFormPanel}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="caId" Value="#{fieldCaId}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="pattern" Value="#{patternFormPanel}.getForm().getValues()" Mode="Raw" Encode="true" />

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
            ID="alternateDaysWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:AlternateDaysWindow %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel DefaultButton="Button8"
                    ID="alternateDaysForm"
                    runat="server"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        
                        <ext:DateField runat="server" AllowBlank="false" ID="startDate" Name="dateFrom" FieldLabel="<%$ Resources:From %>" />
                        <ext:DateField runat="server" AllowBlank="false" ID="endDate" Name="dateTo" FieldLabel="<%$ Resources:To %>" />
                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" Name="dayTypeId" ID="dtId" DisplayField="name" ValueField="recordId" FieldLabel="<%$ Resources:DayType %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="alternateDTStore">
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
                        </ext:ComboBox>
                          <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local"   ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: DOW %>" Name="dow" runat="server" ID="dow">
                            <Items>
                                <ext:ListItem Text="<%$ Resources:Common, MondayText %>" Value="1" />
                                <ext:ListItem Text="<%$ Resources:Common, TuesdayText %>" Value="2" />
                                <ext:ListItem Text="<%$ Resources:Common, WednesdayText %>" Value="3" />
                                <ext:ListItem Text="<%$ Resources:Common, ThursdayText %>" Value="4" />
                                <ext:ListItem Text="<%$ Resources:Common, FridayText %>" Value="5" />
                                <ext:ListItem Text="<%$ Resources:Common, SaturdayText %>" Value="6" />
                                <ext:ListItem Text="<%$ Resources:Common, SundayText %>" Value="7" />
                            </Items>
                        </ext:ComboBox>
                    </Items>

                </ext:FormPanel>


            </Items>

            <Buttons>
                <ext:Button ID="Button10" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{alternateDaysForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveDayAlternation" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{alternateDaysWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="caId" Value="#{fieldCaId}.getValue()" Mode="Raw" />
                                
                                <ext:Parameter Name="alt" Value="#{alternateDaysForm}.getForm().getValues()" Mode="Raw" Encode="true" />

                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button11" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

        <ext:Window
            ID="copyCalendarWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:ImportCalendar %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel DefaultButton="Button13"
                    ID="copyCalendarForm"
                    runat="server"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>

                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="sourceCal" DisplayField="name" AllowBlank="false"  ValueField="recordId" FieldLabel="<%$ Resources:Calendar %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="calStore">
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
                        </ext:ComboBox>
                       
                    </Items>

                </ext:FormPanel>


            </Items>

            <Buttons>
                <ext:Button ID="Button13" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{copyCalendarForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="CopyCalendar" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{copyCalendarWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="caId" Value="#{sourceCal}.getValue()" Mode="Raw" />


                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button14" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

               <ext:Window
            ID="alternateWeekWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:AlternateWeeksWindow %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel DefaultButton="Button15"
                    ID="alternateWeekForm"
                    runat="server"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5"
                     Title="<%$ Resources:AlternateWeeksWindow %>" >
                    <Items>
                        
                        <ext:DateField runat="server" AllowBlank="false" ID="DateField1" Name="dateFrom" FieldLabel="<%$ Resources:From %>" />
                        <ext:DateField runat="server" AllowBlank="false" ID="DateField2" Name="dateTo" FieldLabel="<%$ Resources:To %>" />
                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" Name="sc1Id" ID="firstSched" DisplayField="name" ValueField="recordId" FieldLabel="<%$ Resources:Schedule1 %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="sc1Store">
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
                        </ext:ComboBox>
                           <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" Name="sc2Id" ID="secondSched" DisplayField="name" ValueField="recordId" FieldLabel="<%$ Resources:Schedule2 %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="sc2Store">
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
                        </ext:ComboBox>
                    </Items>

                </ext:FormPanel>


            </Items>

            <Buttons>
                <ext:Button ID="Button15" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{alternateWeekForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveWeekAlternation" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{alternateWeekForm}.body}" />
                            <ExtraParams>
                                
                                <ext:Parameter Name="alt" Value="#{alternateWeekForm}.getForm().getValues()" Mode="Raw" Encode="true" />

                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button16" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>


