<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SynchronizeAttendanceDays.aspx.cs" Inherits="AionHR.Web.UI.Forms.SynchronizeAttendanceDays" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    
    <script type="text/javascript" src="Scripts/common.js">

        function updateEventMask(f) {
        alert(f);
            App.GridPanel1.getView().loadMask.msg = "New Loading Message";

        }
       
    </script>
  

</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />
         
          <ext:Hidden ID="currentRow" runat="server"  />
          <ext:Hidden ID="totalRow" runat="server"  />
        
          <ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task 
                    TaskID="longactionprogress"
                    Interval="10000" 
                    AutoRun="false"
                    OnStart="alert('start');"
                       
                    OnStop="
                       alert('stop');">
                    <DirectEvents>
                        <Update OnEvent="RefreshProgress" />
                    </DirectEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>


        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:TabPanel runat="server">
                    <Items>
                       
                        <ext:FormPanel DefaultButton="SynchronizeAttendancebtn"
                            ID="SynchronizeAttendanceForm"
                            runat="server"
                            Title="<%$ Resources: SynchronizeAttendanceDays%>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:ComboBox   FieldLabel="<%$ Resources: FilterEmployee%>"  MaxWidth="300" AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeFilter" 
                                      DisplayField="fullName"
                                      ValueField="recordId" AllowBlank="false"
                                      TypeAhead="false"
                                        HideTrigger="true" SubmitValue="true"
                                      MinChars="3" 
                                  TriggerAction="Query" ForceSelection="true" >
                                        <Store>
                                            <ext:Store runat="server" ID="Store2" AutoLoad="false">
                                                <Model>
                                                    <ext:Model runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="recordId" />
                                                            <ext:ModelField Name="fullName" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                                <Proxy>
                                                    <ext:PageProxy DirectFn="App.direct.FillEmployee"></ext:PageProxy>
                                                </Proxy>
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
                                    
                                <ext:Button Hidden="false" ID="SynchronizeAttendanceBtn" runat="server" Text="<%$ Resources: Synchronize %>" Icon="ApplicationGo" MaxWidth="300">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if(!#{SynchronizeAttendanceForm}.getForm().isValid()){return false;} " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="StartLongAction" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"  />
                                         </Click>
                                    </DirectEvents>
                                </ext:Button>


                                 <ext:ProgressBar ID="Progress1" runat="server" Width="300" />
        
      
                           
                                        
                                 
                                
                            </Items>
                            
                           
                        
               </ext:FormPanel>

                    </Items>
                      
                       </ext:TabPanel>
                


            </Items>


        </ext:Viewport>






    </form>
</body>
</html>