using AutoMapper;
using Domain.DTOs.DogDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
