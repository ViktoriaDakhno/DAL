using System;
using LibraryApp.DAL;

namespace LibraryApp.BLL
{
    public static class ContentFactory
    {
        public static Content CreateContent(int type, string title, 
            string format, string location, string additionalInfo, int storageId)
        {
            switch (type)
            {
                case 1: 
                    return new Book
                    {
                        Title = title,
                        Format = format,
                        Location = location,
                        Type = "Книга",
                        Author = additionalInfo,
                        StorageId = storageId
                    };
                case 2: 
                    return new Document
                    {
                        Title = title,
                        Format = format,
                        Location = location,
                        Type = "Документ",
                        Author = additionalInfo,
                        StorageId = storageId
                    };
                case 3: 
                    return new Video
                    {
                        Title = title,
                        Format = format,
                        Location = location,
                        Type = "Відео",
                        Director = additionalInfo,
                        StorageId = storageId
                    };
                case 4: 
                    return new Audio
                    {
                        Title = title,
                        Format = format,
                        Location = location,
                        Type = "Аудіо",
                        Artist = additionalInfo,
                        StorageId = storageId
                    };
                default:
                    throw new ArgumentException("Невідомий тип контенту. " +
                        "Використовуйте: 1 - Книга, 2 - Документ, 3 - Відео, 4 - Аудіо.");
            }
        }
    }
}
