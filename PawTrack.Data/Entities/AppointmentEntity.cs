using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Entities
{
    public class AppointmentEntity : BaseEntity
    {
        public int AnimalId { get; set; }

        public int VetId { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string? NotesByVet { get; set; }

        public bool IsCompleted { get; set; } = false;

        //Relational Properties

        // 1:1 Relations

        public AnimalEntity Animal { get; set; }
        public UserEntity Vet { get; set; }

        //1:N Relations

        public ICollection<CommentEntity> Comments { get; set; }

        // Many-to-many Relation
        public ICollection<AppointmentVisitReasonsEntity> AppointmentVisitReasons { get; set; }

    }

    public class AppointmentConfiguration : BaseConfiguration<AppointmentEntity>
    {
        public override void Configure(EntityTypeBuilder<AppointmentEntity> builder)
        {
            builder.Property(x => x.NotesByVet)
                .IsRequired(false);

            builder.HasOne(x => x.Animal)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Vet)
                .WithMany(a => a.Appointments)
                .HasForeignKey(s => s.VetId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
