<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScriptUpdater.aspx.cs" Inherits="ScriptUpdater.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Script Updater</title>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center">
            <tr>
                <td>
                    Select script file:
                </td>
                <td>
                    <asp:FileUpload ID="scriptFileUpload" runat="server" />
                    <asp:RequiredFieldValidator ID="scriptFileUploadValidator" runat="server" ControlToValidate="scriptFileUpload" ErrorMessage="Please select a script file." Display="Dynamic" />
                </td>
                <td>
                    <asp:Button ID="browseButton" runat="server" Text="Browse" OnClick="browseButton_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table align="center">
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="convertButton" runat="server" Text="Convert" OnClick="convertButton_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
