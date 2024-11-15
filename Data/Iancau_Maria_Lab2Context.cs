using Iancau_Maria_Lab2.Models;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Iancau_Maria_Lab2.Data
{
    public class Iancau_Maria_Lab2Context :  IdentityDbContext<IdentityUser>
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the Book <-> Borrowing relationship
            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Book) // Borrowing has one Book
                .WithOne(bk => bk.Borrowing) // Book has one Borrowing
                .HasForeignKey<Borrowing>(b => b.BookID) // Specify the FK on Borrowing
                .OnDelete(DeleteBehavior.SetNull); // Specify delete behavior, SetNull is a safe choice

            // You might also need to configure other relationships like Borrowing <-> Member if necessary.
        }
        public DbSet<Iancau_Maria_Lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<Iancau_Maria_Lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }

}
