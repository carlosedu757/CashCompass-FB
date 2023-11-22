using RestAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using RestAPI.Repositories.Interfaces;
using RestAPI.Repositories;
using AutoMapper;
using RestAPI.Models.DTOs.Mappings;

namespace RestAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options =>
                                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
                                    options.UseMySql(mySqlConnection,
                                    ServerVersion.AutoDetect(mySqlConnection)));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://127.0.0.1:5500")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials());
            });

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);

        var app = builder.Build();

        //builder.Services.

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.UseCors("AllowOrigin");

        app.MapControllers();

        app.Run();
    }
}