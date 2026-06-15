using ClosedXML.Excel;
using Lab13_Chavez.Application.Interfaces;
using Lab13_Chavez.Domain.Entities;
using MediatR;

namespace Lab13_Chavez.Application.Features.Reports.Queries;

internal sealed class GenerateExcelReportTwoQueryHandler
    : IRequestHandler<GenerateExcelReportTwoQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public GenerateExcelReportTwoQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(
        GenerateExcelReportTwoQuery request,
        CancellationToken cancellationToken)
    {
        List<DataV23> data = await _unitOfWork
            .Repository<DataV23>()
            .GetAllAsync();

        string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "ReportesLab13");

        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "reporte_resumen.xlsx");

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Resumen");

        worksheet.Cell(1, 1).Value = "Resumen de Registros";
        worksheet.Range("A1:B1").Merge();
        worksheet.Cell(1, 1).Style.Font.Bold = true;
        worksheet.Cell(1, 1).Style.Font.FontSize = 14;
        worksheet.Cell(1, 1).Style.Alignment.Horizontal =
            XLAlignmentHorizontalValues.Center;

        worksheet.Cell(3, 1).Value = "Descripción";
        worksheet.Cell(3, 2).Value = "Valor";

        worksheet.Cell(4, 1).Value = "Total de registros";
        worksheet.Cell(4, 2).Value = data.Count;

        worksheet.Cell(5, 1).Value = "Fecha de generación";
        worksheet.Cell(5, 2).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        worksheet.Range("A3:B5").CreateTable();

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(filePath);

        return filePath;
    }
}