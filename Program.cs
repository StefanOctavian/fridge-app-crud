using Crud.Services.Implementations;
using Crud.Middlewares;
using Crud.Extensions;

namespace Crud;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddApi();
        builder.AddDatabase();
        builder.AddServices();

        builder.AddCorsConfiguration();
        builder.AddSwaggerAuthorization("Fridge App CRUD API");
        builder.ConfigureAuthentication();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            SeederService.Seed(app.Services);
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
