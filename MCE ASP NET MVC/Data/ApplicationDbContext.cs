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
        public DbSet<Championship> championships { get; set; } = null!;
        public DbSet<GrandPrix> grandprixes { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // user_friends table
            builder.Entity<UserFriend>().HasKey(a => new { a.userId, a.friendId });
            builder.Entity<UserFriend>().HasOne<IdentityUser>().WithMany().HasForeignKey(a => a.userId); // IdentityUser == AspNetUser table

            // notifications table
            builder.Entity<Notification>().HasKey(a => new { a.id });
            builder.Entity<Notification>().HasOne<IdentityUser>().WithMany().HasForeignKey(a => a.userId);

            // championships table
            builder.Entity<Championship>().HasKey(a => new { a.Id });
            builder.Entity<Championship>().HasOne<IdentityUser>().WithMany().HasForeignKey(a => a.OwnerId);

            // grandprixes table
            builder.Entity<GrandPrix>().HasKey(a => new { a.Id });
            builder.Entity<GrandPrix>().HasOne<Championship>().WithMany().HasForeignKey(a => a.ChampionshipId);

            base.OnModelCreating(builder);
        }
    }
}