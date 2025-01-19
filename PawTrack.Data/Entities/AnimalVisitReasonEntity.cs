using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Entities
{
    public class AnimalVisitReasonEntity : BaseEntity
    {
        public int AnimalId { get; set; }

        public int VisitReasonId { get; set; }

        public AnimalEntity Animal { get; set; }

        public VisitReasonEntity VisitReason { get; set; }
    }

    public class AnimalVisitReasonConfiguration : BaseConfiguration<AnimalVisitReasonEntity>
    {
        public override void Configure(EntityTypeBuilder<AnimalVisitReasonEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("AnimalId", "VisitReasonId");

            base.Configure(builder);
        }
    }
}
