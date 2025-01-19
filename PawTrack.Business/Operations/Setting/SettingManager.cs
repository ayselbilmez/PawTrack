using PawTrack.Data.Entities;
using PawTrack.Data.Repositories;
using PawTrack.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Setting
{
    public class SettingManager : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _settingRepository;

        public SettingManager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        public bool GetMaintenanceState()
        {
            var maintanenceState = _settingRepository.GetById(1).MaintanenceMode;

            return maintanenceState;
        }

        public async Task ToggleMaintanence()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintanenceMode = !setting.MaintanenceMode;

            _settingRepository.Update(setting);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Bakim durumu kaydedilemedi");
            }
        }
    }
}
