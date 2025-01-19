using Microsoft.EntityFrameworkCore;
using PawTrack.Business.Operations.Animal.Dtos;
using PawTrack.Business.Types;
using PawTrack.Data.Entities;
using PawTrack.Data.Repositories;
using PawTrack.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Animal
{
    public class AnimalManager : IAnimalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AnimalEntity> _animalRepository;
        private readonly IRepository<AnimalVisitReasonEntity> _animalVisitReasonRepository;

        public AnimalManager(IUnitOfWork unitOfWork, IRepository<AnimalEntity> repository, 
            IRepository<AnimalVisitReasonEntity> animalVisitReasonRepository)
        {
            _animalRepository = repository;
            _unitOfWork = unitOfWork;
            _animalVisitReasonRepository = animalVisitReasonRepository;
        }

        public async Task<ServiceMessage> AddAnimal(AddAnimalDto animal)
        {
            var existAnimal = _animalRepository.GetAll(x => x.OwnerId == animal.OwnerId && 
                                                            x.Name.ToLower() == animal.Name.ToLower()).Any();

            if (existAnimal)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu sahibe ait olan evcil hayvan zaten sistemde var"
                };
            }

            await _unitOfWork.BeginTransactionAsync();

            var animalEntity = new AnimalEntity
            {
                OwnerId = animal.OwnerId,
                Name = animal.Name,
                BirthYear = animal.BirthYear,
                Breed = animal.Breed,
                Species = animal.Species,
                Age = animal.Age,
                Gender = animal.Gender
            };

            _animalRepository.Add(animalEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kayit sirasinda hata olustu");
            }

            foreach (var visitReasonId in animal.VisitReasonIds)
            {
                var animalVisitReason = new AnimalVisitReasonEntity
                {
                    AnimalId = animalEntity.Id,
                    VisitReasonId = visitReasonId
                };

                _animalVisitReasonRepository.Add(animalVisitReason);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Visit Reason eklenirken sorunla karsilasildi.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Kayit basariyla olusturuldu"
            };
        }

        public async Task<ServiceMessage> Delete(int id)
        {
            var animal = _animalRepository.GetById(id);

            if (animal == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Boyle bir hayvan bulunamadi"
                };
            }

            _animalRepository.Delete(animal);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kaydedilirken bir hata meydana geldi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basariyla silindi"
            };
        }

        public async Task<ServiceMessage> GenderUpdate(int id, string newGender)
        {
            var animal = _animalRepository.GetById(id);

            if(animal == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Boyle bir hayvan bulunamadi"
                };
            }

            animal.Gender = newGender;

            _animalRepository.Update(animal);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kaydedilirken bir hata meydana geldi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basariyla kaydedildi"
            };
        }

        public async Task<List<AnimalDto>> GetAllAnimals()
        {
            var animals = await _animalRepository.GetAll().Select(x => new AnimalDto
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                OwnerId = x.OwnerId,
                Species = x.Species,
                Breed = x.Breed,
                BirthYear = x.BirthYear,
                Gender = x.Gender,
                VisitReasons = x.AnimalVisitReasons.Select(r => new AnimalVisitReasonDto
                {
                    Id = r.Id,
                    VisitReason = r.VisitReason.Reason
                }).ToList()
            }).ToListAsync();

            return animals;
        }

        public async Task<AnimalDto> GetAnimal(int id)
        {
            var animal = await _animalRepository.GetAll(x => x.Id == id)
                .Select(x => new AnimalDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Gender = x.Gender,
                    OwnerId = x.OwnerId,
                    Species = x.Species,
                    Breed = x.Breed,
                    BirthYear = x.BirthYear,
                    Age = x.Age,
                    VisitReasons = x.AnimalVisitReasons.Select(v => new AnimalVisitReasonDto
                    {
                        Id = v.Id,
                        VisitReason = v.VisitReason.Reason
                    }).ToList()
                }).FirstOrDefaultAsync();

            return animal;
        }

        public async Task<ServiceMessage> UpdateAnimal(UpdateAnimalsDto animal)
        {
            var animalEntity = _animalRepository.GetById(animal.Id);

            if (animalEntity == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Evcil hayvan bulunamadi"
                };
            }

            await _unitOfWork.BeginTransactionAsync();

            animalEntity.Name = animal.Name;
            animalEntity.OwnerId = animal.OwnerId;
            animalEntity.Gender = animal.Gender;
            animalEntity.BirthYear = animal.BirthYear;
            animalEntity.Age = animal.Age;
            animalEntity.Breed = animal.Breed;
            animalEntity.Species = animal.Species;
            animalEntity.ModifiedDate = DateTime.Now;

            _animalRepository.Update(animalEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Kaydedilirken bir hata meydana geldi");
            }

            var animalVisitReasons = _animalVisitReasonRepository.GetAll(x => x.AnimalId == x.AnimalId).ToList();

            foreach (var visitReason in animalVisitReasons)
            {
                _animalVisitReasonRepository.Delete(visitReason, false);
            }

            foreach(var reasonId in animal.VisitReasonIds)
            {
                var animalVisitReason = new AnimalVisitReasonEntity
                {
                    AnimalId = animalEntity.Id,
                    VisitReasonId = reasonId
                };

                _animalVisitReasonRepository.Add(animalVisitReason);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Guncellenirken bir hata meydana geldi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basari ile guncellendi"
            };
        }
    }
}
