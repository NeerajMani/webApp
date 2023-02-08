<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkloadModelUI.aspx.cs" Inherits="WorkloadModelUI" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="vusersLabel" runat="server" Text="No of Total Vusers"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="vusersField" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="tphLabel" runat="server" Text="TPH value"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tphField" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="scriptExecutionTimeLabel" runat="server" Text="Script execution time in seconds"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="baselineField" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="thinkTimeLabel" runat="server" Text="Total think time in seconds: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="thinkTimeField" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="pacingLabel" runat="server" Text="Pacing value (This is fixed pacing between iteration - 2nd Option in Runtime Settings)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="pacingField" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="pacingLabelNew" runat="server" Text="Pacing value (This is fixed pacing for interval - 3rd Option in Runtime Settings)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="pacingFieldNew" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="calculateButton" runat="server" Text="Calculate" OnClick="CalculateButton_Click" />
                        <asp:Button ID="clearButton" runat="server" Text="Clear" OnClick="ClearButton_Click" />
                    </td>
                </tr>
