using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.VisitReason.Dtos
{
    public class AddVisitReasonDto
    {
        public string Reason { get; set; }

        public string? Description { get; set; }
    }
}
