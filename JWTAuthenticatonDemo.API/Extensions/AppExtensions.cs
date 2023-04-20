namespace JWTAuthenticatonDemo.API.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTAuthenticationDemo");
                c.RoutePrefix = "swagger";
            });
        }
    }
}
