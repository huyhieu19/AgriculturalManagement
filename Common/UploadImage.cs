using Microsoft.AspNetCore.Http;

namespace Common
{
    public static class UploadImage
    {
        public static string UploadImageRoot(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate a unique file name (you can customize this logic)
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            // Combine the path to the uploads folder and the unique file name
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return uniqueFileName;
        }
    }
}
