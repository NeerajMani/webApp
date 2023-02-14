protected void GenerateTrendReport_Click(object sender, EventArgs e)
{
    string applicationName = ApplicationName.SelectedValue;
    string responseTimeType = ResponseTimeType.SelectedValue;
    string timePeriod = TimePeriod.SelectedValue;

    DateTime fromDate = DateTime.Now;
    switch (timePeriod)
    {
        case "1 Year":
            fromDate = DateTime.Now.AddYears(-1);
            break;
        case "6 Months":
            fromDate = DateTime.Now.AddMonths(-6);
            break;
        case "3 Months":
            fromDate = DateTime.Now.AddMonths(-3);
            break;
        case "2 Months":
            fromDate = DateTime.Now.AddMonths(-2);
            break;
        case "1 Month":
            fromDate = DateTime.Now.AddMonths(-1);
            break;
        default:
            break;
    }

    DataTable dt = new DataTable();
    dt.Columns.Add("Transaction Name", typeof(string));

    List<DateTime> months = new List<DateTime>();
    for (DateTime date = fromDate; date <= DateTime.Now; date = date.AddMonths(1))
    {
        months.Add(date);
        dt.Columns.Add(date.ToString("MMM-yyyy"), typeof(string));
    }

    string folderPath = RootFolderPath.Text;
    folderPath = Path.Combine(folderPath, "PerformanceResults", "Projects", applicationName);

    foreach (DateTime month in months)
    {
        string monthFolder = month.ToString("yyyy_MM") + month.ToString("MMM", CultureInfo.InvariantCulture);

        string[] testFolders = Directory.GetDirectories(Path.Combine(folderPath, monthFolder, "Stress Testing"));

        foreach (string testFolder in testFolders)
        {
            string summaryPath = Path.Combine(testFolder, "Summary.html");

            if (!File.Exists(summaryPath))
            {
                continue;
            }

            string[] summaryLines = File.ReadAllLines(summaryPath);
            if (summaryLines.Length < 4)
            {
                continue;
            }

            string transactionName = summaryLines[1].Split(',')[0];
            string averageResponseTime = summaryLines[2].Split(',')[0];
            string responseTime90th = summaryLines[3].Split(',')[0];

            DataRow row = dt.AsEnumerable().FirstOrDefault(r => r.Field<string>("Transaction Name") == transactionName);
            if (row == null)
            {
                row = dt.NewRow();
                row["Transaction Name"] = transactionName;
                dt.Rows.Add(row);
            }

            string column = month.ToString("MMM-yyyy");
            row[column] = responseTimeType == "Average Response Time" ? averageResponseTime : responseTime90th;
        }
    }

    GridView1.DataSource = dt;
    GridView1.DataBind();
}
