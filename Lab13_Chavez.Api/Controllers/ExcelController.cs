using Lab13_Chavez.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab13_Chavez.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExcelController : ControllerBase
{
    private readonly ExcelReportService _excelReportService;

    public ExcelController(ExcelReportService excelReportService)
    {
        _excelReportService = excelReportService;
    }

    [HttpPost("crear-basico")]
    public IActionResult CreateBasicExcel()
    {
        string filePath = _excelReportService.CreateBasicExcel();

        return Ok(new
        {
            mensaje = "Archivo Excel creado correctamente.",
            ruta = filePath
        });
    }
    [HttpPut("modificar-basico")]
    public IActionResult ModifyBasicExcel()
    {
        string filePath = _excelReportService.ModifyBasicExcel();

        return Ok(new
        {
            mensaje = "Archivo Excel modificado correctamente.",
            ruta = filePath
        });
    }
    [HttpPost("crear-tabla")]
    public IActionResult CreateExcelTable()
    {
        string filePath = _excelReportService.CreateExcelTable();

        return Ok(new
        {
            mensaje = "Archivo Excel con tabla creado correctamente.",
            ruta = filePath
        });
    }
    [HttpPost("crear-formato")]
    public IActionResult CreateFormattedExcel()
    {
        string filePath = _excelReportService.CreateFormattedExcel();

        return Ok(new
        {
            mensaje = "Archivo Excel con formato creado correctamente.",
            ruta = filePath
        });
    }
}