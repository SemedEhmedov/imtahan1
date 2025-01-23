namespace WebApplication2.Helpers.Extensions
{
    public static class FileExtensions
    {
        public static string Upload(this IFormFile File, string rootpath, string foldername)
        {
            string filename = File.FileName;
            if (filename.Length > 64)
            {
                filename = filename.Substring(filename.Length - 64, 64);
            }
            filename = Guid.NewGuid() + filename;
            string path = Path.Combine(rootpath, foldername, filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                File.CopyTo(stream);
            }
            return filename;
        }
        public static bool DeleteFile(string rootpath, string foldername, string filename)
        {
            string path = Path.Combine(rootpath, foldername, filename);
            if (!File.Exists(path))
            {
                return false;
            }
            File.Delete(path);
            return true;
        }
    }
}
