using PawTrack.Business.DataProtection;
using PawTrack.Business.Operations.User.Dtos;
using PawTrack.Business.Types;
using PawTrack.Data.Entities;
using PawTrack.Data.Enums;
using PawTrack.Data.Repositories;
using PawTrack.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _dataProtector;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _dataProtector = protector;
        }

        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var existUser = _userRepository.GetAll(x => x.Email == user.Email);

            if (existUser.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu email uzerine zaten bir hesap var."
                };
            }

            var userEntity = new UserEntity()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = _dataProtector.Encrypt(user.Password),
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = UserType.Owner
            };

            _userRepository.Add(userEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kullanici kaydi sirasinda bir hata olustu");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }

        public async Task<ServiceMessage> Delete(int id)
        {
            var user = _userRepository.GetById(id);

            if(user == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Boyle bir kullanici bulunamadi"
                };
            }

            _userRepository.Delete(user);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kullanici silinmesi sirasinda bir hata olustu");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basari ile silindi"
            };

        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _userRepository.Get(x => x.Email == user.Email);
            
            if(userEntity == null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Email veya parola hatali"
                };
            }

            var unprotectedPassword = _dataProtector.Decrypt(userEntity.Password);

            if (unprotectedPassword == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Email=userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        PhoneNumber = userEntity.PhoneNumber,
                        UserType = userEntity.UserType
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Email / Parola hatali"
                };
            }
        }

        public async Task<ServiceMessage> UpdateRole(int id, UserType newUserType)
        {
            var user = _userRepository.GetById(id);

            if(user == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Istenilen kullanici bulunamadi"
                };
            }

            user.UserType = newUserType;
            user.ModifiedDate = DateTime.Now;

             _userRepository.Update(user);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Rol degisimi kaydedilemedi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basariyla guncellendi"
            };
        }
    }
}
