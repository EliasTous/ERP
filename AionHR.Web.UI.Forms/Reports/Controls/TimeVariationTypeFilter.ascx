<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeVariationTypeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.TimeVariationTypeFilter" %>

<ext:Panel runat="server" Layout="HBoxLayout" Width="190"  ><Items>
    
                              
<ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="timeCode" Editable="false" Width="190" FieldLabel="<%$ Resources:Common,TimeVariationType %>" LabelWidth="80">
    <Items>
        
                        <ext:ListItem Text="<%$ Resources:Common ,  UnpaidLeaves %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_UNPAID_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  PaidLeaves %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_PAID_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  SHIFT_LEAVE_WITHOUT_EXCUSE %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_SHIFT_LEAVE_WITHOUT_EXCUSE %>" ></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  DAY_LEAVE_WITHOUT_EXCUSE  %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_DAY_LEAVE_WITHOUT_EXCUSE %>" ></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  LATE_CHECKIN %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_LATE_CHECKIN %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  DURING_SHIFT_LEAVE %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_DURING_SHIFT_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  EARLY_LEAVE   %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_EARLY_LEAVE   %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  EARLY_CHECKIN %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_EARLY_CHECKIN %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  OVERTIME %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_OVERTIME %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  MISSED_PUNCH %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_MISSED_PUNCH %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Common , COUNT %>" Value="<%$ Resources: ComboBoxValues , TimeVariationType_COUNT %>"></ext:ListItem>
    
    </Items>

</ext:ComboBox>
</Items></ext:Panel>