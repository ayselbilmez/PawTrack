using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Appointment.Dtos
{
    public class AddAppointmentDto
    {
        public int AnimalId { get; set; }

        public int VetId { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string? NotesByVet { get; set; }

        public List<int> VisitReasonIds { get; set; }

    }
}
