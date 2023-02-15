using ASP.Net_Core_WebAPI_Test.Extensions;
using ASP.Net_Core_WebAPI_Test.Middleware;
using DAL;
using Microsoft.EntityFrameworkCore;
using Service.AutoMapperProfiles;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite("Data Source=DogHouse.db"));
        builder.Services.InitialiseRepositories();
        builder.Services.InitialiseServices();
        builder.Services.InitialiseHelpers();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(DogMapperProfiles));
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseItToSeedSqlite();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseMiddleware<GlobalExceptionHandler>();
        app.UseMiddleware<RateLimiting>();

        app.Run();
    }
}