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
            _context.SaveChanges();
        }

        public void Remove(int contentId)
        {
            var content = _context.Contents.Find(contentId);
            if (content != null)
            {
                _context.Contents.Remove(content);
                _context.SaveChanges();
            }
        }

        public Content GetById(int id) => _context.Contents.Find(id);

        public IEnumerable<Content> GetAll() => _context.Contents.ToList();

        public IEnumerable<Content> Search(string query)
        {
            return _context.Contents
                .Where(c => c.Title.Contains(query) || c.Type.Contains(query))
                .ToList();
        }
    }
}
