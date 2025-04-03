using LibraryApp.DAL;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace LibraryApp.BLL
{
    public class ContentService
    {
        private readonly IContentRepository _contentRepository;

        public ContentService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public void AddContent(string title, string format, string location, string type, string additionalInfo)
        {
            Content content = type switch
            {
                "Book" => new Book { Title = title, Format = format, Location = location, Type = type, Author = additionalInfo },
                "Document" => new Document { Title = title, Format = format, Location = location, Type = type, Author = additionalInfo },
                "Video" => new Video { Title = title, Format = format, Location = location, Type = type, Director = additionalInfo },
                "Audio" => new Audio { Title = title, Format = format, Location = location, Type = type, Artist = additionalInfo },
                _ => throw new ArgumentException("Invalid content type")
            };
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

        public IEnumerable<Content> GetAllContent()
        {
            return _contentRepository.GetAll();
        }
    }
}
