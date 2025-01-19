using Microsoft.EntityFrameworkCore;
using PawTrack.Business.Operations.VisitReason.Dtos;
using PawTrack.Business.Types;
using PawTrack.Data.Entities;
using PawTrack.Data.Repositories;
using PawTrack.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.VisitReason
{
    public class VisitReasonManager : IVisitReasonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<VisitReasonEntity> _repository;

        public VisitReasonManager(IRepository<VisitReasonEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceMessage> AddVisitReason(AddVisitReasonDto visitReason)
        {
            var existReason = _repository.GetAll(x => x.Reason.ToLower() == visitReason.Reason.ToLower()).Any();

            if (existReason)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Visit Reason sistemde zaten var"
                };
            }

            var visitReasonEntity = new VisitReasonEntity
            {
                Reason = visitReason.Reason,
                Description = visitReason.Description
            };

            _repository.Add(visitReasonEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kayit sirasinda hata olustu");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basarili bir sekilde kaydedilmistir"
            };
        }

        public async Task<ServiceMessage> Delete(int id)
        {
            var reason = _repository.GetById(id);

            if(reason == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Boyle bir ziyaret sebep bulunamadi"
                };
            }

            _repository.Delete(reason);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kayit sirasinda hata olustu");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basarili bir sekilde silinmistir"
            };
        }

        public async Task<List<VisitReasonsDto>> GetVisitReasons()
        {
            var reasons = await _repository.GetAll().Select(x => new VisitReasonsDto
            {
                Id = x.Id,
                Reason = x.Reason,
                Description = x.Description
            }).ToListAsync();

            return reasons;
        }
    }
}
