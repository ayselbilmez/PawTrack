using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawTrack.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public UserType UserType { get; set; }

        //Relational Properties

        //1:N Relations

        public ICollection<CommentEntity> Comments { get; set; }
        public ICollection<AnimalEntity> Animals { get; set; }
        public ICollection<AppointmentEntity> Appointments { get; set; }
    }

    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.PhoneNumber)
                .IsRequired();
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(x => x.Email)
                .IsUnique();

            base.Configure(builder);
        }
    }
}
