using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawTrack.Business.Operations.VisitReason;
using PawTrack.Business.Operations.VisitReason.Dtos;
using PawTrack.Data.UnitOfWork;
using PawTrack.WebApi.Models;

namespace PawTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Vet")]
    [ApiController]
    public class VisitReasonsController : ControllerBase
    {
        private readonly IVisitReasonService _visitReasonService;

        public VisitReasonsController(IVisitReasonService visitReasonService)
        {
            _visitReasonService = visitReasonService;
        }

        [HttpGet]

        public async Task<IActionResult> GetReasons()
        {
            var reasons = await _visitReasonService.GetVisitReasons();
            
            if(reasons == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(reasons);
            }
        }

        [HttpPost]

        public async Task<IActionResult> AddVisitReason(AddReasonRequest request)
        {
            var addReasonDto = new AddVisitReasonDto
            {
                Reason = request.Reason,
                Description = request.Description
            };

            var result = await _visitReasonService.AddVisitReason(addReasonDto);

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

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _visitReasonService.Delete(id);

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
