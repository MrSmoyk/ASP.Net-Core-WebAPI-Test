using Domain.DTOs.DogDTOs;
using Domain.Entities;
using Domain.Helpers;
using Domain.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IDogService : IBaseService<Dog,DogDTO>
    {
        public Task<PagedList<DogDTO>> GetAllDogsAsync(DogParameters dogParameters);
        public Task<DogDTO> CreateDogAsync(DogCreateUpdateDTO dogCreateUpdateDTO);
        public Task<DogDTO> UpdateDogAsync(int id, DogCreateUpdateDTO dogCreateUpdateDTO);
    }
}
