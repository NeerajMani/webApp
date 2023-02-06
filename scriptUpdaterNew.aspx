<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScriptUpdater.aspx.cs" Inherits="ScriptUpdater.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Script Updater</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>Script Updater</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" />

    <fieldset>
        <legend>Upload File</legend>
        <table>
            <tr>
                <td>
                    Select script file: 
                </td>
                <td>
                    <asp:FileUpload ID="scriptFileUpload" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="browseButton" runat="server" Text="Browse" OnClick="browseButton_Click" />
                </td>
            </tr>
        </table>
    </fieldset>

    <fieldset>
        <legend>Actions</legend>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="convertButton" runat="server" Text="Convert" OnClick="convertButton_Click" 
                        ValidationGroup="scriptUpload"/>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
