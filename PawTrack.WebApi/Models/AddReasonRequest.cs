using System.ComponentModel.DataAnnotations;

namespace PawTrack.WebApi.Models
{
    public class AddReasonRequest
    {
        [Required]
        public string Reason { get; set; }

        public string? Description { get; set; }
    }
}
