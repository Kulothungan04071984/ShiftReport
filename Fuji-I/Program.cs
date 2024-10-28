//using Fuji_I.Data;
using Fuji_I.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        ConfigureServices(builder.Services, configuration);

        var app = builder.Build();

        Configure(app, configuration, builder.Environment);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.AddTransient<Inter_details, services_details>(); // Example service registration
       // services.AddDbContext<ApplicationDbContext>(options =>
        //options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<DataAccess>();
        


        // Configure Serilog for file logging
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration) // Read Serilog configuration from appsettings.json
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(dispose: true); // Add Serilog as the logging provider
        });

        // Set EPPlus license context
        OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Adjust based on your licensing scenario

        // Other configurations can be added here
    }

    private static void Configure(IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
    {
        if (configuration["Logging:LogLevel:Default"] == "Information")
        {
            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        //app.UseSerilogRequestLogging(); // Uncomment if you want to log HTTP requests using Serilog

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");
        });
    }
}


//using Fuji_I.Models;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Serilog;

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);
//        var configuration = builder.Configuration;

//        ConfigureServices(builder.Services, configuration);

//        var app = builder.Build();

//        Configure(app, configuration,IWebHostEnvironment,loggerFactory);

//        app.Run();
//    }

//    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddControllersWithViews();
//        services.AddTransient<Inter_details, services_details>(); // Example service registration

//        // Configure Serilog for file logging
//        //Log.Logger = new LoggerConfiguration()
//        //    .MinimumLevel.Information()
//        //    .WriteTo.Console()
//        //    .WriteTo.Debug()
//        //    .WriteTo.File(@"\\192.168.1.181\sap_xmls\myapp-.txt",
//        //        rollingInterval: RollingInterval.Day,
//        //        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
//        //    .CreateLogger();

//        //services.AddLogging(logging =>
//        //{
//        //    logging.ClearProviders();
//        //    logging.AddSerilog(); // Add Serilog as the logging provider
//        //});

//        // Set EPPlus license context
//        OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Adjust based on your licensing scenario

//        // Other configurations can be added here
//    }

//    private static void Configure(IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env, ILoggerFactory loggerFactory)
//    {
//        if (configuration["Logging:LogLevel:Default"] == "Information")
//        {
//            app.UseExceptionHandler("/Home/Error");
//            app.UseHsts();
//        }

//        app.UseHttpsRedirection();
//        app.UseStaticFiles();
//        app.UseRouting();
//        app.UseAuthorization();

//        //app.UseSerilogRequestLogging(); // This logs HTTP requests using Serilog

//        app.UseEndpoints(endpoints =>
//        {
//            endpoints.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=MainPage}/{action=shiftReport}/{id?}");
//        });
//    }
//}
