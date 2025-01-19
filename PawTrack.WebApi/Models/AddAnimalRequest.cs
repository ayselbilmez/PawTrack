using System.ComponentModel.DataAnnotations;

namespace PawTrack.WebApi.Models
{
    public class AddAnimalRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public List<int> VisitReasonIds { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public int BirthYear { get; set; }

        public int? Age { get; set; }

        public string? Gender { get; set; }
    }
}
