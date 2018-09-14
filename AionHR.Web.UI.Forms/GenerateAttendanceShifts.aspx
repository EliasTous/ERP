<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateAttendanceShifts.aspx.cs" Inherits="AionHR.Web.UI.Forms.GenerateAttendanceShifts" %>

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
         

        

        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:TabPanel runat="server">
                    <Items>
                       
                        <ext:FormPanel DefaultButton="GenerateAttendancebtn"
                            ID="GenerateAttendanceForm"
                            runat="server"
                            Title="<%$ Resources: GenerateAttendanceDays%>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                  <ext:ComboBox      AnyMatch="true" CaseSensitive="false" MaxWidth="300"  runat="server" EnableRegEx="true"  AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" >
                                                <Store>
                                                    <ext:Store runat="server" ID="BranchStore">
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
                                    
                                <ext:Button Hidden="false" ID="GenerateAttendanceBtn" runat="server" Text="<%$ Resources: Generate %>" Icon="ApplicationGo" MaxWidth="300">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if(!#{GenerateAttendanceForm}.getForm().isValid()){return false;} " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="GenerateAttendance" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"  />
                                         </Click>
                                    </DirectEvents>
                                </ext:Button>
                           
                                        
                                 
                                
                            </Items>
                            
                           
                        
               </ext:FormPanel>

                    </Items>
                      
                       </ext:TabPanel>
                


            </Items>


        </ext:Viewport>






    </form>
</body>
</html>
