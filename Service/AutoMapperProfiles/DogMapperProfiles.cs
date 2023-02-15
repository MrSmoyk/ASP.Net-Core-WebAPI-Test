using AutoMapper;
using Domain.DTOs.DogDTOs;
using Domain.Entities;

namespace Service.AutoMapperProfiles
{
    public class DogMapperProfiles : Profile
    {
        public DogMapperProfiles()
        {
            CreateMap<Dog, DogDTO>()
                .ReverseMap();
            CreateMap<DogCreateUpdateDTO, Dog>()
                .ReverseMap();
        }
    }
}
