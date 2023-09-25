using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VisitorManagement.Application.MappingProfile;
using VisitorManagement.Application.Services;
using VisitorManagement.Infrastructure.Data;
using VisitorManagement.Infrastructure.Repositories;

namespace VisitorManagement.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("vmscorspolicy", p =>
                {
                    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            }
            );

            // Add services to the container.
            ConfigurationManager configuration = builder.Configuration;
            builder.Services.AddDbContext<VisitorManagementApplicationContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("VisitorManagementApplicationContext"));
            });

            // Add services to the container.
            builder.Services.AddScoped<IHostVisitorRequestService, HostVisitorRequestService>();
            builder.Services.AddScoped<IHostVisitorRequestRepository, HostVisitorRequestRepository>();

            builder.Services.AddScoped<IAdminApprovalStatusService, AdminApprovalStatusService>();
            builder.Services.AddScoped<IAdminApprovalStatusRepository, AdminApprovalStatusRepository>();

            builder.Services.AddScoped<DataSeeder>();

            builder.Services.AddAutoMapper(typeof(VisitorManagementAutoMapperProfile).Assembly);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseCors("vmscorspolicy");

            using (var scope = app.Services.CreateScope())
            {
                var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                dataSeeder.SeedAdminApprovalStatusesAsync().Wait();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}