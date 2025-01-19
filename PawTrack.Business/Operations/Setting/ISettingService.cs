using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Setting
{
    public interface ISettingService
    {
        Task ToggleMaintanence();

        bool GetMaintenanceState();
    }
}
