<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Notes" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Notes.js?id=18"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>
    <script type="text/javascript" src="../Scripts/moment.js?id=0"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server"  />
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
        <ext:Hidden ID="CurrentEmployeeName" runat="server" />
          <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
        


        <Items>
             <ext:Button Region="East" ID="btnAdd" Disabled="true" Height="30" Width="40" MaxWidth="40"  runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                  <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <ExtraParams >
                                                <ext:Parameter Name="noteText" Value="#{newNoteText}.getValue()"   Mode="Raw" />
                                            </ExtraParams>
                                            <EventMask ShowMask="true" CustomTarget="{#{employeementHistoryGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                 <Listeners>
                     <AfterRender Handler="this.setDisabled(true);" />
                 </Listeners>
                                </ext:Button>
            <ext:Panel runat="server"  Layout="FitLayout" DefaultAnchor="100%" >
                                    <Items>
                                          <ext:TextArea runat="server" ID="newNoteText" Region="North" Width="400" DefaultAnchor="100%" >
                                              <Listeners>
                                                  <Change Handler="App.btnAdd.setDisabled(this.value=='');" />
                                              </Listeners>
                                              </ext:TextArea>
                                
                                    </Items>
                                </ext:Panel>
           
                <ext:GridPanel AutoUpdateLayout="true"
                    ID="employeementHistoryGrid" Collapsible="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: EHGridTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical" Flex="1"
                    Border="false" 
                    Icon="User" DefaultAnchor="100%"    HideHeaders="true"
                    ColumnLines="false" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="employeementHistoryStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="employeementHistory_RefreshData"
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
                                        <ext:ModelField Name="note" />
                                        <ext:ModelField Name="date"  />
                                        <ext:ModelField Name="userName"  />

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false ">
                            
                            <Items>
                                
                              
                              
                               

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server"  SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column CellCls="cellLink" ID="ColEHName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEHStatus%>" DataIndex="note" Flex="7" Hideable="false">
                                <Renderer Handler="var s = moment(record.data['date']);  return '<b>'+ record.data['userName']+'</b>- '+ s.calendar()+'<br />'+ record.data['note'];">
                                </Renderer>
                                <Editor>
                                    
                                    <ext:TextArea runat="server" ID="notesEditor" name="note" >
                                        
                                    </ext:TextArea>
                                </Editor>
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
                                ID="ColEHDelete" Flex="1" Visible="true"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="70"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="if(App.EmployeeTerminated.value=='0') return editRender() + '&nbsp;&nbsp;'+ deleteRender();" >
                                    </Renderer>

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




                        </Columns>
                    </ColumnModel>
                    <Plugins>
                        <ext:RowEditing runat="server" SaveHandler="validateSave"  SaveBtnText="<%$ Resources:Common , Save %>" CancelBtnText="<%$ Resources:Common , Cancel %>" />
                        
                    </Plugins>
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
                        <RowBodyDblClick Handler="App.employeementHistoryGrid.editingPlugin.cancelEdit();" />
                        <RowDblClick Handler="App.employeementHistoryGrid.editingPlugin.cancelEdit();" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="index" Value="rowIndex" Mode="Raw" />
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
        

    </form>
</body>
</html>
