using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.DAL
{
    public class Content
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Format { get; set; }

        [Required]
        public string Location { get; set; }

        public string Type { get; set; } // Book, Document, Video, Audio
    }

    public class Book : Content
    {
        public string Author { get; set; }
    }

    public class Document : Content
    {
        public string Author { get; set; }
    }

    public class Video : Content
    {
        public string Director { get; set; }
    }

    public class Audio : Content
    {
        public string Artist { get; set; }
    }

    public class LibraryContext : DbContext
    {
        public DbSet<Content> Contents { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Audio> Audios { get; set; }
    }
}
