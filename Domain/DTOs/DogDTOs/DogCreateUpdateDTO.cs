using System.ComponentModel.DataAnnotations;

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
