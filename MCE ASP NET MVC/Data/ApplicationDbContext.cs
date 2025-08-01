﻿using MCE_ASP_NET_MVC.models;
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
        public DbSet<ChampionshipMember> championship_members { get; set; } = null!;
        public DbSet<GrandPrixResult> grandprix_results { get; set; } = null!;
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

            // championship_members table
            builder.Entity<ChampionshipMember>().HasKey(a => new { a.ChampionshipId, a.UserId });
            builder.Entity<ChampionshipMember>().HasOne<IdentityUser>().WithMany().HasForeignKey(a => a.UserId);
            builder.Entity<ChampionshipMember>().HasOne<Championship>().WithMany().HasForeignKey(a => a.ChampionshipId);

            // grandprix_results table
            builder.Entity<GrandPrixResult>().HasKey(a => new { a.GrandPrixId, a.ChampionshipMemberId });
            builder.Entity<GrandPrixResult>().HasOne<ChampionshipMember>().WithMany().HasForeignKey(a => a.ChampionshipMemberId).HasPrincipalKey(p => p.UserId);
            builder.Entity<GrandPrixResult>().HasOne<GrandPrix>().WithMany().HasForeignKey(a => a.GrandPrixId);

            base.OnModelCreating(builder);
        }
    }
}