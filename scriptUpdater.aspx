<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScriptUpdater.aspx.cs" Inherits="ScriptUpdater.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Script Updater</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                Select script file: 
                <asp:FileUpload ID="scriptFileUpload" runat="server" />
                <asp:Button ID="browseButton" runat="server" Text="Browse" OnClick="browseButton_Click" />
            </p>
            <p>
                <asp:Button ID="convertButton" runat="server" Text="Convert" OnClick="convertButton_Click" />
            </p>
        </div>
    </form>
</body>
</html>
