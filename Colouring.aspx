<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Script Updater</h2>
  <div style="font-weight: bold; color: #006400;">Description:</div>
  <p>Information about the Script Updater goes here.</p>
  <div style="font-weight: bold; color: #FF0000;">Pre-requisite:</div>
  <p>Information about the Pre-requisites for the Script Updater goes here.</p>
  <asp:Fieldset ID="ScriptUpdaterFieldset" runat="server">
    <!-- rest of the code -->
  </asp:Fieldset>
  <h2>Transaction Updater</h2>
  <div style="font-weight: bold; color: #006400;">Description:</div>
  <p>Information about the Transaction Updater goes here.</p>
  <div style="font-weight: bold; color: #FF0000;">Pre-requisite:</div>
  <p>Information about the Pre-requisites for the Transaction Updater goes here.</p>
  <asp:Fieldset ID="TransactionUpdaterFieldset" runat="server">
    <!-- rest of the code -->
  </asp:Fieldset>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Script Updater</h2>
  <asp:Fieldset ID="ScriptUpdaterFieldset" runat="server">
    <table>
      <tr>
        <td>
          <b style="color: lightgreen">Description: </b>
          This section allows you to upload a script file and convert it.
        </td>
      </tr>
      <tr>
        <td>
          <b style="color: red">Pre-Requisite: </b>
          The script file should be in a specific format.
        </td>
      </tr>
      <tr>
        <td>
          <asp:Accordion ID="accExample" runat="server">
            <Panes>
              <asp:AccordionPane ID="apExample" runat="server">
                <Header>Example</Header>
                <Content>
                  <p>Here is an example of the expected format for the script file:</p>
                  <code>
                    -- Example format
                  </code>
                </Content>
              </asp:AccordionPane>
            </Panes>
          </asp:Accordion>
        </td>
      </tr>
      <tr>
        <td>
          Select Script File:
        </td>
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
