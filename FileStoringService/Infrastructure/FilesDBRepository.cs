using FileStoringService.Models;

namespace FileStoringService.Infrastructure
{
    public class FilesDBRepository
    {
        public FileText GetFile(int id)
        {
            using (FilesDBContext db = new FilesDBContext())
            {
                return db.Find<FileText>(id);
            }
        }

        public List<FileText> GetFiles()
        {
            using (FilesDBContext db = new FilesDBContext())
            {
                return db.Files.ToList();
            }
        }

        public void AddFile(FileText file)
        {
            using (FilesDBContext db = new FilesDBContext())
            {
                db.Add(file);
                db.SaveChanges();
            }
        }
    }
}
