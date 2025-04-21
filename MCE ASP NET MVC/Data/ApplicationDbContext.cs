using MCE_ASP_NET_MVC.models;
using MCE_ASP_NET_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MCE_ASP_NET_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<UserFriend> user_friends { get; set; } = null!;
        public DbSet<Notification> notifications { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // user_friend table
            builder.Entity<UserFriend>().HasKey(m => new { m.userId, m.friendId });
            builder.Entity<UserFriend>().HasOne<IdentityUser>().WithMany().HasForeignKey(m => m.userId); // IdentityUser == AspNetUser table

            // notifications table
            builder.Entity<Notification>().HasKey(m => new { m.id });
            builder.Entity<Notification>().HasOne<IdentityUser>().WithMany().HasForeignKey(m => m.userId);

            base.OnModelCreating(builder);
        }
    }
}