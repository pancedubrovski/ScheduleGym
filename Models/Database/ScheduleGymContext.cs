using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ScheduleGym.Models;


namespace ScheduleGym.Models.Database
{
  
    public class ScheduleGymContext : DbContext
    {
      public ScheduleGymContext() {
        
      }
      public ScheduleGymContext(DbContextOptions<ScheduleGymContext> options)
            : base(options)
      {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
      }
      public DbSet<User> users { get; set; }
      public DbSet<Place> places { get; set; }
      public DbSet<Review> reviewes { get; set; }
      public DbSet<CustomerPreferences> customersPreferences { get; set; }
      public DbSet<Appointments> appointments { get; set; }
      public DbSet<AvalableTerms> avalableTerms { get; set; }

      public DbSet<Photo> photos { get; set; }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.Entity<Place>()
            .HasMany(p => p.appointments)
            .WithOne(a => a.Place)
            .HasForeignKey(p => p.PlaceId)
            .HasPrincipalKey(p => p.Id);
 

            modelBuilder.Entity<User>()
            
            .HasMany(p => p.owners)
            .WithOne(a => a.Owner)
            .IsRequired()
            .HasForeignKey(p => p.OwnerId)
            .HasPrincipalKey(p => p.Id);
    

            modelBuilder.Entity<Appointments>()
            .HasOne(s => s.User)
            .WithMany(u => u.appointments)
            .OnDelete(DeleteBehavior.SetNull);
      }
  
        
    }
}