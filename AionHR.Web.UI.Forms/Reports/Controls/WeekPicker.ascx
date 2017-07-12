<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeekPicker.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.WeekPicker" %>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-min.js" type="text/javascript"></script>
<script src="../Scripts/weekPicker.js?id=1" type="text/javascript"></script>
<style>
    
    </style>
<script type="text/javascript">
    function dump(obj) {
        var out = '';
        for (var i in obj) {
            out += i + ": " + obj[i] + "\n";


        }
        return out;
    }
    function called(s) {
        
        App.weekControl1_week.value = s[0].value;
    };
</script>
<link href="../CSS/jquery-ui.css" rel="stylesheet" />


<ext:Panel runat="server" Layout="HBoxLayout" Width="250" >
    
    <Items>
        <ext:Hidden runat="server" ID="week" />
<ext:Container runat="server" ID="cont1">
    <Listeners>
        <AfterLayout Handler="convertToWeekPicker($('#weekPicker1')); globalAdditionalFunction =called;  globalTriggeringElement=$('#weekPicker1'); " />
    </Listeners>
    <Content>
        <div style="float:left;display:inline;" >
        <asp:Literal  runat="server" Text="<%$Resources:PickWeek %>" ></asp:Literal>
        <input type="text" id="weekPicker1"   placeholder="" />
            </div>
    </Content>
</ext:Container>

    </Items></ext:Panel>