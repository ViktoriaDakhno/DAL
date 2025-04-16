using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryApp.DAL
{
    public class ContentRepository : IContentRepository
    {
        private readonly LibraryContext _context;

        public ContentRepository(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Content content)
        {
            _context.Contents.Add(content);
            SaveChanges();
        }

        public void Remove(int contentId)
        {
            var content = _context.Contents.Find(contentId);
            if (content != null)
            {
                _context.Contents.Remove(content);
                SaveChanges();
            }
        }

        public Content? GetById(int id)
        {
            // Завантажує лише об'єкт Content за його Id
            var content = _context.Contents.Find(id);

            // Ліниве завантаження автоматично виконає запити до бази,
            // коли ви звернетеся до content.Storage або content.Tags.
            return content;
        }


        public IEnumerable<Content> GetAll()
        {
            return _context.Contents
                .Include(c => c.Storage) // Жадібне завантаження
                .Include(c => c.Tags)
                .Include(c => c.Metadata)
                .ToList();
        }

        public IEnumerable<Content> Search(string query)   
        {
            var filtered = _context.Contents
                .Where(c => c.Title.Contains(query) || c.Type.Contains(query))
                .ToList();

            foreach (var content in filtered)
            {
                _context.Entry(content).Reference(c => c.Storage).Load();
                _context.Entry(content).Collection(c => c.Tags).Load();
            }

            return filtered;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}