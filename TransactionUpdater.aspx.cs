using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransactionUpdater : System.Web.UI.Page
{
protected void Page_Load(object sender, EventArgs e)
{}

protected void updateButton_Click(object sender, EventArgs e)
{
    if (FileUpload1.HasFile)
    {
        try
        {
            string filePath = FileUpload1.FileName;
            StreamReader reader = new StreamReader(FileUpload1.FileContent);

            int transactionNum = 1;
            string updatedFile = "";
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("lr_start_transaction"))
                {
                    int startIndex = line.IndexOf("lr_start_transaction") + 20;
                    int endIndex = line.IndexOf("\"", startIndex + 1);
                    string transactionName = line.Substring(startIndex + 1, endIndex - startIndex - 1);

                    // Update the transaction name
                    transactionName = txtAppCICode.Text + "_" + txtScriptName.Text + "_"
                            + txtFuncName.Text + "_" + string.Format("{0:D2}", transactionNum) + "_"
                            + transactionName;
                    line = line.Substring(0, startIndex + 1) + transactionName + line.Substring(endIndex);

                    // Increment the transaction number
                    transactionNum++;
                }
                if (line.Contains("lr_end_transaction"))
                {
                    int startIndex = line.IndexOf("lr_end_transaction") + 18;
                    int endIndex = line.IndexOf("\"", startIndex + 1);
                    string transactionName = line.Substring(startIndex + 1, endIndex - startIndex - 1);
                    // Update the transaction name
                    transactionName = txtAppCICode.Text + "_" + txtScriptName.Text + "_"
                            + txtFuncName.Text + "_" + string.Format("{0:D2}", transactionNum - 1) + "_"
                            + transactionName;
                    line = line.Substring(0, startIndex + 1) + transactionName + line.Substring(endIndex);
                }
                updatedFile += line + "\n";
            }
            reader.Close();

            // Write the updated file as the output
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(updatedFile);
            writer.Close();
            
           /*
           // Write the updated file as the output
            byte[] fileBytes = Encoding.UTF8.GetBytes(updatedFile);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(fileBytes);
            Response.End();
            Response.Close();
           
           */
            lblMessage.Text = "File updated successfully!";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error updating file: " + ex.Message;
        }
    }
    else
    {
        lblMessage.Text = "Please select a file to update.";
    }
}
}
