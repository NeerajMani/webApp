<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScriptAndTransactionUpdater.aspx.cs" Inherits="ScriptAndTransactionUpdater" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Script and Transaction Updater</title>
  <script type="text/javascript">
    function validateUpload() {
      var scriptFile = document.getElementById("<%= scriptFileUpload.ClientID %>");
      var browseBtn = document.getElementById("<%= browseButton.ClientID %>");
      var convertBtn = document.getElementById("<%= convertButton.ClientID %>");
      if (scriptFile.value == "") {
        convertBtn.disabled = true;
        return false;
      }
      convertBtn.disabled = false;
      return true;
    }

    function validateTransaction() {
      var appCode = document.getElementById("<%= appCICodeField.ClientID %>");
      var scriptName = document.getElementById("<%= scriptNameField.ClientID %>");
      var funcName = document.getElementById("<%= funcNameField.ClientID %>");
      var updateBtn = document.getElementById("<%= updateButton.ClientID %>");
      if (appCode.value == "" || scriptName.value == "" || funcName.value == "") {
        updateBtn.disabled = true;
        return false;
      }
      updateBtn.disabled = false;
      return true;
    }
  </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Script Updater</h2>
  <asp:Fieldset ID="ScriptUpdaterFieldset" runat="server">
    <table>
      <tr>
        <td>Select Script File:</td>
        <td>
          <asp:FileUpload ID="scriptFileUpload" runat="server" onchange="validateUpload();" />
          <asp:Button ID="browseButton" runat="server" Text="Browse" OnClick="browseButton_Click" />
        </td>
      </tr>
      <tr>
        <td colspan="2" align="center">
          <asp:Button ID="convertButton" runat="server" Text="Convert" OnClick="convertButton_Click" Disabled="True" />
        </td>
      </tr>
    </table>
  </asp:Fieldset>
  <h2>Transaction Updater</h2>
  <asp:Fieldset ID="TransactionUpdaterFieldset" runat="server">
    <table>
      <tr>
        <td>Application CI Code:</td>
        <td>
          <asp:TextBox ID="appCICodeField" runat="server" onkeyup="validateTransaction();" />
        </td>
      </tr>
      <tr>
        <td>Script Name:</td>
        <td>
          <asp:TextBox ID="scriptNameField" runat="server" onkeyup="validateTransaction();" />
        </td>
      </tr>
      <tr>
        <td>Functionality Name:</td>
        <td>
          <asp:TextBox ID="funcNameField" runat="server" onkeyup="validateTransaction();" />
        </td>
      </tr>
      <tr>
        <td colspan="2" align="center">
          <asp:Button ID="updateButton" runat="server" Text="Update Transaction" OnClick="updateButton_Click" 
			Enabled="false" ClientIDMode="Static" />
        </td>
      </tr>
    </table>
  </asp:Fieldset>
</asp:Content>
