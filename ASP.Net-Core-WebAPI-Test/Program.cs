using ASP.Net_Core_WebAPI_Test.Extensions;
using Service.AutoMapperProfiles;
using DAL;
using System.Runtime.CompilerServices;
using ASP.Net_Core_WebAPI_Test.Middleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.InitialiseRepositories();
        builder.Services.InitialiseServices();
        builder.Services.InitialiseHelpers();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(DogMapperProfiles));
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

        app.Run();
    }
}