using PawTrack.Business.Operations.Comment.Dtos;
using PawTrack.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Comment
{
    public interface ICommentService
    {
        Task<ServiceMessage> AddComment(AddCommentDto comment);

        Task<List<CommentDto>> GetComments(int appointmentId);

        Task<ServiceMessage> Delete(int id);

    }
}
