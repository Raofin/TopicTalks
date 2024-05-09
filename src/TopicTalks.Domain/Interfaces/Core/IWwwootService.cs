namespace TopicTalks.Domain.Interfaces.Core;

public interface IWwwootService
{
    string GetPath(params string[] paths);
    byte[] GetBytes(params string[] paths);
    string GetDataUri(params string[] paths);
}