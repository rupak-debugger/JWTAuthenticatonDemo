using JWTAuthenticatonDemo.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AppConfigurations(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerExtension();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerExtension();

}

app.UseHttpsRedirection();

app.UseCors(x => x
                .AllowAnyHeader()
                .AllowAnyMethod()
                //.AllowCredentials()
                .AllowAnyOrigin()
                );

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
