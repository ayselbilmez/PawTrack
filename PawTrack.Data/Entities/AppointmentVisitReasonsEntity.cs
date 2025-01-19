using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Entities
{
    public class AppointmentVisitReasonsEntity : BaseEntity
    {
        public int AppointmentId { get; set; }

        public int VisitReasonId { get; set; }

        public AppointmentEntity Appointment { get; set; }

        public VisitReasonEntity VisitReason { get; set; }
    }

    public class AppoinmentVisitReasonConfiguration : BaseConfiguration<AppointmentVisitReasonsEntity>
    {
        public override void Configure(EntityTypeBuilder<AppointmentVisitReasonsEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("AppointmentId", "VisitReasonId");

            base.Configure(builder);
        }
    }
}
