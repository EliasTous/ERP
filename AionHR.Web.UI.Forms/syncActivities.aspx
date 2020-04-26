<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncActivities.aspx.cs" Inherits="Web.UI.Forms.SyncActivities" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    
    <script type="text/javascript" src="Scripts/common.js"></script>
  

</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />
         

          <ext:Hidden ID="processing" runat="server"/>

        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:TabPanel runat="server">
                    <Items>
                       
                        <ext:FormPanel DefaultButton="syncBtn"
                            ID="syncActivityForm"
                            runat="server"
                            Title="<%$ Resources: syncActivityForm%>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                  <ext:ComboBox  AnyMatch="true" CaseSensitive="false" MaxWidth="300"  QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources:FieldActivityId%>"  runat="server" DisplayField="value" ValueField="key"   Name="activityId" ID="activityId" AllowBlank="false" >
                                             <Store>
                                                <ext:Store runat="server" ID="activityStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                       </ext:ComboBox>
                           
                                <ext:DateField AllowBlank="false" runat="server" ID="startingDate" MaxWidth="300" FieldLabel="<%$ Resources: date %>" Format="dd/MM/yyyy" >
                                     <Listeners> 
                                         <Change Handler="App.endingDate.setMinValue(this.value);"></Change>
                                     </Listeners>
                                    </ext:DateField>
                                  <ext:DateField AllowBlank="false" runat="server" ID="endingDate" MaxWidth="300" FieldLabel="<%$ Resources: endDate %>" Format="dd/MM/yyyy"  >
                                         
                                    </ext:DateField>
                                    
                                <ext:Button  ID="syncBtn" runat="server" Text="<%$ Resources: Synchronize %>" Icon="ApplicationGo" MaxWidth="300">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if(!#{syncActivityForm}.getForm().isValid()){return false;} " />
                                    </Listeners>
                                    <DirectEvents>

                                        <Click OnEvent="StartLongAction" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                      <%--      <EventMask ShowMask="true"  />--%>
                                         </Click>
                                    </DirectEvents>
                                </ext:Button>
                           
                                        
                                  <ext:ProgressBar ID="Progress1" runat="server" MaxWidth="300" />
                                
                            </Items>
                            
                           
                        
               </ext:FormPanel>

                    </Items>
                     
                      
                       </ext:TabPanel>
                


            </Items>
          
          
        </ext:Viewport>


        
        
        <ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task 
                    TaskID="longactionprogress"
                    Interval="100" 
                    AutoRun="false" 
                    OnStart="#{syncBtn}.setDisabled(true);"
                       OnStop="#{syncBtn}.setDisabled(false);" >
                 
                    <DirectEvents>
                        <Update OnEvent="RefreshProgress" />
                    </DirectEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>



    </form>
</body>
</html>