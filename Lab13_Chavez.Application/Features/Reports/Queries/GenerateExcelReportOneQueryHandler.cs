using ClosedXML.Excel;
using Lab13_Chavez.Application.Interfaces;
using Lab13_Chavez.Domain.Entities;
using MediatR;

namespace Lab13_Chavez.Application.Features.Reports.Queries;

internal sealed class GenerateExcelReportOneQueryHandler
    : IRequestHandler<GenerateExcelReportOneQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public GenerateExcelReportOneQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(
        GenerateExcelReportOneQuery request,
        CancellationToken cancellationToken)
    {
        List<DataV23> data = await _unitOfWork
            .Repository<DataV23>()
            .GetAllAsync();

        string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "ReportesLab13");

        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "reporte_general.xlsx");

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Reporte General");

        worksheet.Cell(1, 1).Value = "Reporte General de Datos";
        worksheet.Range("A1:E1").Merge();
        worksheet.Cell(1, 1).Style.Font.Bold = true;
        worksheet.Cell(1, 1).Style.Font.FontSize = 14;
        worksheet.Cell(1, 1).Style.Alignment.Horizontal =
            XLAlignmentHorizontalValues.Center;

        worksheet.Cell(3, 1).Value = "N°";
        worksheet.Cell(3, 2).Value = "Habitantes";
        worksheet.Cell(3, 3).Value = "Ingresos";
        worksheet.Cell(3, 4).Value = "Analfabetismo";
        worksheet.Cell(3, 5).Value = "Esperanza de Vida";

        int row = 4;
        int index = 1;

        foreach (DataV23 item in data)
        {
            string[] valores = (item.Col1 ?? "").Split(';');

            if (valores.Length >= 4 && valores[0] != "habitantes")
            {
                worksheet.Cell(row, 1).Value = index;
                worksheet.Cell(row, 2).Value = valores[0];
                worksheet.Cell(row, 3).Value = valores[1];
                worksheet.Cell(row, 4).Value = valores[2];
                worksheet.Cell(row, 5).Value = valores[3];

                row++;
                index++;
            }
        }

        var tableRange = worksheet.Range(3, 1, row - 1, 5);
        tableRange.CreateTable();

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(filePath);

        return filePath;
    }
}