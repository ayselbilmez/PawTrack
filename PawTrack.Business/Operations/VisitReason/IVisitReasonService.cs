using PawTrack.Business.Operations.Animal.Dtos;
using PawTrack.Business.Operations.VisitReason.Dtos;
using PawTrack.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.VisitReason
{
    public interface IVisitReasonService
    {
        public Task<ServiceMessage> AddVisitReason(AddVisitReasonDto visitReason);

        public Task<List<VisitReasonsDto>> GetVisitReasons();

        public Task<ServiceMessage> Delete(int id); 
    }
}
