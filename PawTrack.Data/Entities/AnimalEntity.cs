using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Entities
{ 
    public class AnimalEntity : BaseEntity
    {
        public string Name { get; set; }

        public int OwnerId { get; set; }

        public string Species { get; set; }

        public string Breed { get; set; }

        public int BirthYear { get; set; }

        public int? Age { get; set; }

        public string? Gender { get; set; }

        // Relational properties
        // 1:1 relation
       
        public UserEntity Owner { get; set; }

        // 1:N Relations

        public ICollection<AppointmentEntity> Appointments { get; set; }

        // Many-to-Many Relation

        public ICollection<AnimalVisitReasonEntity> AnimalVisitReasons { get; set; }

    }

    public class AnimalConfiguration : BaseConfiguration<AnimalEntity>
    {
        public override void Configure(EntityTypeBuilder<AnimalEntity> builder)
        {
            builder.Property(a => a.Age)
                .IsRequired(false);
            builder.Property(a => a.Gender)
                .IsRequired(false);

            base.Configure(builder);
        }
    }
}
