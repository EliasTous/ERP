<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDepts.aspx.cs" Inherits="AionHR.Web.UI.Forms.AdminDepts" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="CSS/Common.css?id=11" />






   
    <link rel="stylesheet" href="CSS/LiveSearch.css?id=10" />


    <script type="text/javascript" src="Scripts/common.js?id=122"></script>
    <script type="text/javascript" src="Scripts/jquery-new.js?id=125"></script>
  
    



     

    <script type="text/javascript">
     
        
    </script>

</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        
        <ext:Hidden ID="CurrentEmployeePhotoName" runat="server" EnableViewState="true"  />
        <ext:Hidden runat="server" ID="imageData" />
        <ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />


        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
               
                    
                       
                        <ext:FormPanel DefaultButton="SaveDepartmentsBtn"
                            ID="admindeptsForm"
                            runat="server"
                            Title="<%$Resources:AdminDepartments %>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                   <ext:ItemSelector runat="server" MaxHeight="300" MinHeight="300" AutoScroll="true" ID="departmentSelector" FromTitle="<%$Resources:AllDepartments %>" DisplayField="departmentName" ValueField="departmentId"
                            ToTitle="<%$Resources:AdminDepartments %>">
                            <Listeners>
                                <AfterRender Handler="App.departmentStore.reload();" />
                            </Listeners>
                            <Store>
                                <ext:Store 
                                    PageSize="30" IDMode="Explicit" Namespace="App" runat="server" ID="departmentStore" OnReadData="departmentStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="departmentId">
                                            <Fields>
                                                <ext:ModelField Name="departmentId" />
                                                <ext:ModelField Name="departmentName" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>

                                  
                                </ext:Store>
                            </Store>

                        </ext:ItemSelector>
                             
                        
                            </Items>
                            <Buttons>
                                <ext:Button  ID="SaveDepartmentsBtn" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession();  if (!#{admindeptsForm}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveDepartments" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"  />
                                            <ExtraParams>
                                             <ext:Parameter 
                            Name="selectedDepts"                                  
                            Value="App.departmentSelector.toField.getStore().getRecordsValues()" 
                            Mode="Raw" 
                            Encode="true" />
                                                
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:FormPanel>
                        </Items>
                                  
                


       

        </ext:Viewport>



       


    </form>
</body>
</html>

