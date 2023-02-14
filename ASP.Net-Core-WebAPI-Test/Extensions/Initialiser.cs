using DAL.Interfaces;
using DAL.Repositories;
using Domain.Entities;
using Domain.Helpers;
using Service.Implementations;
using Service.Interfaces;

namespace ASP.Net_Core_WebAPI_Test.Extensions
{
    public static class Initialiser
    {
        public static void InitialiseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDogRepository, DogRepository>();
        }

        public static void InitialiseServices(this IServiceCollection services)
        {
            services.AddScoped<IDogService, DogService>();
        }

        public static void InitialiseHelpers(this IServiceCollection services)
        {
            services.AddScoped<ISortHelper<Dog>, SortHelper<Dog>>();
        }
    }
}
