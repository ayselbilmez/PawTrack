using PawTrack.Business.Operations.Animal.Dtos;
using PawTrack.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Animal
{
    public interface IAnimalService
    {
        Task<ServiceMessage> AddAnimal(AddAnimalDto animal);

        Task<AnimalDto> GetAnimal(int id);

        Task<List<AnimalDto>> GetAllAnimals();

        Task<ServiceMessage> GenderUpdate(int id, string newGender);

        Task<ServiceMessage> Delete(int id);

        Task<ServiceMessage> UpdateAnimal(UpdateAnimalsDto animal);
    }
}
