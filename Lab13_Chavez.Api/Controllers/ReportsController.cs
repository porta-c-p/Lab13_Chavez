using Lab13_Chavez.Application.Features.Reports.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab13_Chavez.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("excel/reporte-general")]
    public async Task<IActionResult> GenerateReportOne()
    {
        string filePath = await _mediator.Send(new GenerateExcelReportOneQuery());

        return Ok(new
        {
            mensaje = "Reporte general generado correctamente.",
            ruta = filePath
        });
    }

    [HttpPost("excel/reporte-resumen")]
    public async Task<IActionResult> GenerateReportTwo()
    {
        string filePath = await _mediator.Send(new GenerateExcelReportTwoQuery());

        return Ok(new
        {
            mensaje = "Reporte resumen generado correctamente.",
            ruta = filePath
        });
    }
}