<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenaltyTypes.aspx.cs" Inherits="AionHR.Web.UI.Forms.PenaltyTypes" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/DocumentTypes.js?id=1"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">
     
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
        function openInNewTab() {
            window.document.forms[0].target = '_blank';

        }

        function actionRenderer(value) {
            if (App.actionStore.getById(value) != null) {
                return App.actionStore.getById(value).data.name;
            } else {
                return value;
            }
        }
        function deductionTypeRenderer(value) {
            if (App.deductionTypeStore.getById(value) != null) {
                return App.deductionTypeStore.getById(value).data.name;
            } else {
                return value;
            }
        }

       function fromToCheck(reason, value)
        {
          

               switch (reason)
               {
                   case '1': if (value == '0' || value == '15' || value == '30' || value == '60') return true; else return false;
                       break;
                   case '2': if (value >= '0' && value <= '30')  return true; else return false; 
                       break;
               }
       }
       function DisabledFileds(disabled)
       {
          
           App.from.setDisabled(disabled);
           App.to.setDisabled(disabled);
           App.timeCode.setDisabled(disabled);
        
       }
       function ChangeReason(Reason,Attendance, TimeBase)
       {
         
           if (Reason == Attendance)
           {
            
               App.from.setDisabled(false);
               App.to.setDisabled(false);
               App.timeCode.setDisabled(false);
               App.timeBase.setReadOnly(false);
           }
           else
           {
             
               App.from.setDisabled(true);
               App.to.setDisabled(true);
               App.timeCode.setDisabled(true);
               App.timeBase.setValue(TimeBase);
               App.timeBase.setReadOnly(true); 
           }

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
        <ext:Hidden ID="reasonStringHF" runat="server" />
        <ext:Hidden ID="timeBaseStringHF" runat="server"  />
        <ext:Hidden ID="timeVariationTypeString" runat="server"  />
         <ext:Hidden ID="currentPenaltyType" runat="server"  />
         <ext:Hidden ID="penaltyDetailStoreCount" runat="server"  />


         <ext:Hidden ID="Reason_ATTENDANCE_Value" Text="<%$ Resources:ComboBoxValues, Reason_ATTENDANCE%>" runat="server"  />
         <ext:Hidden ID="TimeBasee_DAYS_Value" Text="<%$ Resources:ComboBoxValues, TimeBasee_DAYS%>" runat="server"  />

      
       

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
                        
                         <ext:ModelField Name="reason" />
                         <ext:ModelField Name="reasonString" />
                        
                      <%--   <ext:ModelField Name="edId" />--%>
                         <ext:ModelField Name="timeBase" />
                         <ext:ModelField Name="timeBaseString" />
                        
                         <ext:ModelField Name="from" />
                         <ext:ModelField Name="to" />
                         <ext:ModelField Name="timeCode" />
                         <ext:ModelField Name="timeCodeString" />
                         <ext:ModelField Name="name" />
                        <%--<ext:ModelField Name="intName" />--%>
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>
               <ext:Store
            ID="actionStore"
            runat="server">
           
            <Model>
                <ext:Model ID="Model3" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="name" />
                        <%--<ext:ModelField Name="intName" />--%>
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>
            <ext:Store
            ID="deductionTypeStore"
            runat="server">
           
            <Model>
                <ext:Model ID="Model4" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="name" />
                        <%--<ext:ModelField Name="intName" />--%>
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
                            <%--    <ext:Column ID="ColEdId" Visible="false" DataIndex="edId" runat="server" />--%>
                            <ext:Column CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false"/>
                        <%--     <ext:Column CellCls="cellLink" ID="ColEdName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEdName%>" DataIndex="edName" Flex="2" Hideable="false"/>--%>
                             <ext:Column CellCls="cellLink" ID="ColReason" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldReason%>" DataIndex="reasonString" Flex="2" Hideable="false">
                                 </ext:Column>
                             <ext:Column CellCls="cellLink" ID="ColTimeBase" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTimeBase%>" DataIndex="timeBaseString" Flex="2" Hideable="false"/>
                             <ext:Column CellCls="cellLink" ID="ColDuration" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFrom%>" DataIndex="from"  Hideable="false"/>
                             <ext:Column CellCls="cellLink" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTo%>" DataIndex="to"  Hideable="false"/>
                             <ext:Column CellCls="cellLink" ID="ColTimeVariationType" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTimeVariationType%>" DataIndex="timeCodeString" Flex="2" Hideable="false"/>
                     


                          

                            

                            <ext:Column runat="server"
                                ID="colDelete" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="100"
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
            Width="800"
            Height="320"
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
                            BodyPadding="5" >
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name" AllowBlank="false" />
                                 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="reason" runat="server" FieldLabel="<%$ Resources:FieldReason%>" Name="reason" IDMode="Static" SubmitValue="true" AllowBlank="false">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: ReasonATTENDANCE%>" Value="<%$ Resources:ComboBoxValues, Reason_ATTENDANCE%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: ResonLAW_VIOLATION%>" Value="<%$ Resources:ComboBoxValues, Reason_LAW_VIOLATION%>"></ext:ListItem>
                                        
                                      
                                    </Items>
                                     <Listeners>
                                     <Select Handler="ChangeReason(this.value,#{Reason_ATTENDANCE_Value}.getValue() , #{TimeBasee_DAYS_Value}.getValue());"></Select>
                                     </Listeners>
                                </ext:ComboBox>

                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="timeBase" runat="server" FieldLabel="<%$ Resources:FieldTimeBase%>" Name="timeBase" IDMode="Static" SubmitValue="true" AllowBlank="false">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: TimeBaseMINUTES%>" Value="<%$ Resources:ComboBoxValues, TimeBase_MINUTES%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: TimeBaseDAYS%>" Value="<%$ Resources:ComboBoxValues, TimeBasee_DAYS%>"></ext:ListItem>
                                      
                                    </Items>
                                </ext:ComboBox>


                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="timeCode" runat="server" FieldLabel="<%$ Resources:FieldTimeVariationType%>" Name="timeCode" ValueField="timeCode" DisplayField="name"  AllowBlank="false">
                                   <Store>
                <ext:Store runat="server" ID="timeVariationStore">
                    <Model>
                        <ext:Model runat="server">
                            <Fields>
                                                
                        <ext:ModelField Name="timeCode" />
                          <ext:ModelField Name="name" />
                       

                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>
            
                                </ext:ComboBox>
                                   <ext:NumberField ID="from" runat="server" FieldLabel="<%$ Resources: FieldFrom%>" Name="from" MinValue="0" AllowBlank="true"  >
                                    <%--  <Validator Handler=" if ( this.value<#{to}.getValue() ) {return fromToCheck(#{timeBase}.getValue(),this.value);} else return false; " />--%>
                                    <Validator Handler=" if (this.value>#{to}.getValue() &&  #{from}.getValue()!=null  && this.value!=null ) return false;else return true; " />
                                       <Listeners>
                                         <Change Handler="#{to}.validate();"></Change>
                                           </Listeners>
                                       </ext:NumberField>
                                <ext:NumberField ID="to" runat="server" FieldLabel="<%$ Resources: FieldTo%>" Name="to"  MinValue="0" AllowBlank="true" >
                                    <Validator Handler="if (this.value<#{from}.getValue() && #{to}.getValue()!=null && this.value!=null  ){ return false;} else return true; " />
                                    <Listeners>
                                        <Change Handler="#{from}.validate();"></Change>
                                    </Listeners>
                                    </ext:NumberField>
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
                            </Items>

                        </ext:FormPanel>
                        <ext:GridPanel
                            ID="penaltyDetailGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            Title="<%$ Resources: penaltyDetailTitle %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True" >
                            <Plugins>
	<ext:CellEditing ClicksToEdit="1" >
		
	</ext:CellEditing>
</Plugins> 
                            <Store>
                                <ext:Store
                                    ID="penaltyDetailStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="false"
                                    OnReadData="penaltyDetailStore_ReadData"
                                    PageSize="50" IDMode="Explicit" Namespace="App">

                                    <Model>
                                        <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                            <Fields>

                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="ptId" />
                                                <ext:ModelField Name="damage" />
                                                <ext:ModelField Name="sequence" />
                                                <ext:ModelField Name="action" />
                                                <ext:ModelField Name="pct" />
                                                <ext:ModelField Name="includeTV" />
                                              

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
                            
                                 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="damage" runat="server" FieldLabel="<%$ Resources:FieldDamage%>" Name="damage" IDMode="Static" SubmitValue="true" ForceSelection="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: DamageWITH_DAMAGE%>" Value="<%$ Resources:ComboBoxValues, Damage_WITH_DAMAGE%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: DamageWITHOUT_DAMAGE%>" Value="<%$ Resources:ComboBoxValues, Damage_WITHOUT_DAMAGE%>" ></ext:ListItem>
                                      
                                    </Items>
                                     <Listeners>
                                         <Select Handler="#{penaltyDetailStore}.reload();"> </Select>
                                     </Listeners>
                                </ext:ComboBox>
                             
                               
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                     

                            <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="Column1" Visible="false" DataIndex="recordId" runat="server" />
                                     <ext:Column ID="Column4" Visible="false" DataIndex="ptId" runat="server" />
                                    <ext:Column ID="Column6" Visible="true" DataIndex="sequence" runat="server" Width="150" Text="<%$ Resources:FieldSeq  %>" Flex="1">
                                        
                                    </ext:Column>
                                   
                                    


                                       <ext:Column ID="COaction" Visible="true" DataIndex="action" runat="server" Text="<%$ Resources: FieldAction  %>" Flex="2" >
                                           <Renderer Handler="actionRenderer" />
                               <Editor>
                                           <ext:ComboBox 
                                                            runat="server" 
                                                            Shadow="false" 
                                                            Mode="Local" 
                                                            TriggerAction="All" 
                                                            ForceSelection="true"
                                                            StoreID="actionStore" 
                                                            DisplayField="name" 
                                                            ValueField="recordId"
                                                            Name="action"
                                                            AllowBlank="false"
                                                            >
                                                <Listeners>

                                                    <Select Handler="var rec = this.getWidgetRecord(); rec.set('action',this.value); ">
                                                    </Select>
                                                </Listeners>
                                               </ext:ComboBox>
                                  
                </Editor>
                                             
                                           </ext:Column>
                                     <ext:Column ID="CodeductionType" Visible="true" DataIndex="deductionType" runat="server" Text="<%$ Resources: FieldDeductionType  %>" Flex="2" >
                                           <Renderer Handler="deductionTypeRenderer" />
                               <Editor>
                                           <ext:ComboBox 
                                                            runat="server" 
                                                            Shadow="false" 
                                                            Mode="Local" 
                                                            TriggerAction="All" 
                                                            ForceSelection="true"
                                                            StoreID="deductionTypeStore" 
                                                            DisplayField="name" 
                                                            ValueField="recordId"
                                                            Name="deductionType"
                                                            AllowBlank="true"
                                                            >
                                                <Listeners>

                                                    <Select Handler="var rec = this.getWidgetRecord(); rec.set('deductionType',this.value); ">
                                                    </Select>
                                                </Listeners>
                                               </ext:ComboBox>
                                  
                </Editor>
                                             
                                           </ext:Column>
                                     

                                        <ext:WidgetColumn ID="WidgetColumn2" Visible="true" DataIndex="pct" runat="server" Text="<%$ Resources: FieldPct  %>" Flex ="1" >
                                        <Widget>
                                            <ext:NumberField AllowBlank="true" runat="server" Name="pct" MinValue="0" >
                                                 <Listeners>

                                                    <Change Handler="var rec = this.getWidgetRecord(); rec.set('pct',this.value); ">
                                                    </Change>
                                                     
                                                </Listeners>
                                             
                                                </ext:NumberField>
                                        </Widget>
                                    </ext:WidgetColumn>

                                     <ext:WidgetColumn ID="Column2" MenuDisabled="true"    DataIndex="includeTV" runat="server" Text="<%$ Resources: FieldIncludeTV  %>"  Width="150"   >
                                        <Widget>
                                            <ext:Checkbox runat="server" Name="includeTV"  ID="includeTV"  >
                                                <Listeners>

                                                    <Change Handler="var rec = this.getWidgetRecord(); rec.set('includeTV',this.value); ">
                                                    </Change>
                                                </Listeners>
                                            </ext:Checkbox>
                                        </Widget>
                                    </ext:WidgetColumn>
                                      
                                  
                                   

                                   

                                    
                                    



                                </Columns>
                            </ColumnModel>
                        
 
                            <View>
                                <ext:GridView ID="GridView2" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                            <Listeners> 
                                <Activate Handler="#{damage}.setValue(1); #{penaltyDetailStore}.reload();  #{penaltyDetailStoreCount}.setValue(1);"></Activate>
                            </Listeners>
                        </ext:GridPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                                  <ext:Parameter Name="codes" Value="Ext.encode(#{penaltyDetailGrid}.getRowsValues( ))" Mode="Raw"   />
                               
                              

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
