<Query Kind="Program">
  <Connection>
    <ID>7fd3ab03-410e-4482-8822-c517085087b8</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Drawing.dll</Reference>
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>EPPlus</NuGetReference>
  <Namespace>Dapper</Namespace>
  <Namespace>OfficeOpenXml</Namespace>
  <Namespace>OfficeOpenXml.Style</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

/*
    Requires:
    Dapper
    EPPlus
        OfficeOpenXml
        OfficeOpenXml.Style

    This code generates an excel file from a SQL statement with or without parameters using Dapper and EPPlus.
    The column names in the SQL statement are used as the header.
    Assumes a connection is set in LINQPad.
*/

void Main()
{
    var report = GetReport();

    // report folder
    string reportDir = @"C:\Temp\";
    string reportName = report.FirstOrDefault().Name;

    // file setup
    var file = FileSetup(reportDir, reportName);

    // excel package using file
    using (ExcelPackage package = new ExcelPackage(file)) {
        StringBuilder sb = new StringBuilder();

        // loop through the report object, fetch the data, and create sheets
        int i = 0;
        foreach (var item in report)
        {
            i++;
            var data = GetData(item);
            CreateNewSheet(package, item.SheetName, "Table" + i.ToString(), data);
            sb.AppendFormat("Added new sheet: {0}", item.SheetName);
            sb.Append(Environment.NewLine);
        }

        // write the excel doc to the file
        package.Save();

        sb.Append(Environment.NewLine);
        sb.AppendFormat("{0}.xlsx created!", reportName);

        // print a happy message
        Console.WriteLine(sb.ToString());
    }
}

List<Report> GetReport() {
    List<Report> reportList = new List<Report>();
    reportList.Add(new Report() {
        Name = "Server-Principals-" + DateTime.Now.ToString("yyyy-MM-dd"),
        SheetName = "Sheet 1",
        SQLQuery = @"-- Server Principals
SELECT	principal_id AS [Principal ID],
		name AS [User],
		type_desc AS [Type Description],
		is_disabled AS [Is Disabled] 
FROM sys.server_principals"
    });
    return reportList;
}

// report class
class Report
{
    public string Name {get; set;}
    public string SQLQuery {get; set;}
    public object SQLArgs {get; set;}
    public string SheetName {get; set;}
}

// file setup
FileInfo FileSetup(string reportDir, string reportName) {
    StringBuilder sb = new StringBuilder();
    sb.AppendFormat(@"{0}{1}.xlsx", reportDir, reportName);
    if (File.Exists(sb.ToString())) {
        try {
            File.Delete(sb.ToString());
        } catch (IOException e) {
            Console.WriteLine(e.Message);
        }
    }
    FileInfo file = new FileInfo(sb.ToString());
    return file;
}

// dynamic data call
List<dynamic> GetData(Report rpt) {
    using (var connection = new SqlConnection(this.Connection.ConnectionString)) {
        connection.Open();
        return SqlMapper.Query<dynamic>(connection, rpt.SQLQuery, rpt.SQLArgs).ToList();
    }
}

// excel
void CreateNewSheet(ExcelPackage excelPackage, string sheetName, string tableName, List<dynamic> sheetData) {
    int row = 1, col = 1;
    int properties = 0;

    excelPackage.Workbook.Worksheets.Add(sheetName);
    ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[sheetName];
    sheet.Name = sheetName;
    sheet.Cells.Style.Font.Size = 11;
    sheet.Cells.Style.Font.Name = "Calibri";

    // add header
    foreach (var item in sheetData)
    {
        foreach (var key in item)
        {
            sheet.Cells[row, col].Value = key.Key;
            sheet.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(91, 155, 213));
            sheet.Cells[row, col].Style.Font.Color.SetColor(Color.White);
            sheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            properties++;
            col++;
        }
        break;
    }

    // start on row 2, col 1
    row = 2;

    // add data
    foreach (var item in sheetData)
    {
        // start on col 1
        col = 1;
        foreach (var key in item)
        {
            var value = key.Value;
            sheet.Cells[row, col].Value = value;

            if (value.GetType() == typeof(DateTime)) {
                sheet.Cells[row, col].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
            }

            // alternate row background color
            if (row % 2 == 0)
            {
                sheet.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }

            col++;
        }
        row++;
    }

    // style data as a table
    if (properties > 0) {
        using (var range = sheet.Cells[1, 1, row-1, properties]) {
            var table = sheet.Tables.Add(range, tableName);
            table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium16;
            range.AutoFitColumns();
        }
    }
}