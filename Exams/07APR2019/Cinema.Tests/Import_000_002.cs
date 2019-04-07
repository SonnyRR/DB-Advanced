//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Cinema;
using Cinema.Data;
using Newtonsoft.Json;

[TestFixture]
public class Import_000_002
{
    private IServiceProvider serviceProvider;

    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [SetUp]
    public void Setup()
    {
        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile(GetType("CinemaProfile")));

        this.serviceProvider = ConfigureServices<CinemaContext>("Cinema");
    }

    [Test]
    public void ImportHallSeatsZeroTest()
    {
        var context = this.serviceProvider.GetService<CinemaContext>();

        var inputJson =
            "[{\"Name\":\"Methocarbamol\",\"Is4Dx\":false,\"Is3D\":true,\"Seats\":52},{\"Name\":\"Glycopyrrolate\",\"Is4Dx\":true,\"Is3D\":false,\"Seats\":36},{\"Name\":\"Corn Grass\",\"Is4Dx\":true,\"Is3D\":true,\"Seats\":40},{\"Name\":\"Aminophylline\",\"Is4Dx\":false,\"Is3D\":false,\"Seats\":31},{\"Name\":\"Aspergillus flavus\",\"Is4Dx\":false,\"Is3D\":false,\"Seats\":34},{\"Name\":\"Pain Relief Plus\",\"Is4Dx\":true,\"Is3D\":false,\"Seats\":24},{\"Name\":\"Vanilla Cupcake Antibacterial Hand Sanitizer\",\"Is4Dx\":false,\"Is3D\":true,\"Seats\":35},{\"Name\":\"Vitalizer\",\"Is4Dx\":false,\"Is3D\":false,\"Seats\":48},{\"Name\":\"Stroped\",\"Is4Dx\":false,\"Is3D\":false,\"Seats\":-1},{\"Name\":\"CVS SPF 8\",\"Is4Dx\":true,\"Is3D\":true,\"Seats\":22},{\"Name\":\"CYZONE\",\"Is4Dx\":false,\"Is3D\":false,\"Seats\":22},{\"Name\":\"C\",\"Is4Dx\":false,\"Is3D\":false,\"Seats\":22}]";

        var actualOutput =
            Cinema.DataProcessor.Deserializer.ImportHallSeats(context, inputJson).TrimEnd();

        var expectedOutput =
            "Successfully imported Methocarbamol(3D) with 52 seats!\r\nSuccessfully imported Glycopyrrolate(4Dx) with 36 seats!\r\nSuccessfully imported Corn Grass(4Dx/3D) with 40 seats!\r\nSuccessfully imported Aminophylline(Normal) with 31 seats!\r\nSuccessfully imported Aspergillus flavus(Normal) with 34 seats!\r\nSuccessfully imported Pain Relief Plus(4Dx) with 24 seats!\r\nInvalid data!\r\nSuccessfully imported Vitalizer(Normal) with 48 seats!\r\nInvalid data!\r\nSuccessfully imported CVS SPF 8(4Dx/3D) with 22 seats!\r\nSuccessfully imported CYZONE(Normal) with 22 seats!\r\nInvalid data!";

        var assertContext = this.serviceProvider.GetService<CinemaContext>();

        const int expectedHallCount = 9;
        var actualHallCount = assertContext.Halls.Count();

        const int expectedSeatCount = 309;
        var actualSeatCount = assertContext.Seats.Count();

        Assert.That(actualHallCount, Is.EqualTo(expectedHallCount),
            $"Inserted {nameof(context.Halls)} count is incorrect!");

        Assert.That(actualSeatCount, Is.EqualTo(expectedSeatCount),
            $"Inserted {nameof(context.Seats)} count is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(Cinema.DataProcessor.Deserializer.ImportHallSeats)} output is incorrect!");




    }

    private static Type GetType(string modelName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == modelName);

        Assert.IsNotNull(modelType, $"{modelName} model not found!");

        return modelType;
    }

    private static IServiceProvider ConfigureServices<TContext>(string databaseName)
        where TContext : DbContext
    {
        var services = ConfigureDbContext<TContext>(databaseName);

        var context = services.GetService<TContext>();

        try
        {
            context.Model.GetEntityTypes();
        }
        catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
        {
            services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
        }

        return services;
    }

    private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
        where TContext : DbContext
    {
        var services = new ServiceCollection();

        services
            .AddDbContext<TContext>(
                options => options
                    .UseInMemoryDatabase(databaseName)
                    .UseLazyLoadingProxies(useLazyLoading)
            );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}