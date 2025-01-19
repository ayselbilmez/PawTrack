namespace PawTrack.WebApi.Models
{
    public class AddCommentRequest
    {
        public int UserId { get; set; }

        public int AppointmentId { get; set; }

        public string Content { get; set; }
    }
}
