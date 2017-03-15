<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeProfile.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.EmployeeProfile" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />

    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/EmployeeProfile.js?id=0"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <ext:ResourceManager runat="server" />
         <ext:Viewport ID="Viewport11" runat="server" Layout="BorderLayout">
           
             <Items>
    <ext:FormPanel DefaultButton="SaveButton"
        ID="BasicInfoTab"
        runat="server"
        Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
        Icon="ApplicationSideList"
         Header="false"  
        DefaultAnchor="100%"
        BodyPadding="5" Layout="TableLayout" Region="Center">

        <Items>
            <ext:Panel runat="server" Margin="20">
                <Items>
                    <ext:TextField ID="recordId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Name="recordId" />
                    <ext:TextField ID="reference" runat="server" FieldLabel="<%$ Resources:FieldReference%>" Name="reference" BlankText="<%$ Resources:Common, MandatoryField%>" />
                    <ext:TextField ID="firstName" runat="server" FieldLabel="<%$ Resources:FieldFirstName%>" Name="name.firstName"  AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>">
                        
                    </ext:TextField>
                    <ext:TextField ID="middleName" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:FieldMiddleName%>" Name="middleName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                    <ext:TextField ID="lastName" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:FieldLastName%>" Name="lastName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                    <ext:TextField ID="familyName" runat="server" FieldLabel="<%$ Resources:FieldFamilyName%>" Name="familyName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                    <ext:TextField ID="homeEmail" runat="server" FieldLabel="<%$ Resources:FieldHomeEmail%>" Name="homeMail" Vtype="email" BlankText="<%$ Resources:Common, MandatoryField%>" />
                    <ext:TextField ID="workEmail" runat="server" FieldLabel="<%$ Resources:FieldWorkEmail%>" Name="workMail" Vtype="email" BlankText="<%$ Resources:Common, MandatoryField%>" />
                    <ext:TextField ID="mobile" runat="server" FieldLabel="<%$ Resources:FieldMobile%>" Name="mobile" BlankText="<%$ Resources:Common, MandatoryField%>">
                        <Plugins>
                            <ext:InputMask Mask="999-999999" />
                        </Plugins>
                    </ext:TextField>
                    <ext:RadioGroup ID="gender" AllowBlank="false" runat="server" GroupName="gender" FieldLabel="<%$ Resources:FieldGender%>">
                        <Items>
                            <ext:Radio runat="server" ID="gender0" Name="gender" InputValue="0" BoxLabel="<%$ Resources:Common ,Male%>" />
                            <ext:Radio runat="server" ID="gender1" Name="gender" InputValue="1" BoxLabel="<%$ Resources:Common ,Female%>" />
                        </Items>
                    </ext:RadioGroup>
                    <ext:ComboBox ID="religionCombo" runat="server" FieldLabel="<%$ Resources:FieldReligion%>" Name="religion" IDMode="Static" SubmitValue="true">
                        <Items>
                            <ext:ListItem Text="<%$ Resources:Common, Religion0%>" Value="0"></ext:ListItem>
                            <ext:ListItem Text="<%$ Resources:Common, Religion1%>" Value="1"></ext:ListItem>
                            <ext:ListItem Text="<%$ Resources:Common, Religion2%>" Value="2"></ext:ListItem>
                            <ext:ListItem Text="<%$ Resources:Common, Religion3%>" Value="3"></ext:ListItem>
                            <ext:ListItem Text="<%$ Resources:Common, Religion4%>" Value="4"></ext:ListItem>
                            <ext:ListItem Text="<%$ Resources:Common, Religion5%>" Value="5"></ext:ListItem>
                            <ext:ListItem Text="<%$ Resources:Common, Religion6%>" Value="6"></ext:ListItem>
                        </Items>
                    </ext:ComboBox>
                    <ext:DateField
                        runat="server"
                        Name="birthDate"
                        FieldLabel="<%$ Resources:FieldDateOfBirth%>"
                        MsgTarget="Side"
                        AllowBlank="false" />
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" Margin="20">
                <Items>

                    <ext:ComboBox runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" DisplayField="name" ID="nationalityId" Name="nationalityId" FieldLabel="<%$ Resources:FieldNationality%>" SimpleSubmit="true">
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
                            <ext:Button runat="server" Icon="Add">
                            </ext:Button>
                        </RightButtons>
                    </ext:ComboBox>
                    <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="positionId" Name="positionId" FieldLabel="<%$ Resources:FieldPosition%>" SimpleSubmit="true">
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
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="departmentId" Name="departmentId" FieldLabel="<%$ Resources:FieldDepartment%>" SimpleSubmit="true">
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
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" SimpleSubmit="true">
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
                    <ext:DateField
                        runat="server"
                        Name="contractEndingDate"
                        FieldLabel="<%$ Resources:ContractEndingDate%>"
                        MsgTarget="Side"
                        AllowBlank="false" />
                    <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="sponsorId" Name="sponsorId" FieldLabel="<%$ Resources:FieldSponsor%>" SimpleSubmit="true">
                        <Store>
                            <ext:Store runat="server" ID="SponsorStore">
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
                    <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="vsId" Name="vsId" FieldLabel="<%$ Resources:FieldVacationSchedule%>" SimpleSubmit="true">
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
                    <ext:ComboBox runat="server" ID="caId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" Name="caId" FieldLabel="<%$ Resources:FieldWorkingCalendar%>" SimpleSubmit="true">
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
                    <ext:TextField ID="birthPlace" runat="server" FieldLabel="<%$ Resources:FieldBirthPlace%>" Name="placeOfBirth" />


                    <ext:Checkbox ID="isInactive" runat="server" FieldLabel="<%$ Resources: FieldIsInactive%>" Name="isInactive" InputValue="true" />
                    <ext:DateField
                        runat="server"
                        Name="hireDate"
                        FieldLabel="<%$ Resources:FieldHireDate%>"
                        MsgTarget="Side"
                        AllowBlank="false" />
                </Items>
            </ext:Panel>
           <%-- <ext:Panel runat="server" Margin="20" Visible="false">
                <Items>
                    <ext:Image runat="server" ID="imgControl" Width="200" Height="200">
                        <Listeners>
                            <Click Handler="triggierImageClick(App.picturePath.fileInputEl.id); " />
                        </Listeners>
                    </ext:Image>
                    <ext:FileUploadField ID="picturePath" runat="server" ButtonOnly="true" Hidden="true">

                        <Listeners>
                            <Change Handler="showImagePreview(App.picturePath.fileInputEl.id);" />
                        </Listeners>
                    </ext:FileUploadField>

                </Items>
            </ext:Panel>--%>
        </Items>
        <Buttons >
            
            <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                <Listeners>
                    <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
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
                    <Click Handler="this.up('window').hide();" />
                </Listeners>
            </ext:Button>
        </Buttons>
    </ext:FormPanel>
                 </Items>
</ext:Viewport>

</body>
</html>

