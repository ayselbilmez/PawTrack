using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Comment.Dtos
{
    public class AddCommentDto
    {
        public int UserId { get; set; }

        public int AppointmentId { get; set; }

        public string Content { get; set; }
    }
}
