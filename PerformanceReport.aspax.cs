using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using HtmlAgilityPack;

public partial class PerformanceTrendReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Populate the application name dropdown list with the available project folders
            string performanceResultsPath = Server.MapPath(ConfigurationManager.AppSettings["PerformanceResultsPath"]);
            string[] projectFolders = Directory.GetDirectories(Path.Combine(performanceResultsPath, "Projects"));
            ApplicationNameDropDownList.DataSource = projectFolders.Select(f => new DirectoryInfo(f).Name);
            ApplicationNameDropDownList.DataBind();
        }
    }

    protected void GenerateTrendReport_Click(object sender, EventArgs e)
    {
        string applicationName = ApplicationNameDropDownList.SelectedValue;
        string timePeriod = TimePeriodDropDownList.SelectedValue;
        string responseTimeType = ResponseTimeRadioButtonList.SelectedValue;

        DateTime endDate = DateTime.Now;
        DateTime startDate;

        switch (timePeriod)
        {
            case "1 Year":
                startDate = endDate.AddYears(-1);
                break;
            case "6 Months":
                startDate = endDate.AddMonths(-6);
                break;
            case "3 Months":
                startDate = endDate.AddMonths(-3);
                break;
            case "2 Months":
                startDate = endDate.AddMonths(-2);
                break;
            case "1 Month":
            default:
                startDate = endDate.AddMonths(-1);
                break;
        }

        // Create a DataTable to hold the test data
        DataTable testData = new DataTable();
        testData.Columns.Add("Test Folder Name");

        // Iterate through each month in the selected time period
        for (DateTime date = startDate; date <= endDate; date = date.AddMonths(1))
        {
            string monthFolder = date.ToString("yyyy_MM") + date.ToString("MMM");
		string rootFolderPath = "C:\\PerformanceResults"; // replace with the actual path to the root folder
		string folderPath = Path.Combine(rootFolderPath, "Projects", applicationName, monthFolder, "Stress Testing");

           // string folderPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["PerformanceResultsPath"]), "Projects", applicationName, monthFolder, "Stress Testing");

            // Iterate through each test folder in the month folder
            foreach (string testFolderPath in Directory.GetDirectories(folderPath))
            {
                string testFolderName = Path.GetFileName(testFolderPath);
                string summaryFilePath = Path.Combine(testFolderPath, "Summary.html");

                if (File.Exists(summaryFilePath))
                {
                    // Read the Summary.html file and extract the relevant data
                    string html = File.ReadAllText(summaryFilePath);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    string transactionName = doc.DocumentNode.SelectSingleNode("//table/tr[1]/td[1]").InnerText;
                    string averageResponseTime = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[1]").InnerText;
                    string responseTime90th = doc.DocumentNode.SelectSingleNode("//table/tr[3]/td[1]").InnerText;

                    // Check if this test already exists in the DataTable
                    DataRow row = testData.Rows.Cast<DataRow>().FirstOrDefault(r => r["Test Folder Name"].ToString() == testFolderName);

                    if (row == null)
                    {
                        // Add a new row for this test
                        row = testData.NewRow();
                        row["Test Folder Name"] = testFolderName;
                        testData.Rows.Add(row);
                    }

                    // Add the test data to the DataTable
                    row[transactionName] = responseTimeType == "Average Response Time" ? averageResponseTime : responseTime90th;
                }
            }
        }

        // Bind the DataTable to a GridView
        TestGridView.DataSource = testData;
        TestGridView.DataBind();
    }
}
