using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Entities
{
    public class VisitReasonEntity : BaseEntity
    {
        public string Reason { get; set; }

        public string? Description { get; set; }

        //Relational Properties
        //Many-to-Many Relation

        public ICollection<AnimalVisitReasonEntity> AnimalVisitReasons { get; set; }
        public ICollection<AppointmentVisitReasonsEntity> AppointmentVisitReasons { get; set; }
    }

    public class VisitReasonConfiguration : BaseConfiguration<VisitReasonEntity>
    {
        public override void Configure(EntityTypeBuilder<VisitReasonEntity> builder)
        {
            builder.Property(x => x.Description)
                .IsRequired(false);

            base.Configure(builder);
        }
    }
}
