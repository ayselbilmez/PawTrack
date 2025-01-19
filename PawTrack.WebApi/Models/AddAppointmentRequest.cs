using System.ComponentModel.DataAnnotations;

namespace PawTrack.WebApi.Models
{
    public class AddAppointmentRequest
    {
        [Required]
        public int AnimalId { get; set; }

        [Required]
        public int VetId { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        public string? NotesByVet { get; set; }

        [Required]
        public List<int> VisitReasonIds { get; set; }
    }
}
