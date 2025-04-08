using Microsoft.EntityFrameworkCore;
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

        [Required]
        public string Type { get; set; }

        // Зв'язок 1:1 – контент має метадані
        public virtual Metadata Metadata { get; set; }

        // Зв’язок 1:M – кожен контент пов’язаний із сховищем
        public int StorageId { get; set; }
        public virtual Storage Storage { get; set; }

        // Зв’язок M:N – контент може мати багато тегів
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
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

    public class Metadata
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        // Зв'язок 1:1 з Content
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
    }


    // Сховище (Storage): 1 сховище – багато контенту
    public class Storage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 

        [Required]
        public string Location { get; set; } 

        public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
    }

    // Теги – для прикладу зв’язок M:N: контент може мати багато тегів, тег – багато контентів
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
    }
}