<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceTrendReport.aspx.cs" Inherits="MyApplication.PerformanceTrendReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Performance Trend Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Performance Trend Report</h1>
            <label for="ApplicationNameDropDownList">Application Name:</label>
            <asp:DropDownList ID="ApplicationNameDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ApplicationNameDropDownList_SelectedIndexChanged"></asp:DropDownList>
            <br /><br />
            <label for="TimePeriodDropDownList">Time Period:</label>
            <asp:DropDownList ID="TimePeriodDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TimePeriodDropDownList_SelectedIndexChanged">
                <asp:ListItem Text="1 Year" Value="1 Year" />
                <asp:ListItem Text="6 Months" Value="6 Months" />
                <asp:ListItem Text="3 Months" Value="3 Months" />
                <asp:ListItem Text="2 Months" Value="2 Months" />
                <asp:ListItem Text="1 Month" Value="1 Month" />
            </asp:DropDownList>
            <br /><br />
            <label>Response Time Type:</label>
            <br />
            <asp:RadioButtonList ID="ResponseTimeRadioButtonList" runat="server">
                <asp:ListItem Text="Average Response Time" Value="Average Response Time" />
                <asp:ListItem Text="90th Response Time" Value="90th Response Time" />
            </asp:RadioButtonList>
            <br /><br />
            <asp:Button ID="GenerateTrendReport" runat="server" Text="Generate Trend Report" OnClick="GenerateTrendReport_Click" />
            <br /><br />
            <asp:GridView ID="TestDataGridView" runat="server" AutoGenerateColumns="true" />
        </div>
    </form>
</body>
</html>
