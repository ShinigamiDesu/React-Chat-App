namespace ReactBackend.Interfaces
{
    public interface FileInterface
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
