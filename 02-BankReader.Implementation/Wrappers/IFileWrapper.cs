namespace BankReader.Implementation.Wrappers
{
    public interface IFileWrapper
    {
        bool Exists(string path);
    }
}