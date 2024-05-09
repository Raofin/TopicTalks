namespace TopicTalks.Domain.Interfaces.Core;

public interface IWwwootService
{
    string GetPath(string fileName);
    byte[] GetBytes(string fileName);
    string GetDataUri(string fileName);
}