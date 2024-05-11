namespace TopicTalks.Domain.Interfaces.Core;

public interface IPdfGenerator
{
    byte[] GeneratePdf(string htmlContent, bool footerDisable = false, bool showPageNumbers = false);
}