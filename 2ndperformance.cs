protected void btnGenerateTrendReport_Click(object sender, EventArgs e)
{
    string applicationName = ddlApplication.SelectedValue;
    string responseTimeType = rdbtnAvgResponseTime.Checked ? "Average Response Time" : "90th Percentile Response Time";
    string timePeriod = ddlTimePeriod.SelectedValue;

    DateTime startDate;
    switch (timePeriod)
    {
        case "Last Year":
            startDate = DateTime.Now.AddYears(-1);
            break;
        case "Last 6 Months":
            startDate = DateTime.Now.AddMonths(-6);
            break;
        case "Last 3 Months":
            startDate = DateTime.Now.AddMonths(-3);
            break;
        case "Last 2 Months":
            startDate = DateTime.Now.AddMonths(-2);
            break;
        case "Last Month":
            startDate = DateTime.Now.AddMonths(-1);
            break;
        default:
            return;
    }

    DataTable reportTable = new DataTable();
    reportTable.Columns.Add("Transaction Name");

    // Loop through each monthly folder for the selected time period
    for (DateTime month = startDate; month <= DateTime.Now; month = month.AddMonths(1))
    {
        string monthFolder = month.ToString("yyyy_MM") + "Jan";
        string folderPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["PerformanceResultsPath"]), "Projects", applicationName, monthFolder, "Stress Testing");

        // Loop through each test folder in the monthly folder
        foreach (string testFolderPath in Directory.GetDirectories(folderPath))
        {
            string summaryFilePath = Path.Combine(testFolderPath, "Summary.html");
            if (File.Exists(summaryFilePath))
            {
                // Read the summary file and extract the transaction name and response time
                HtmlDocument doc = new HtmlDocument();
                doc.Load(summaryFilePath);

                string transactionName = doc.DocumentNode.SelectSingleNode("//table/tr[1]/td[1]").InnerText;
                string responseTime = responseTimeType == "Average Response Time" ? doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[1]").InnerText : doc.DocumentNode.SelectSingleNode("//table/tr[3]/td[1]").InnerText;

                // Check if this transaction name already exists in the report table
                DataRow row = reportTable.Rows.Cast<DataRow>().FirstOrDefault(r => r["Transaction Name"].ToString() == transactionName);
                if (row == null)
                {
                    // Add a new row to the report table
                    row = reportTable.NewRow();
                    row["Transaction Name"] = transactionName;
                    reportTable.Rows.Add(row);
                }

                // Add the response time to the report table
                string columnName = month.ToString("MMM yyyy");
                if (!reportTable.Columns.Contains(columnName))
                {
                    reportTable.Columns.Add(columnName);
                }
                row[columnName] = responseTime;
            }
        }
    }

    // Bind the report table to the GridView
    gvTrendReport.DataSource = reportTable;
    gvTrendReport.DataBind();
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;

public partial class PerformanceTrendReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlApplicationName.Items.Add(new ListItem("Select Application Name", "0"));
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

                        string transactionName = doc.DocumentNode.SelectSingleNode("//table/tr[1]/td[1]").InnerText;
                        string averageResponseTime = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[1]").InnerText;
                        string responseTime90th = doc.DocumentNode.SelectSingleNode("//table/tr[3]/td[1]").InnerText;

                        DateTime reportDate = Convert.ToDateTime(doc.DocumentNode.SelectSingleNode("//table/tr[1]/td[2]").InnerText);
                        if (reportDate >= fromDate && reportDate <= toDate)
                        {
                            Dictionary<string, string> data = performanceData.FirstOrDefault(d => d["Transaction Name"] == transactionName);

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
            Response.Write("<script>alert('No performance data found for the selected time period.');</script>");
        return;
    }

    // create a DataTable to store the aggregated performance data
    var dataTable = new System.Data.DataTable();
    dataTable.Columns.Add("Transaction Name");
    dataTable.Columns.Add(responseTimeType);

    // populate the DataTable with the aggregated performance data
    foreach (var data in performanceData)
    {
        var row = dataTable.NewRow();
        row["Transaction Name"] = data["Transaction Name"];
        row[responseTimeType] = data[responseTimeType];
        dataTable.Rows.Add(row);
    }

    // bind the DataTable to the GridView to display the aggregated performance data
    gvPerformanceData.DataSource = dataTable;
    gvPerformanceData.DataBind();
}
}
