using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleRestApi.Database;
using SampleRestApi.Managers;
using SampleRestApi.Middlewares;
using SampleRestApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
services.AddDbContext<DogsContext>(opts =>
{
    opts.UseSqlServer(connection);
    opts.EnableSensitiveDataLogging();
});

services.AddScoped<IDogService, DogManager>();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    app.UseHsts();

app.UseHttpsRedirection();

app.UseMiddleware<ControlRequestsMiddleware>(10);

app.UseAuthorization();

app.MapControllers();

app.Run();
