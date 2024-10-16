using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Iancau_Maria_Lab2.Models;

namespace Iancau_Maria_Lab2.Data
{
    public class Iancau_Maria_Lab2Context : DbContext
    {
        public Iancau_Maria_Lab2Context(DbContextOptions<Iancau_Maria_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = default;
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; } = default;
    }

}
