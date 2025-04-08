using LibraryApp.DAL;
using System.Collections.Generic;

namespace LibraryApp.BLL
{
    public class ContentService
    {
        private readonly IContentRepository _contentRepository;

        public ContentService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public void AddContent(int type, string title, string format, string location, string additionalInfo, int storageId)
        {
            var content = ContentFactory.CreateContent(type, title, format, location, additionalInfo, storageId);
            //content.Tags = tags;
            _contentRepository.Add(content);
        }

        public void RemoveContent(int contentId)
        {
            _contentRepository.Remove(contentId);
        }

        public IEnumerable<Content> SearchContent(string query)
        {
            return _contentRepository.Search(query);
        }

        //public IEnumerable<Content> SearchByTag(string tagName)
        //{
        //    return _contentRepository.GetAll()
        //        .Where(c => c.Tags.Any(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase)));
        //}

        public IEnumerable<Content> GetAllContent()
        {
            return _contentRepository.GetAll();
        }
    }
}
