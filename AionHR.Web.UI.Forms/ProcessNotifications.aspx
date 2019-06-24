<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessNotifications.aspx.cs" Inherits="AionHR.Web.UI.Forms.ProcessNotifications" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
  
    <script type="text/javascript" src="Scripts/common.js" ></script>
    <script  type="text/javascript" src="Scripts/PayCodes.js?id=8"></script>
    <script type="text/javascript" >
        function actionRenderer(value) {
          
            var find = App.Store1.find('recordId', value);
           
            if (find != -1) {
                var record = App.Store1.getAt(find);
                
                
                return record.data.name;
            }
            else
                return " ";
            
                

          
        }
    </script>
 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        
            <ext:Store
            ID="Store1"
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


    
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
              <ext:GridPanel ID="ProcessNotificationGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            Title="<%$ Resources: ProcessNotificationTitle %>"
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
                                    ID="ProcessNotificationStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="false"
                                    OnReadData="ProcessNotificationStore_ReadData"
                                    PageSize="50" IDMode="Explicit" Namespace="App"  OnSubmitData="SubmitData">

                                    <Model>
                                        <ext:Model ID="Model2" runat="server" >
                                            <Fields>

                                                <ext:ModelField Name="teName" />
                                                <ext:ModelField Name="processName" />
                                                <ext:ModelField Name="templateId" />
                                                      <ext:ModelField Name="processId" />
                                             
                                              

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                 
                                </ext:Store>

                            </Store>
                           
                     

                            <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="Column1" Visible="false" DataIndex="recordId" runat="server" />
                                  
                                    <ext:Column ID="prName" Visible="true" DataIndex="processName" runat="server" Text="<%$ Resources:prName  %>" Flex="1">
                                        
                                    </ext:Column>
                                   
                                    


                                       <ext:Column ID="templateId" Visible="true" DataIndex="templateId" runat="server" Text="<%$ Resources: templateId  %>" Flex="2" >
                                           <Renderer Handler="actionRenderer" />
                               <Editor>
                                           <ext:ComboBox 
                                                            runat="server" 
                                                            Shadow="false" 
                                                            Mode="Local" 
                                                            TriggerAction="All" 
                                                            ForceSelection="true"
                                                            StoreID="Store1" 
                                                            DisplayField="name" 
                                                            ValueField="recordId"
                                                            Name="templateId"
                                                            AllowBlank="true"
                                                            >
                                                <Listeners>

                                                    <Select Handler="var rec = this.getWidgetRecord(); rec.set('templateId',this.value); ">
                                                    </Select>
                                                </Listeners>
                                               </ext:ComboBox>
                                  
                </Editor>
                                             
                                           </ext:Column>
                                    </Columns>
                                          </ColumnModel>
                        
 
                            <View>
                                <ext:GridView ID="GridView2" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                  <Buttons>
              <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk" >
                    <Listeners>
                        <Click Handler="#{ProcessNotificationGrid}.submitData();" />
                    </Listeners>
                </ext:Button>

               </Buttons>
            </ext:GridPanel>  
             
            </Items>
            
        </ext:Viewport>

        

      


    </form>
</body>
</html>