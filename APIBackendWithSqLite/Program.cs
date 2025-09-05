using APIBackend.Services.Interfaces;
using APIBackend.Services;
using APIBackend.Data;
using Microsoft.EntityFrameworkCore;
using APIBackend.Utilities.Reports;

namespace APIBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var builder = WebApplication.CreateBuilder(args);
            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

            // Add services to the container.

            builder.Services.AddControllers();

            // Configure SQLite
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Automatically register all IReportGenerator implementations
            builder.Services.Scan(scan => scan
                .FromAssemblyOf<IReportGenerator>()       // scans current assembly
                .AddClasses(classes => classes.AssignableTo<IReportGenerator>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());


            builder.Services.AddScoped<ILoggingService, LoggingService>();
            builder.Services.AddTransient<ReportFactory>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins(allowedOrigins!)
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseCors("AllowSpecificOrigins");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
