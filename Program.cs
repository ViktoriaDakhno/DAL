using LibraryApp.BLL;
using LibraryApp.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Metrics;
using System.Linq;

namespace LibraryApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LibraryDb;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True")

                .Options;

            var context = new LibraryContext(options);
            var repository = new ContentRepository(context);
            var contentService = new ContentService(repository);

            if (!context.Storages.Any())
            {
                context.Storages.Add(new Storage { Name = "Сховище 1", Location = "Локація 1" });
                context.Storages.Add(new Storage { Name = "Сховище 2", Location = "Локація 2" });
                context.SaveChanges();
            }

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
                        AddContent(contentService, context);
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

        static void AddContent(ContentService contentService, LibraryContext context)
        {
            Console.WriteLine("Виберіть тип контенту:");
            Console.WriteLine("1. Книга");
            Console.WriteLine("2. Документ");
            Console.WriteLine("3. Відео");
            Console.WriteLine("4. Аудіо");
            Console.Write("Введіть номер типу контенту: ");
            if (!int.TryParse(Console.ReadLine(), out int type) || type < 1 || type > 4)
            {
                Console.WriteLine("Невірно введено тип контенту. Спробуйте ще раз.");
                return;
            }

            Console.Write("Введіть назву: ");
            var title = Console.ReadLine();

            Console.Write("Введіть формат (наприклад, PDF, MP4, MP3): ");
            var format = Console.ReadLine();

            Console.Write("Введіть локацію контенту: ");
            var location = Console.ReadLine();

            Console.Write("Введіть додаткову інформацію (Автор, Режисер, Артист): ");
            var additionalInfo = Console.ReadLine();

            // Вибір сховища – демонстраційно вибираємо перший запис
            var storage = context.Storages.FirstOrDefault();
            if (storage == null)
            {
                Console.WriteLine("Сховище не знайдено!");
                return;
            }

            contentService.AddContent(type, title, format, location, additionalInfo, storage.Id);
            Console.WriteLine($"{(type == 1 ? "Книга" : type == 2 ? "Документ" : type == 3 ? "Відео" : "Аудіо")} успішно додано!");
        }

        static void RemoveContent(ContentService contentService)
        {
            Console.Write("Введіть ID контенту для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int contentId))
            {
                contentService.RemoveContent(contentId);
                Console.WriteLine("Контент успішно видалено!");
            }
            else
            {
                Console.WriteLine("Невірно введено ID.");
            }
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

        static void ViewAllContent(ContentService service)
        {
            var contents = service.GetAllContent();
            Console.WriteLine("\nID\tНазва\t\tТип\t\tФормат\t\tЛокація");
            foreach (var content in contents)
            {
                Console.WriteLine($"{content.Id}\t{content.Title,-15}\t{content.Type,-10}\t{content.Format,-10}\t{content.Location}");
            }
        }
    }
}
