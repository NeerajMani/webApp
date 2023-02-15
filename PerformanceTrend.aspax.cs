using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;
using System.Diagnostics;
using System.Drawing;

public partial class PerformanceTrendReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlApplicationName.Items.Add(new ListItem("Select Application Name", "0"));
            //string rootPath = Server.MapPath("~");
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["PerformanceResultsPath"]), "Projects"));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                ddlApplicationName.Items.Add(new ListItem(dir.Name, dir.Name));
            }
        }
    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        if (ddlApplicationName.SelectedValue == "0")
        {
            Response.Write("<script>alert('Please select an application name');</script>");
            return;
        }

        if (string.IsNullOrEmpty(rblResponseTime.SelectedValue))
        {
            Response.Write("<script>alert('Please select a response time');</script>");
            return;
        }

        int timePeriod = Convert.ToInt32(ddlTimePeriod.SelectedValue);
        string responseTimeType = rblResponseTime.SelectedValue;

        DateTime fromDate = DateTime.Today.AddMonths(-timePeriod).AddDays(1 - DateTime.Today.Day);
        DateTime toDate = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddTicks(-1);

        string folderPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["PerformanceResultsPath"]), "Projects", ddlApplicationName.SelectedValue);

        List<Dictionary<string, string>> performanceData = new List<Dictionary<string, string>>();

        for (DateTime month = fromDate; month <= toDate; month = month.AddMonths(1))
        {
            string monthFolder = month.ToString("yyyy_MM") + month.ToString("MMM");
            string monthFolderPath = Path.Combine(folderPath, monthFolder, "Stress Testing");

            if (Directory.Exists(monthFolderPath))
            {
                DirectoryInfo monthDirectoryInfo = new DirectoryInfo(monthFolderPath);

                foreach (DirectoryInfo testFolder in monthDirectoryInfo.GetDirectories())
                {
                    string summaryFilePath = Path.Combine(testFolder.FullName, "Summary.html");
                    if (File.Exists(summaryFilePath))
                    {
                        HtmlDocument doc = new HtmlDocument();
                        doc.Load(summaryFilePath);

                        string transactionName = GetInnerText(doc.DocumentNode.SelectSingleNode("//table/tr[1]/td[1]"));
                        string averageResponseTime = GetInnerText(doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[1]"));
                        string responseTime90th = GetInnerText(doc.DocumentNode.SelectSingleNode("//table/tr[3]/td[1]"));

                        DateTime reportDate = Convert.ToDateTime(GetInnerText(doc.DocumentNode.SelectSingleNode("//table/tr[1]/td[2]")));
                        if (reportDate >= fromDate && reportDate <= toDate)
                        {
                            Dictionary<string, string> data = performanceData.Find(d => d["Transaction Name"] == transactionName);

                            if (data == null)
                            {
                                data = new Dictionary<string, string>();
                                data.Add("Transaction Name", transactionName);
                                data.Add(responseTimeType, responseTimeType == "Average Response Time" ? averageResponseTime : responseTime90th);
                                performanceData.Add(data);
                            }
                            else
                            {
                                data[responseTimeType] = responseTimeType == "Average Response Time" ? averageResponseTime : responseTime90th;
                            }
                        }
                    }
                }
            }
        }

        if (performanceData.Count == 0)
       
{
            Response.Write("<script>alert('No data found for the selected criteria');</script>");
            return;
        }

        // Generate the report
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Html);

        // Generate the header row
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Head);
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Style);
        htmlWriter.Write(".report-table {border-collapse: collapse;}");
        htmlWriter.Write(".report-table th, .report-table td {border: 1px solid black; padding: 5px;}");
        htmlWriter.Write(".report-table th {background-color: #dddddd;}");
        htmlWriter.RenderEndTag(); // End Style
        htmlWriter.RenderEndTag(); // End Head

        // Generate the body
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Body);
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);
        htmlWriter.AddAttribute("class", "report-table");
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Thead);
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
        htmlWriter.Write("Transaction Name");
        htmlWriter.RenderEndTag(); // End Th
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
        htmlWriter.Write(responseTimeType);
        htmlWriter.RenderEndTag(); // End Th
        htmlWriter.RenderEndTag(); // End Tr
        htmlWriter.RenderEndTag(); // End Thead

        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tbody);
        foreach (Dictionary<string, string> data in performanceData)
        {
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            htmlWriter.Write(data["Transaction Name"]);
            htmlWriter.RenderEndTag(); // End Td
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            htmlWriter.Write(data[responseTimeType]);
            htmlWriter.RenderEndTag(); // End Td
            htmlWriter.RenderEndTag(); // End Tr
        }
        htmlWriter.RenderEndTag(); // End Tbody

        htmlWriter.RenderEndTag(); // End Table
        htmlWriter.RenderEndTag(); // End Body
        htmlWriter.RenderEndTag(); // End Html

        Response.ClearContent();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=PerformanceReport.html");
        Response.Write(stringWriter.ToString());
        Response.End();
    }

    private string GetInnerText(HtmlNode node)
    {
        return node != null ? node.InnerText : string.Empty;
    }
}

<configuration>
  <appSettings>
    <add key="PerformanceResultsPath" value="C:\Results\" />
  </appSettings>
</configuration>

