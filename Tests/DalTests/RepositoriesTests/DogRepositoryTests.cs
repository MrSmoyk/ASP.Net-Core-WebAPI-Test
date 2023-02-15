using DAL;
using DAL.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Tests.DalTests.FakeDataBase;

namespace Tests.DalTests.RepositoriesTests
{
    public class DogRepositoryTests
    {
        private ApplicationDbContext Context;
        private DogRepository dogRepository;

        public DogRepositoryTests()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
        }

        [Fact]
        public void DogRepository_GetByIdAsync_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            var testDog = new Dog()
            {
                Id = 1,
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };
            var id = testDog.Id;

            Dog result = new();
            Task.Run(async () => result = await dogRepository.GetByIdAsync(id)).Wait();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(testDog);
            Context.Database.EnsureDeleted();
        }

        [Fact]
        public void DogRepository_GetAll_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            var expectedDogs = new Dog[]
            {
                new Dog { Id = 1, Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32},
                new Dog { Id = 2, Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14}
            };


            var result = dogRepository.GetAll().OrderBy(x => x.Id);


            result.Should().NotBeNull();
            result.Count().Should().Be(expectedDogs.Length);

            result.First().Should().BeEquivalentTo(expectedDogs[0]);


            result.Last().Should().BeEquivalentTo(expectedDogs[1]);
            Context.Database.EnsureDeleted();
        }

        [Fact]
        public async void DogRepository_Create_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            var expected = new Dog
            {
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };

            Dog result = new();
            Task.Run(async () => result = await dogRepository.CreateAsync(expected)).Wait();

            result.Equals(expected);
            Context.Database.EnsureDeleted();
        }

        [Fact]
        public async void DogService_Delete_SucsessAsync()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            int id = 1;

            await dogRepository.DeleteAsync(id);

            var result = dogRepository.GetByIdAsync(id).Exception;

            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result.InnerException.Should().BeOfType<EntityNotFoundException>();
            result.InnerException.Message.Should().BeEquivalentTo("Entity with id: 1 not found.");
            Context.Database.EnsureDeleted();
        }


        [Fact]
        public void CourseRepository_Uppdate_Sucsess()
        {
            Context = FakeDbContext.GetDbContext();
            dogRepository = new DogRepository(Context);
            var testDog = new Dog()
            {
                Id = 1,
                Name = "Pip",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };
            var result = dogRepository.UpdateAsync(testDog).Result;

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(testDog);
            Context.Database.EnsureDeleted();
        }
    }
}
