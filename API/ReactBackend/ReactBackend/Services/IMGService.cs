using ReactBackend.Interfaces;

namespace ReactBackend.Services
{
    public class IMGService : FileInterface
    {
        public async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
