using Domain.DTOs.DogDTOs;
using Domain.Params;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;

namespace ASP.Net_Core_WebAPI_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : Controller
    {
        private readonly IDogService dogService;

        public DogsController(IDogService dogService)
        {
            this.dogService = dogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDogs([FromQuery] DogParameters ownerParameters)
        {
            var dogs = await dogService.GetAllDogsAsync(ownerParameters);

            var metadata = new
            {
                dogs.TotalCount,
                dogs.PageSize,
                dogs.CurrentPage,
                dogs.TotalPages,
                dogs.HasNext,
                dogs.HasPrevious
            };

            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(dogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DogDTO>> GetByIdAsync(int id)
        {
            var dogFromId = await dogService.GetByIdAsync(id);
            return Ok(dogFromId);
        }

        [HttpPost]
        public async Task<ActionResult<DogDTO>> CreateAsync([FromBody] DogCreateUpdateDTO newDog)
        {

                var createdDog = await dogService.CreateDogAsync(newDog);

            return Created(nameof(GetAllDogs), createdDog);
        }
    }
}
