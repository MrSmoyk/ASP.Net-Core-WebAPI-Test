using DAL;

namespace ASP.Net_Core_WebAPI_Test.Extensions
{
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder UseItToSeedSqlite(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {

            }

            return app;
        }
    }
}
