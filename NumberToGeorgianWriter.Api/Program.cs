using NumberToGeorgianWriter.Api.Middlewares;
using NumberToGeorgianWriter.Core.Contracts;
using NumberToGeorgianWriter.Core.Libraries;
using NumberToGeorgianWriter.Features.ConvertNumber;

namespace NumberToGeorgianWriter.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<INumberToGeoContract, NumberToGeoLibrary>();
            builder.Services.AddScoped<INumberConverter, NumberConvertService>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.Run();
        }
    }
}
