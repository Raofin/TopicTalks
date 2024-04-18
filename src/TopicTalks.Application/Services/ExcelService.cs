using TopicTalks.Application.Dtos;
using TopicTalks.Domain;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class ExcelService(IUnitOfWork unitOfWork, IExcelGenerator excelGenerator) : IExcelService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IExcelGenerator _excelGenerator = excelGenerator;

    public async Task<ExcelFile> UserListExcelFile()
    {
        var users = await _unitOfWork.User.GetWithDetailsAsync();
        var file = _excelGenerator.UserListExcel(users);

        return new ExcelFile(file);
    }
}