using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Entities
{
    public class CommentEntity : BaseEntity
    {
        public int UserId { get; set; }

        public int AppointmentId { get; set; }

        public string Content { get; set; }

        //Relational Properties

        //1:N Relations

        public UserEntity Vet { get; set; }

        public AppointmentEntity Appointment { get; set; }
    }

    public class CommentConfiguration : BaseConfiguration<CommentEntity>
    {
        public override void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.HasOne(x => x.Appointment)
                .WithMany()
                .HasForeignKey(x => x.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Vet)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }   
}
