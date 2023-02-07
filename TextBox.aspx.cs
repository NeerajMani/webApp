using System;
using System.Web.UI;

namespace WebApplication1
{
    public partial class UpdateFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdateFile_Click(object sender, EventArgs e)
        {
            string fileContent = txtFileContent.Text;
            string updatedFile = "";
            try
            {
                // Your update logic here
                updatedFile = fileContent + " (updated)";

                byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(updatedFile);
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=UpdatedFile.txt");
                Response.Buffer = true;
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.BinaryWrite(fileBytes);
                Response.End();
                Response.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating file: " + ex.Message;
            }
        }
    }
}
