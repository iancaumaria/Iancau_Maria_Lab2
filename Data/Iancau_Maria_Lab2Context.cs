using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Iancau_Maria_Lab2.Models;
using System.Collections;
using System.Configuration;

namespace Iancau_Maria_Lab2.Data
{
    public class Iancau_Maria_Lab2Context : DbContext
    {
        public Iancau_Maria_Lab2Context(DbContextOptions<Iancau_Maria_Lab2Context> options)
            : base(options)
        {
        }
      

        public DbSet<Book> Book { get; set; } = default;
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Author> Author { get; set; } = default;
        
        public DbSet<Iancau_Maria_Lab2.Models.BookCategory> BookCategory { get; set; } = default!;
        
       
        public DbSet<Category> Category { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
    }

}
