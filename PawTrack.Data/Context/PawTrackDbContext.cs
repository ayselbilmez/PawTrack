using Microsoft.EntityFrameworkCore;
using PawTrack.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Context
{
    public class PawTrackDbContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<AnimalEntity> Animals => Set<AnimalEntity>();
        public DbSet<AppointmentEntity> Appointments => Set<AppointmentEntity>();
        public DbSet<CommentEntity> Comments => Set<CommentEntity>();
        public DbSet<VisitReasonEntity> VisitReasons => Set<VisitReasonEntity>();
        public DbSet<AnimalVisitReasonEntity> AnimalVisitReasons => Set<AnimalVisitReasonEntity>();
        public DbSet<AppointmentVisitReasonsEntity> AppointmentVisitReasons => Set<AppointmentVisitReasonsEntity>();
        public DbSet<SettingEntity> Settings => Set<SettingEntity>();

        public PawTrackDbContext(DbContextOptions<PawTrackDbContext> options) : base(options) 
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnimalConfiguration());
            modelBuilder.ApplyConfiguration(new AnimalVisitReasonConfiguration());
            modelBuilder.ApplyConfiguration(new AppoinmentVisitReasonConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new VisitReasonConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity()
                {
                    Id = 1,
                    MaintanenceMode = false
                }
            );



            base.OnModelCreating(modelBuilder);
        }
    }
}
