using JWTAuthenticatonDemo.Application;

namespace JWTAuthenticatonDemo.API
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationLayer(builder.Configuration);
            builder.Services.AddControllers();
            //builder.Services.AddCors();
            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseCors();
            app.MapControllers();
            return app;
        }
    }
}
