using MediatR;

namespace Lab13_Chavez.Application.Features.Reports.Queries;

public sealed record GenerateExcelReportOneQuery : IRequest<string>;