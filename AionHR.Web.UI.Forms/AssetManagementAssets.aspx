<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssetManagementAssets.aspx.cs" Inherits="AionHR.Web.UI.Forms.AssetManagementAssets" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Nationalities.js?id=1"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" >
        function hideWindow() {
            App.EditRecordWindow.close();
        }
        function setUrlAfterNewAsset() {
            App.Panel8.loader.url = 'AssetPropertyExplorer.aspx?_assetId='+App.currentAsset.value+'&_catId='+App.categoryId.value;
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
        <ext:Hidden ID="currentAsset"  runat="server"/>
        <ext:Hidden ID="currentCat"  runat="server"/>
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
                        <ext:ModelField Name="name" />
                        <ext:ModelField Name="categoryName" />
                        <ext:ModelField Name="supplierName" />
                        <ext:ModelField Name="employeeName" ServerMapping="employeeName.fullName" />
                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="serialNo" />
                        <ext:ModelField Name="statusName" />
                        <ext:ModelField Name="assetRef" />

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
                                        <Click Handler="CheckSession(); App.currentAsset.value='0'; App.Panel8.loader.url='AssetPropertyExplorer.aspx?_assetId=0';" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>

                                        <uc:AssetCategoryControl runat="server" ID="categoryIdFilter" Layout="Fit" />
                                    </Content>
                                </ext:Container>
                                <ext:ToolbarSeparator runat="server" />
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>

                                        <uc:employeeCombo runat="server" ID="employeeFilter" />
                                    </Content>
                                </ext:Container>
                                <ext:ToolbarSeparator runat="server" />
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>

                                        <uc:SupplierControl runat="server" ID="supplierIdFilter" Layout="Auto" />
                                    </Content>
                                </ext:Container>
                                <ext:ToolbarSeparator runat="server" />
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>

                                        <uc:jobInfo runat="server" ID="jobInfo1" EnableBranch="true" EnableDepartment="true" EnablePosition="true" EnableDivision="false" />
                                    </Content>
                                </ext:Container>
                                <ext:ToolbarSeparator runat="server" />
                                <ext:Button runat="server" Text="<%$Resources:Common, Go %>">
                                    <Listeners>
                                        <Click Handler=" App.Store1.reload();" />
                                    </Listeners>
                                </ext:Button>



                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>



                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column CellCls="cellLink" ID="ColAssetRef" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAssetRef%>" DataIndex="assetRef" Flex="1" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColEmployeeName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee%>" DataIndex="employeeName" Flex="2" Hideable="false" />

                            <ext:Column CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="1" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColCategory" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCategory%>" DataIndex="categoryName" Flex="1" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColSupplierName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSupplier%>" DataIndex="supplierName" Flex="1" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColSerialNo" MenuDisabled="true" runat="server" Text="<%$ Resources:FieldSerialNo%>" DataIndex="serialNo" Flex="1" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColDepartment" MenuDisabled="true" runat="server" Text="<%$ Resources:FieldDepartment%>" DataIndex="department" Flex="1" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColStatusName" MenuDisabled="true" runat="server" Text="<%$ Resources:CurrentStatus%>" DataIndex="statusName" Flex="1" Hideable="false" />

                            <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

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
                        <CellClick Handler="App.currentAsset.value=record.getId();App.Panel8.loader.url = 'AssetPropertyExplorer.aspx?_assetId='+record.getId();" />
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
            Width="600"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="FitLayout" Resizable="true">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Layout="FitLayout">
                    <Items>


                        <ext:FormPanel
                            ID="BasicInfoTab" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" Layout="HBoxLayout" BodyPadding="5">

                            <Items>
                                <ext:Panel runat="server" MarginSpec="0 20 0 0" ID="left">
                                    <Items>

                                        <ext:TextField ID="recordId" Hidden="true" runat="server" Disabled="true" Name="recordId" />
                                         <ext:TextField ID="assetRef" runat="server" FieldLabel="<%$ Resources:FieldAssetRef%>" Name="assetRef"  >
                                      <Validator Handler="return !isNaN(this.value);" ></Validator>
                                   </ext:TextField>
                                        <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name" AllowBlank="false" />
                                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="categoryId" FieldLabel="<%$ Resources:FieldCategory%>">

                                            <Store>
                                                <ext:Store runat="server" ID="categoryIdStore">
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
                                            <Change Handler="App.Panel8.loader.url='AssetPropertyExplorer.aspx?_assetId='+App.currentAsset.value+'&_catId='+this.value;" />
                                        </Listeners>

                                        </ext:ComboBox>
                                        <ext:TextField ID="serialNo" runat="server" FieldLabel="<%$ Resources:FieldSerialNo%>" Name="serialNo" AllowBlank="false" />

                                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" AllowBlank="false" ValueField="key" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="value" ID="condition" Name="condition" FieldLabel="<%$ Resources:FieldCondition%>" SimpleSubmit="true">

                                            <Store>
                                                <ext:Store runat="server" ID="conditionStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="key" />
                                                                <ext:ModelField Name="value" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>

                                        </ext:ComboBox>
                                        <ext:DateField ID="warrantyEndDate" runat="server" FieldLabel="<%$ Resources:FieldWarrantyEndDate%>" Name="warrantyEndDate">
                                            <Listeners>
                                                <Change Handler="#{depreciationDate}.validate(); " />
                                            </Listeners>
                                        </ext:DateField>



                                        <ext:TextField ID="department" runat="server" FieldLabel="<%$ Resources:FieldDepartment%>" Name="department" ReadOnly="true" />
                                        <ext:TextField ID="disposedDate" runat="server" FieldLabel="FieldDisposedDate" Name="disposedDate" ReadOnly="true" Hidden="true" />
                                        <ext:TextField ID="employeeFullName" runat="server" FieldLabel="<%$ Resources:FieldEmployeeName%>" Name="employeeFullName" ReadOnly="true" />
                                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ValueField="key" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="value" ID="status" Name="status" FieldLabel="<%$ Resources:FieldStatus%>" ReadOnly="true">

                                            <Store>
                                                <ext:Store runat="server" ID="statusStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="key" />
                                                                <ext:ModelField Name="value" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>

                                        </ext:ComboBox>

                                    </Items>

                                </ext:Panel>
                                <ext:Panel runat="server" MarginSpec="0 0 0 0" ID="rightPanel">
                                    <Items>

                                        <ext:DateField ID="receptionDate" runat="server" FieldLabel="<%$ Resources:FieldReceptionDate%>" Name="receptionDate">
                                            <Listeners>
                                                <Change Handler="App.warrantyEndDate.setMinValue(this.value);App.warrantyEndDate.setValue(this.value);"></Change>
                                            </Listeners>
                                        </ext:DateField>
                                        <ext:Container runat="server" Layout="AutoLayout">
                                            <Content>

                                                <uc:SupplierControl runat="server" ID="supplierId" FieldLabel="<%$ Resources: Fieldsupplier %>" />
                                            </Content>
                                        </ext:Container>
                                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="reference" ID="currencyId" Name="currencyId" FieldLabel="<%$ Resources:FieldCurrency%>">

                                            <Store>
                                                <ext:Store runat="server" ID="currencyStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="recordId" />
                                                                <ext:ModelField Name="reference" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <RightButtons>
                                                <ext:Button ID="Button7" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addCurrency">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>

                                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:NumberField ID="costPrice" runat="server" FieldLabel="cost" Name="<%$ Resources:FieldcostPrice%>" AllowDecimals="true" />
                                        <ext:TextField ID="poRef" runat="server" FieldLabel="<%$ Resources:FieldpoRef%>" Name="poRef" />

                                        <ext:DateField ID="depreciationDate" runat="server" FieldLabel="<%$ Resources:FieldDepreciationDate%>" Name="depreciationDate">
                                            <Validator Handler="if (#{warrantyEndDate}.getValue()!=null) return this.value>#{warrantyEndDate}.getValue(); else return true;" />
                                            <Listeners>
                                                <Change Handler="#{depreciationDate}.validate(); " />
                                            </Listeners>
                                        </ext:DateField>


                                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" MaxWidth="300" runat="server" EnableRegEx="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" SubmitValue="true" ReadOnly="true">
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


                                        </ext:ComboBox>
                                        <ext:TextArea ID="comments" runat="server" FieldLabel="<%$ Resources:FieldComments%>" Name="comments" DataIndex="comments" />
                                    </Items>

                                </ext:Panel>

                            </Items>
                            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession();  if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="disposedDate" Value="#{disposedDate}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').close();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
                        </ext:FormPanel>
                     
                     <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: Properties %>" ID="Panel8" DefaultAnchor="100%">
                    <Loader runat="server" Url="AssetPropertyExplorer.aspx" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <Listeners>
                         </Listeners>
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
                    </Items>
                </ext:TabPanel>
            </Items>
            <Listeners>
                <Hide Handler="App.panelRecordDetails.selectedIndex =0; console.log(App.panelRecordDetails);" />
            </Listeners>
        </ext:Window>



    </form>
</body>
</html>
