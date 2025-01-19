using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawTrack.Business.Operations.Appointment;
using PawTrack.Business.Operations.Appointment.Dtos;
using PawTrack.WebApi.Models;

namespace PawTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("vet/{animalId}")]
        [Authorize(Roles = "Vet")]

        public async Task<IActionResult> GetForVetById(int animalId)
        {
            var appointments = await _appointmentService.GetAppointmentsVetById(animalId);

            if (appointments == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(appointments);
            }
        }

        [HttpGet("owner/{animalId}")]
        [Authorize(Roles = "Owner")]

        public async Task<IActionResult> GetForOwnerById(int animalId)
        {
            var appointments = await _appointmentService.GetAppointmentsOwner(animalId);

            if (appointments == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(appointments);
            }
        }

        [HttpGet("vet/myAppointments/{vetId}")]
        [Authorize(Roles = "Vet")]

        public async Task<IActionResult> GetMyAppointments(int vetId)
        {
            var myAppointments = await _appointmentService.GetAppointmentsVet(vetId);

            if (myAppointments == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(myAppointments);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Vet")]

        public async Task<IActionResult> AddAppointment(AddAppointmentRequest request)
        {
            var appointmentDto = new AddAppointmentDto
            {
                VetId = request.VetId,
                AnimalId = request.AnimalId,
                AppointmentTime = request.AppointmentTime,
                NotesByVet = request.NotesByVet,
                VisitReasonIds = request.VisitReasonIds
            };

            var result  = await _appointmentService.AddAppointment(appointmentDto);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPatch("{id}/date")]
        [Authorize(Roles = "Vet")]

        public async Task<IActionResult> DateUpdate(int id, DateTime newDate)
        {
            var result = await _appointmentService.DateUpdate(id, newDate);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Vet")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appointmentService.Delete(id);

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
