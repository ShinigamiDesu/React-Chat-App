using ReactBackend.Interfaces;

namespace ReactBackend.Services
{
    public class FileService : FileInterface
    {
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null) { return null; }

            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            var filePath = Path.Combine(uploads, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
