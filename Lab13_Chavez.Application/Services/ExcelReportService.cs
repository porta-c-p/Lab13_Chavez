using ClosedXML.Excel;

namespace Lab13_Chavez.Application.Services;

public class ExcelReportService
{
    public string CreateBasicExcel()
    {
        string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "ReportesLab13");

        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "archivo.xlsx");

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Hoja1");

        worksheet.Cell(1, 1).Value = "Nombre";
        worksheet.Cell(1, 2).Value = "Edad";

        worksheet.Cell(2, 1).Value = "Juan";
        worksheet.Cell(2, 2).Value = 28;

        workbook.SaveAs(filePath);

        return filePath;
    }
    public string ModifyBasicExcel()
    {
        string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "ReportesLab13");

        string filePath = Path.Combine(folderPath, "archivo.xlsx");

        if (!File.Exists(filePath))
        {
            CreateBasicExcel();
        }

        using var workbook = new XLWorkbook(filePath);

        var worksheet = workbook.Worksheet(1);

        worksheet.Cell(2, 2).Value = 30;

        workbook.Save();

        return filePath;
    }
    public string CreateExcelTable()
    {
        string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "ReportesLab13");

        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "tabla.xlsx");

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Datos");

        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Nombre";
        worksheet.Cell(1, 3).Value = "Edad";

        worksheet.Cell(2, 1).Value = 1;
        worksheet.Cell(2, 2).Value = "Juan";
        worksheet.Cell(2, 3).Value = 28;

        worksheet.Cell(3, 1).Value = 2;
        worksheet.Cell(3, 2).Value = "Maria";
        worksheet.Cell(3, 3).Value = 34;

        var range = worksheet.Range("A1:C3");
        range.CreateTable();

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(filePath);

        return filePath;
    }
    public string CreateFormattedExcel()
    {
        string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "ReportesLab13");

        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "archivo_con_estilo.xlsx");

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Estilos");

        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Nombre";
        worksheet.Cell(1, 3).Value = "Edad";

        worksheet.Cell(2, 1).Value = 1;
        worksheet.Cell(2, 2).Value = "Juan";
        worksheet.Cell(2, 3).Value = 28;

        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.Cyan;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(filePath);

        return filePath;
    }
}