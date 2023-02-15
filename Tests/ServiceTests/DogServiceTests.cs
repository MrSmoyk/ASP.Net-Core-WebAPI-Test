using AutoMapper;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.DTOs.DogDTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Helpers;
using Domain.Params;
using Service.AutoMapperProfiles;
using Service.Implementations;
using Service.Interfaces;
using Tests.DalTests.FakeDataBase;

namespace Tests.ServiceTests
{
    public class DogServiceTests
    {
        private MapperConfiguration mockMapper;
        private IMapper mapper;
        private ApplicationDbContext Context;
        private ISortHelper<Dog> sortHelper;
        private IDogRepository dogRepository;
        private IDogService dogService;

        public DogServiceTests()
        {
            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(DogMapperProfiles));
            });
            mapper = mockMapper.CreateMapper();

            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            sortHelper = new SortHelper<Dog>();
            dogService = new DogService(dogRepository, sortHelper, mapper);
            Context.Database.EnsureDeleted();
        }

        [Fact]
        public async void DogService_GetAll_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            dogService = new DogService(dogRepository, sortHelper, mapper);
            var testDogs = new DogDTO[]
            {
                new DogDTO { Id = 1, Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32},
                new DogDTO { Id = 2, Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14}
            };

            var testParams = new DogParameters();
            PagedList<DogDTO> result = new();
            Task.Run(async () => result = await dogService.GetAllDogsAsync(testParams)).Wait();

            result.Should().NotBeNullOrEmpty();
            result[0].Should().BeEquivalentTo(testDogs[0]);
            result[1].Should().BeEquivalentTo(testDogs[1]);
            Context.Database.EnsureDeleted();
        }


        [Fact]
        public void DogService_GetFromId_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            dogService = new DogService(dogRepository, sortHelper, mapper);
            var expected = new DogDTO { Id = 1, Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32 };


            int id = 1;

            DogDTO result = new();
            Task.Run(async () => result = await dogService.GetByIdAsync(id)).Wait();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
            Context.Database.EnsureDeleted();

        }

        [Fact]
        public void DogService_Create_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            dogService = new DogService(dogRepository, sortHelper, mapper);
            var expected = new DogDTO { Id = 3, Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32 };
            var dogToCreate = new DogCreateUpdateDTO { Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32 };


            DogDTO result = new();
            Task.Run(async () => result = await dogService.CreateDogAsync(dogToCreate)).Wait();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
            Context.Database.EnsureDeleted();
        }

        [Fact]
        public async void DogService_Delete_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            dogService = new DogService(dogRepository, sortHelper, mapper);

            int id = 1;

            await dogService.DeleteAsync(id);

            var result = dogService.GetByIdAsync(id).Exception;

            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result.InnerException.Should().BeOfType<EntityNotFoundException>();
            result.InnerException.Message.Should().BeEquivalentTo("Entity with id: 1 not found.");
            Context.Database.EnsureDeleted();

        }

        [Fact]
        public void DogService_Update_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            dogService = new DogService(dogRepository, sortHelper, mapper);

            var testId = 1;
            var testDog = new DogCreateUpdateDTO
            {
                Name = "",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };
            var expected = new DogDTO()
            {
                Id = testId,
                Name = "",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };

            DogDTO result = new();
            Task.Run(async () => result = await dogService.UpdateDogAsync(testId, testDog)).Wait();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
            Context.Database.EnsureDeleted();

        }
    }
}
