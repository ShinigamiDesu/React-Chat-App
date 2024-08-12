namespace ReactBackend.Interfaces
{
    public interface FileInterface
    {
        Task<byte[]> ConvertToByteArrayAsync(IFormFile file);
    }
}
