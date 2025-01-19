using Microsoft.EntityFrameworkCore;
using PawTrack.Business.Operations.Appointment.Dtos;
using PawTrack.Business.Types;
using PawTrack.Data.Entities;
using PawTrack.Data.Repositories;
using PawTrack.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Appointment
{
    public class AppointmentManager : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AppointmentEntity> _appointmentRepository;
        private readonly IRepository<AppointmentVisitReasonsEntity> _appointmentVisitReasonRepository;


        public AppointmentManager(IUnitOfWork unitOfWork, IRepository<AppointmentEntity> appointmentRepository, 
                                   IRepository<AppointmentVisitReasonsEntity> appointmentVisitReasonRepository)
        {
            _unitOfWork = unitOfWork;
            _appointmentRepository = appointmentRepository;
            _appointmentVisitReasonRepository = appointmentVisitReasonRepository;
        }

        public async Task<ServiceMessage> AddAppointment(AddAppointmentDto appointmentDto)
        {
            var existAppointment = _appointmentRepository.GetAll(x => x.AppointmentTime == appointmentDto.AppointmentTime);

            if (existAppointment.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Giris yapilan tarihte bir randevu zaten var"
                };
            }

            await _unitOfWork.BeginTransactionAsync();

            var appointmentEntity = new AppointmentEntity
            {
                VetId = appointmentDto.VetId,
                AnimalId = appointmentDto.AnimalId,
                AppointmentTime = appointmentDto.AppointmentTime,
                NotesByVet = appointmentDto.NotesByVet
            };

            _appointmentRepository.Add(appointmentEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw new Exception("Kayit sirasinda bir hata ile karsilasildi");
            }

            foreach (var visitReasonId in appointmentDto.VisitReasonIds)
            {
                var appointmentVisitReason = new AppointmentVisitReasonsEntity
                {
                    AppointmentId = appointmentEntity.Id,
                    VisitReasonId = visitReasonId
                };

                _appointmentVisitReasonRepository.Add(appointmentVisitReason);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Visit Reason eklenirken sorunla karsilasildi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basariyla randevu olusturuldu"
            };
        }

        public async Task<List<AppointmentOwnerDto>> GetAppointmentsOwner(int animalId)
        {
            var appointments = await _appointmentRepository.GetAll(x => x.AnimalId == animalId)
                .Select(a => new AppointmentOwnerDto
                {
                    Id = a.Id,
                    AnimalId = a.AnimalId,
                    AppointmentTime = a.AppointmentTime,
                    IsCompleted = a.IsCompleted,
                    VisitReasons = a.AppointmentVisitReasons.Select(r => new AppointmentVisitReasonDto
                    {
                        Id = r.Id,
                        Reason = r.VisitReason.Reason
                    }).ToList()
                }).ToListAsync();

            return appointments;
        }

        public async Task<List<AppointmentVetDto>> GetAppointmentsVetById(int animalId)
        {
            var appointments = await _appointmentRepository.GetAll(x => x.AnimalId == animalId)
                .Select(a => new AppointmentVetDto
                {
                    Id = a.Id,
                    AnimalId = a.AnimalId,
                    VetId = a.VetId,
                    AppointmentTime = a.AppointmentTime,
                    IsCompleted = a.IsCompleted,
                    VisitReasons = a.AppointmentVisitReasons.Select(r => new AppointmentVisitReasonDto
                    {
                        Id = r.Id,
                        Reason = r.VisitReason.Reason
                    }).ToList()
                }).ToListAsync();

            return appointments;
        }
        
        public async Task<List<AppointmentVetDto>> GetAppointmentsVet(int vetId)
        {
            var myAppointments = await _appointmentRepository.GetAll(x => x.VetId == vetId)
                .Select(a => new AppointmentVetDto
                {
                    Id = vetId,
                    AnimalId = a.AnimalId,
                    VetId = a.VetId,
                    AppointmentTime = a.AppointmentTime,
                    IsCompleted = a.IsCompleted,
                    VisitReasons = a.AppointmentVisitReasons.Select(r => new AppointmentVisitReasonDto
                    {
                        Id = r.Id,
                        Reason = r.VisitReason.Reason
                    }).ToList()
                }).ToListAsync();

            return myAppointments;
        }

        public async Task<ServiceMessage> DateUpdate(int id, DateTime newDate)
        {
            var appointment = _appointmentRepository.GetById(id);

            if (appointment == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Boyle bir randevu yok"
                };
            }

            appointment.AppointmentTime = newDate;
            appointment.ModifiedDate = DateTime.Now;

            _appointmentRepository.Update(appointment);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kaydedilirken bir hata ile karsilasildi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basari ile randevu zamani degistirildi"
            };
        }

        public async Task<ServiceMessage> Delete(int id)
        {
            var appointment = _appointmentRepository.GetById(id);

            if (appointment == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Boyle bir randevu yok"
                };
            }

            _appointmentRepository.Delete(appointment);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kaydedilirken bir hata ile karsilasildi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Randevu basari ile silindi"
            };
        }
    }
}
