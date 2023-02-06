using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScriptUpdater
{
    public partial class Default : System.Web.UI.Page
    {
        protected void browseButton_Click(object sender, EventArgs e)
        {
            if (scriptFileUpload.HasFile)
            {
                scriptFileUpload.SaveAs(Server.MapPath("~/temp/") + scriptFileUpload.FileName);
            }
        }

        protected void convertButton_Click(object sender, EventArgs e)
        {
            string scriptFileName = Server.MapPath("~/temp/") + scriptFileUpload.FileName;
            if (File.Exists(scriptFileName))
            {
                ScriptUpdater(scriptFileName);
                ClearTempFolder();
            }
        }

        private static void ScriptUpdater(string scriptFileName)
        {
            string transactionName = "";
            string newValue1 = ",\"{VuserID} | Iteration : {iterationID} | TimeStamp : {Timestamp} | Username : {Username} | Error at step : ";
            string line;
            StringBuilder updatedScript = new StringBuilder();

            try
            {
                StreamReader sr = new StreamReader(File.OpenText(scriptFileName));

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("web_reg_find"))
                    {
                        if (line.Contains("Text="))
                        {
                            string[] parts = line.Split(new string[] { "Text=" }, StringSplitOptions.None);
                            string textCheck = parts[1].Substring(0, parts[1].IndexOf(","));
                            line = "web_reg_find(\"Text=" + textCheck + newValue1 + transactionName + "\", LAST);";

                        }
                    }
                    if (line.Contains("lr_start_transaction"))
                    {
                        string[] parts = line.Split(new string[] { "\"" }, StringSplitOptions.None);
                        transactionName = parts[1];
                    }
                    if (line.Contains("lr_end_transaction"))
                    {
                        transactionName = "";
                    }
                    updatedScript.AppendLine(line);
                }

                sr.Close();

                File.WriteAllText(scriptFileName, updatedScript.ToString());
            }
        }

        private void ClearTempFolder()
        {
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/temp/"));
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
