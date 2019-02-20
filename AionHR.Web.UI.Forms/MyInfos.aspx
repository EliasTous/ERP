<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyInfos.aspx.cs" Inherits="AionHR.Web.UI.Forms.MyInfos" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=2" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Notes.js?id=30"></script>
    <script type="text/javascript" src="Scripts/common.js?id=20"></script>
    <script type="text/javascript" src="Scripts/moment.js?id=10"></script>
   


</head>
<body style="background: url(Images/bg.png) repeat;">
    
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
        <ext:Hidden ID="CurrentEmployeeName" runat="server" />
       
        <ext:Window 
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="elias"
            Width="650"
            Height="550"
            AutoShow="false"
            Draggable="true"
            Maximizable="true"
            Resizable="false"
            Modal="true"
            Hidden="true"
            Layout="Fit" AutoScroll="true"
            >
            <Listeners>
                <Close Handler="closeCurrentTab();"></Close>
            </Listeners>
            <Items>
                   <ext:TabPanel ID="panelRecordDetails" Layout="FitLayout" DefaultAnchor="100%" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Region="Center">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarFill runat="server">
                        </ext:ToolbarFill>

                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Items>
            <ext:FormPanel DefaultButton="SaveButton"
                    ID="BasicInfoTab" PaddingSpec="0 0 0 0"
                    runat="server"
                    Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5" Layout="VBoxLayout">


                    <Items>
                        
                                <ext:TextField ID="recordId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Name="recordId" />
                                <ext:TextField ID="reference" MsgTarget="None"  runat="server" FieldLabel="<%$ Resources:FieldReference%>" Name="reference" ReadOnly="true" LabelWidth="120" >
                                   <%-- <Listeners>
                                       <FocusLeave Handler="if(#{CurrentEmployee}.value!='') return; App.direct.employeeControl1.CheckReference(this.value,{
                                        success: function (result) {
                                            if(result=='1'){App.employeeControl1_reference.markInvalid('');} }});" />
                                       
                                                            </Listeners>
                                  
                                   --%>
                                    </ext:TextField>
                                <ext:TextField ID="firstName"  runat="server" FieldLabel="<%$ Resources:FieldFirstName%>" Name="firstName" AllowBlank="true" BlankText="<%$ Resources:Common, MandatoryField%>" ReadOnly="true" LabelWidth="120">
                                
                                </ext:TextField>
                                <ext:TextField ID="middleName" LabelWidth="120" runat="server" FieldLabel="<%$ Resources:FieldMiddleName%>" Name="middleName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="lastName"  AllowBlank="true" runat="server" FieldLabel="<%$ Resources:FieldLastName%>" Name="lastName" BlankText="<%$ Resources:Common, MandatoryField%>" ReadOnly="true" LabelWidth="120" />
                                <ext:TextField ID="familyName" LabelWidth="120" runat="server" FieldLabel="<%$ Resources:FieldFamilyName%>" Name="familyName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="idRef" Hidden="true" runat="server" AllowBlank="true" FieldLabel="<%$ Resources:FieldIdRef%>" Name="idRef" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="homeEmail" LabelWidth="120" runat="server" FieldLabel="<%$ Resources:FieldHomeEmail%>" Name="homeMail" Vtype="email" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="workEmail" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldWorkEmail%>" Name="workMail" Vtype="email" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                 <ext:RadioGroup Hidden="true" ID="gender" AllowBlank="true" runat="server" GroupName="gender" FieldLabel="<%$ Resources:FieldGender%>">
                                    <Items>
                                        <ext:Radio runat="server" ID="gender0" Name="gender" InputValue="0" BoxLabel="<%$ Resources:Common ,Male%>" />
                                        <ext:Radio runat="server" ID="gender1" Name="gender" InputValue="1" BoxLabel="<%$ Resources:Common ,Female%>" />
                                    </Items>
                                </ext:RadioGroup>


                        <%--    </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" MarginSpec="0 0 0 0" ID="rightPanel">
                            <Items>--%>
                               
                                <ext:TextField LabelWidth="120" ID="mobile" AllowBlank="true" MinLength="6" MaxLength="18" runat="server" FieldLabel="<%$ Resources:FieldMobile%>" Name="mobile" BlankText="<%$ Resources:Common, MandatoryField%>">
                                    <Validator Handler="return !isNaN(this.value);" />
                                </ext:TextField>
                                <ext:ComboBox LabelWidth="120"   AnyMatch="true" CaseSensitive="false"  ID="religionCombo" runat="server" FieldLabel="<%$ Resources:FieldReligion%>" Name="religion" IDMode="Static" SubmitValue="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion0%>" Value="<%$ Resources:ComboBoxValues, SSEMReligionChristian%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion1%>" Value="<%$ Resources:ComboBoxValues, SSEMReligionMuslim%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion2%>" Value="<%$ Resources:ComboBoxValues, SSEMReligionJew%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion3%>" Value="<%$ Resources:ComboBoxValues, SSEMReligionBudha%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion4%>" Value="<%$ Resources:ComboBoxValues, SSEMReligionSeikh%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion5%>" Value="<%$ Resources:ComboBoxValues, SSEMReligionHindu%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion6%>" Value="<%$ Resources:ComboBoxValues, SSEMReligionOther%>"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                                <ext:DateField LabelWidth="120"
                                    runat="server" ID="birthDate"
                                    Name="birthDate"
                                    FieldLabel="<%$ Resources:FieldDateOfBirth%>"
                                    MsgTarget="Side"
                                    AllowBlank="true"  ReadOnly="true"/>
                                <ext:ComboBox Hidden="true"   AnyMatch="true" CaseSensitive="false"  runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="true" DisplayField="name" ID="nationalityId" Name="nationalityId" FieldLabel="<%$ Resources:FieldNationality%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="NationalityStore">
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
                                    <RightButtons>
                                        <ext:Button ID="Button7" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();" />
                                            </Listeners>
                                           <%-- <DirectEvents>

                                                <Click OnEvent="addNationality">
                                                </Click>
                                            </DirectEvents>--%>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:FieldContainer runat="server" Border="true" Visible="false">
                                    <Items>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="false" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="departmentId" Name="departmentId" FieldLabel="<%$ Resources:FieldDepartment%>" SimpleSubmit="true">
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
                                            <RightButtons>
                                                <ext:Button ID="Button2" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                  <%--  <DirectEvents>

                                                        <Click OnEvent="addDepartment">
                                                        </Click>
                                                    </DirectEvents>--%>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler="  if(!this.readOnly) this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="false" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" SimpleSubmit="true">
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
                                            <RightButtons>
                                                <ext:Button ID="Button3" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();" />
                                                    </Listeners>
                                                   <%-- <DirectEvents>

                                                        <Click OnEvent="addBranch">
                                                        </Click>
                                                    </DirectEvents>--%>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>



                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="false" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="divisionId" Name="divisionId" FieldLabel="<%$ Resources:FieldDivision%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="divisionStore" IDMode="Explicit">
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
                                            <RightButtons>
                                                <ext:Button ID="Button4" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <%--<DirectEvents>

                                                        <Click OnEvent="addDivision">
                                                        </Click>
                                                    </DirectEvents>--%>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="false" ValueField="recordId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="positionId" Name="positionId" FieldLabel="<%$ Resources:FieldPosition%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="positionStore">
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
                                            <RightButtons>
                                                <ext:Button ID="Button1" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                   <%-- <DirectEvents>

                                                        <Click OnEvent="addPosition">
                                                        </Click>
                                                    </DirectEvents>--%>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:FieldContainer>


                                <ext:ComboBox Hidden="true"  AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="vsId" Name="vsId" FieldLabel="<%$ Resources:FieldVacationSchedule%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="VacationScheduleStore">
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
                                <ext:ComboBox Hidden="true"  AnyMatch="true" CaseSensitive="false"  runat="server" ID="caId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" Name="caId" FieldLabel="<%$ Resources:FieldWorkingCalendar%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="CalendarStore">
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
                                <ext:ComboBox  Hidden="true" AnyMatch="true" CaseSensitive="false"  runat="server" ID="scId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" Name="scId" FieldLabel="<%$ Resources:FieldSchedule%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="scheduleStore">
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
                                <ext:TextField LabelWidth="120" ID="birthPlace" runat="server" FieldLabel="<%$ Resources:FieldBirthPlace%>" Name="placeOfBirth" AllowBlank="true" />



                                <ext:DateField Hidden="true"
                                    runat="server"
                                    Name="hireDate" ID="hireDate"
                                    FieldLabel="<%$ Resources:FieldHireDate%>"
                                    MsgTarget="Side"
                                    AllowBlank="true" />


                        
                        <%-- <ext:Panel runat="server" Margin="20" Visible="false">
                                    <Items>
                                        <ext:Image runat="server" ID="imgControl" Width="200" Height="200">
                                            <Listeners>
                                                <Click Handler="triggierImageClick(App.employeeControl1_picturePath.fileInputEl.id); " />
                                            </Listeners>
                                        </ext:Image>
                                        <ext:FileUploadField ID="picturePath" runat="server" ButtonOnly="true" Hidden="true">

                                            <Listeners>
                                                <Change Handler="showImagePreview(App.employeeControl1_picturePath.fileInputEl.id);" />
                                            </Listeners>
                                        </ext:FileUploadField>

                                    </Items>
                                </ext:Panel>--%>
                    </Items>
                    <BottomBar>
                        <ext:Toolbar runat="server" ClassicButtonStyle="false" Cls="tlb-BackGround">

                            <Items>
                            <%--    <ext:Button Cls="x-btn-left" ID="DeleteButton" Visible="false" Text="Delete" DefaultAlign="Left" AlignTarget="Left" Icon="Delete" Region="West" runat="server">
                                    <Listeners>
                                        <Click Handler="CheckSession();  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="DeleteRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>--%>

                                <ext:ToolbarFill runat="server" />
                                 <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {  return false;}" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                 <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                                    <Listeners>
                                        <Click Handler="this.up('window').hide();closeCurrentTab();" />
                                    </Listeners>
                                </ext:Button>

                            </Items>
                        </ext:Toolbar>
                    </BottomBar>

                </ext:FormPanel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: ContactsTab %>" ID="Panel6" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Contacts.aspx?terminated=0&fromSelfService=true" Mode="Frame" ID="Loader6" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                      <Params>
                        <ext:Parameter Name="employeeId" Value="function() { return App.CurrentEmployee.getValue(); }" Mode="Value" />
                      </Params>
                        <LoadMask ShowMask="true" />
                    </Loader>

                </ext:Panel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: Dependants %>" ID="Panel8" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Dependants.aspx?terminated=0&fromSelfService=true" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <Params>
                        <ext:Parameter Name="employeeId" Value="function() { return App.CurrentEmployee.getValue(); }" Mode="Value" />
                      </Params>
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            </Items>
           </ext:TabPanel>
            </Items>
    </ext:Window>


    </form>
</body>
</html>
