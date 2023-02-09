if (line.Contains("lr_start_transaction"))
{
    int startIndex = line.IndexOf("lr_start_transaction") + 21;
    int endIndex = line.IndexOf("\"", startIndex + 1);
    string transactionName = line.Substring(startIndex + 1, endIndex - startIndex - 1);
    
    // Check if the transaction name has the required format
    string[] parts = transactionName.Split('_');
    if (parts.Length >= 4)
    {
        // Capture the TransactionName part
        transactionName = txtAppCICode.Text + "_" + txtScriptName.Text + "_"
                + txtFuncName.Text + "_" + string.Format("{0:D2}", transactionNum) + "_"
                + parts[parts.Length - 1];
    }
    else
    {
        // Update the transaction name with the new format
        transactionName = txtAppCICode.Text + "_" + txtScriptName.Text + "_"
                + txtFuncName.Text + "_" + string.Format("{0:D2}", transactionNum) + "_"
                + transactionName;
    }
    line = line.Substring(0, startIndex + 1) + transactionName + line.Substring(endIndex);
    
    // Increment the transaction number
    transactionNum++;
}
if (line.Contains("lr_end_transaction"))
{
    int startIndex = line.IndexOf("lr_end_transaction") + 19;
    int endIndex = line.IndexOf("\"", startIndex + 1);
    string transactionName = line.Substring(startIndex + 1, endIndex - startIndex - 1);
    
    // Check if the transaction name has the required format
    string[] parts = transactionName.Split('_');
    if (parts.Length >= 5)
    {
        // Capture the TransactionName part
        transactionName = txtAppCICode.Text + "_" + txtScriptName.Text + "_"
                + txtFuncName.Text + "_" + string.Format("{0:D2}", transactionNum) + "_"
                + parts[parts.Length - 1];
    }
    else
    {
        // Update the transaction name
        transactionName = txtAppCICode.Text + "_" + txtScriptName.Text + "_"
                + txtFuncName.Text + "_" + string.Format("{0:D2}", transactionNum - 1) + "_"
                + transactionName;
    }
    line = line.Substring(0, startIndex + 1) + transactionName + line.Substring(endIndex);
}
updatedFile += line + "\n";
