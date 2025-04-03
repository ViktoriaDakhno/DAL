using System;
using LibraryApp.BLL;
using LibraryApp.DAL;
using System.Linq;

namespace LibraryApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new LibraryContext();
            var contentRepository = new ContentRepository(context);
            var contentService = new ContentService(contentRepository);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Управління контентом бібліотеки");
                Console.WriteLine("1. Додати новий контент");
                Console.WriteLine("2. Видалити контент");
                Console.WriteLine("3. Пошук контенту");
                Console.WriteLine("4. Переглянути весь контент");
                Console.WriteLine("5. Вихід");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContent(contentService);
                        break;
                    case "2":
                        RemoveContent(contentService);
                        break;
                    case "3":
                        SearchContent(contentService);
                        break;
                    case "4":
                        ViewAllContent(contentService);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
            }
        }

        static void AddContent(ContentService contentService)
        {
            Console.Write("Введіть тип контенту (Книга, Документ, Відео, Аудіо): ");
            var type = Console.ReadLine();

            Console.Write("Введіть назву: ");
            var title = Console.ReadLine();

            Console.Write("Введіть формат (наприклад, PDF, MP4, MP3): ");
            var format = Console.ReadLine();

            Console.Write("Введіть локацію: ");
            var location = Console.ReadLine();

            Console.Write("Введіть додаткову інформацію (наприклад, Автор, Режисер, Артист): ");
            var additionalInfo = Console.ReadLine();

            contentService.AddContent(title, format, location, type, additionalInfo);
            Console.WriteLine($"{type} успішно додано!");
        }

        static void RemoveContent(ContentService contentService)
        {
            Console.Write("Введіть ID контенту для видалення: ");
            var contentId = int.Parse(Console.ReadLine());

            contentService.RemoveContent(contentId);
            Console.WriteLine("Контент успішно видалено!");
        }

        static void SearchContent(ContentService contentService)
        {
            Console.Write("Введіть пошуковий запит (назва або тип): ");
            var query = Console.ReadLine();

            var contents = contentService.SearchContent(query);

            Console.WriteLine("Результати пошуку:");
            foreach (var content in contents)
            {
                Console.WriteLine($"ID: {content.Id}, Назва: {content.Title}, Тип: {content.Type}, Формат: {content.Format}, Локація: {content.Location}");
            }
        }

        static void ViewAllContent(ContentService contentService)
        {
            var contents = contentService.GetAllContent();

            Console.WriteLine("Весь контент:");
            foreach (var content in contents)
            {
                Console.WriteLine($"ID: {content.Id}, Назва: {content.Title}, Тип: {content.Type}, Формат: {content.Format}, Локація: {content.Location}");
            }
        }
    }
}
