namespace TopicTalks.Domain.Interfaces.Core;

public interface IPdfGenerator
{
    byte[] GeneratePdf(string htmlContent);
}