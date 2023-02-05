<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionUpdater.aspx.cs" Inherits="TransactionUpdater" %>

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
                    <td>Application CI Code:</td>
                    <td><asp:TextBox ID="appCICodeField" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Script Name:</td>
                    <td><asp:TextBox ID="scriptNameField" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Functionality Name:</td>
                    <td><asp:TextBox ID="funcNameField" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Button ID="updateButton" runat="server" Text="Update Transaction" OnClick="updateButton_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
