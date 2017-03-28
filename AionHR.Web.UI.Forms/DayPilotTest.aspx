<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayPilotTest.aspx.cs" Inherits="AionHR.Web.UI.Forms.DayPilotTest" %>
 <%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
  <asp:DropDownList runat="server" ID="drop" AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged">
                  <asp:ListItem Text="<%$ Resources:Common,January%>" Value="1" />
                                           <asp:ListItem Text="February" Value="2" />
                                           <asp:ListItem Text="March" Value="3" />
                                           <asp:ListItem Text="April" Value="4" />
                                           <asp:ListItem Text="May" Value="5" />
                                           <asp:ListItem Text="June" Value="6" />
                                           <asp:ListItem Text="July" Value="7" />
                                           <asp:ListItem Text="August" Value="8" />
                                           <asp:ListItem Text="September" Value="9" />
                                           <asp:ListItem Text="October" Value="10" />
                                           <asp:ListItem Text="November" Value="11" />
  </asp:DropDownList>
    <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server"
                            HeaderFontSize="8pt" HeaderHeight="20"
                           
                            
                            EventFontSize="11px"
                            CellDuration="1440"
                            
                           
                            EventHeight="25">
                            <Resources>
                                <DayPilot:Resource Name="Room A" Value="A" />

                            </Resources>
                        </DayPilot:DayPilotScheduler>
    </div>
    </form>
</body>
</html>
