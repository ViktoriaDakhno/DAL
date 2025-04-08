using System.Collections.Generic;

namespace LibraryApp.DAL
{
    public interface IContentRepository
    {
        void Add(Content content);
        void Remove(int contentId);
        Content GetById(int id);
        IEnumerable<Content> GetAll();
        IEnumerable<Content> Search(string query);
        void SaveChanges();
    }
}
