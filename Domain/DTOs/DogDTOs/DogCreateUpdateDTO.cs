using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DogDTOs
{
    public class DogCreateUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Tail Length must be greater than zero")]
        public int TailLength { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Weight must be greater than zero")]
        public int Weight { get; set; }
    }
}
