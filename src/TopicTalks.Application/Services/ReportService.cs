using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class ReportService(
    IUnitOfWork unitOfWork, 
    IQuestionService questionService, 
    IExcelGenerator excelGenerator, 
    IPdfGenerator pdfGenerator) : IReportService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQuestionService _questionService = questionService;
    private readonly IExcelGenerator _excelGenerator = excelGenerator;
    private readonly IPdfGenerator _pdfGenerator = pdfGenerator;

    #region ### PDF ###

    public async Task<ErrorOr<byte[]>> QuestionWithAnswersPdfAsync(long questionId)
    {
        var question = await _questionService.GetWithAnswersAsync(questionId);

        return question.IsError 
            ? question.Errors 
            : await _pdfGenerator.QuestionPdf(question.Value);
    }

    public async Task<byte[]> UserListPdfAsync()
    {
        var users = await _unitOfWork.User.GetWithDetailsAsync();
        return await _pdfGenerator.UserListPdf(users);
    }

    #endregion
    
    #region ### Excel ###

    public async Task<ExcelFile> UserListExcelAsync()
    {
        var users = await _unitOfWork.User.GetWithDetailsAsync();
        var file = _excelGenerator.UserListExcel(users);

        return new ExcelFile(file);
    }

    #endregion
}