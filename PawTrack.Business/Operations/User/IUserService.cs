using PawTrack.Business.Operations.User.Dtos;
using PawTrack.Business.Types;
using PawTrack.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);
    
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);

        Task<ServiceMessage> Delete(int id);

        Task<ServiceMessage> UpdateRole(int id, UserType UserType);
    }
}
