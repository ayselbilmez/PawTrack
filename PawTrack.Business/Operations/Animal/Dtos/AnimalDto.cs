using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Animal.Dtos
{
    public class AnimalDto
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string Name { get; set; }

        public string Species { get; set; }

        public string Breed { get; set; }

        public int BirthYear { get; set; }

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public List<AnimalVisitReasonDto> VisitReasons { get; set; }
    }
}
