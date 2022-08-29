using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //database connection 
            optionsBuilder.UseSqlServer("server= (localdb)\\MSSQLLocalDB;database=BlogDb;integrated security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message2>()
                .HasOne(x => x.SenderUser) //ilişki içine alınacak prop
                .WithMany(y => y.WriterSender) //ilişki prop'una diğer tabloda karşılık gelen ICollection 
                .HasForeignKey(z => z.SenderId) //prop'a karşılık gelen id
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message2>()
                .HasOne(x => x.ReceiverUser) //ilişki içine alınacak prop
                .WithMany(y => y.WriterReceiver)  //ilişki prop'una diğer tabloda karşılık gelen ICollection 
                .HasForeignKey(z => z.ReceiverId) //prop'a karşılık gelen id
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<BlogRating> BlogRatings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Message2> Messages2 { get; set; }
    }
}