using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using PawTrack.Business.Operations.Animal;
using PawTrack.Business.Operations.Animal.Dtos;
using PawTrack.WebApi.Models;

namespace PawTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Owner,Vet")]
        public async Task<IActionResult> GetAnimalById(int id)
        {
            var animal = await _animalService.GetAnimal(id);

            if (animal == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(animal);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Vet")]
        public async Task<IActionResult> GetAllAnimals()
        {
            var animals = await _animalService.GetAllAnimals();

            return Ok(animals);
        }

        [HttpPost]
        [Authorize(Roles = "Vet")]
        public async Task<IActionResult> AddAnimal(AddAnimalRequest request)
        {
            var addAnimalDto = new AddAnimalDto
            {
                Name = request.Name,
                OwnerId = request.OwnerId,
                Breed = request.Breed,
                Species = request.Species,
                BirthYear = request.BirthYear,
                Age = request.Age,
                Gender = request.Gender,
                VisitReasonIds = request.VisitReasonIds
            };

            var result = await _animalService.AddAnimal(addAnimalDto);

            if (result.IsSucceed)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPatch("{id}/gender")]

        public async Task<IActionResult> GenderUpdate(int id, string newGender)
        {
            var result = await _animalService.GenderUpdate(id, newGender);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Vet")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _animalService.Delete(id);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAnimal(int id, UpdateAnimalRequest request)
        {
            var updateAnimalDto = new UpdateAnimalsDto
            {
                Id = id,
                Gender = request.Gender,
                Name = request.Name,
                Age = request.Age,
                BirthYear = request.BirthYear,
                OwnerId = request.OwnerId,
                Breed = request.Breed,
                Species = request.Species,
                VisitReasonIds = request.VisitReasonIds
            };

            var result = await _animalService.UpdateAnimal(updateAnimalDto);

            if (result.IsSucceed)
            {
                return await GetAnimalById(id);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
    }
}
