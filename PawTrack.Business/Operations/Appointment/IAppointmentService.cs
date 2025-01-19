using PawTrack.Business.Operations.Appointment.Dtos;
using PawTrack.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Appointment
{
    public interface IAppointmentService
    {
        Task<ServiceMessage> AddAppointment(AddAppointmentDto appointmentDto);

        Task<List<AppointmentVetDto>> GetAppointmentsVetById (int animalId);

        Task<List<AppointmentOwnerDto>> GetAppointmentsOwner (int animalId);

        Task<List<AppointmentVetDto>> GetAppointmentsVet(int vetId);

        Task<ServiceMessage> DateUpdate(int id,  DateTime newDate);

        Task<ServiceMessage> Delete(int id);    
    }
}
