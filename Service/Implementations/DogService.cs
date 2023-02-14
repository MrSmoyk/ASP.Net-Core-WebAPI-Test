using AutoMapper;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.DTOs.DogDTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Helpers;
using Domain.Params;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class DogService : BaseService<Dog, DogDTO>, IDogService
    {
        private IDogRepository DogRepository;
        public DogService(IDogRepository dogRepository, ISortHelper<Dog> sortHelper, IMapper mapper) : base(dogRepository, sortHelper, mapper)
        {
            DogRepository = dogRepository;
        }

        public async Task<PagedList<DogDTO>> GetAllDogsAsync(DogParameters dogParameters)
        {
            var dogs = await GetAllEntitysAsync();
            var sortedDogs = sortHelper.ApplySort(dogs, dogParameters.OrderBy);
            return mapper.Map<PagedList<DogDTO>>(await PagedList<Dog>.ToPagedList(sortedDogs, dogParameters.PageNumber, dogParameters.PageSize));
        }

        public async Task<DogDTO> CreateDogAsync(DogCreateUpdateDTO dogCreateUpdateDTO)
        {
            if (dogCreateUpdateDTO is null)
                throw new SourceEntityNullException("Entity to set wasn't given.");

            if (dogCreateUpdateDTO.Name is null)
                throw new SourceEntityNullException("Entity to set wasn't given.");

            var dogEntity = mapper.Map<Dog>(dogCreateUpdateDTO);
            var createdDogEntity = await DogRepository.CreateAsync(dogEntity);
            return mapper.Map<DogDTO>(createdDogEntity);
        }

        public async Task<DogDTO> UpdateDogAsync(int id, DogCreateUpdateDTO dogCreateUpdateDTO)
        {
            if (!await DogRepository.ExistsAsync(id))
                throw new UnknownDogException($"Dog type with id {id} doesn't exsist");

            var dogEntity = mapper.Map<Dog>(dogCreateUpdateDTO);
            dogEntity.Id = id;

            var updatedDogEntity = await DogRepository.UpdateAsync(dogEntity);
            return mapper.Map<DogDTO>(updatedDogEntity);
        }
    }
}
