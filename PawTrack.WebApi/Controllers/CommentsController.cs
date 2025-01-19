using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawTrack.Business.Operations.Comment;
using PawTrack.Business.Operations.Comment.Dtos;
using PawTrack.WebApi.Filters;
using PawTrack.WebApi.Models;

namespace PawTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Vet,Owner")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{appointmentId}")]

        public async Task<IActionResult> GetComments(int appointmentId)
        {
            var comments = await _commentService.GetComments(appointmentId);

            if(comments == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(comments);
            }
        }

        [HttpPost]
        
        public async Task<IActionResult> AddComment(AddCommentRequest request)
        {
            var addCommentDto = new AddCommentDto
            {
                UserId = request.UserId,
                AppointmentId = request.AppointmentId,
                Content = request.Content
            };

            var result = await _commentService.AddComment(addCommentDto);

            if(result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete]
        [TimeControlFilter]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commentService.Delete(id);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
