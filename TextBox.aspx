<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateFile.aspx.cs" Inherits="WebApplication1.UpdateFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update File</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <textarea id="txtFileContent" runat="server"></textarea><br />
            <asp:Button ID="btnUpdateFile" runat="server" Text="Update File" OnClick="btnUpdateFile_Click" />
            <asp:Label ID="lblMessage" runat="server" />
        </div>
    </form>
</body>
</html>
