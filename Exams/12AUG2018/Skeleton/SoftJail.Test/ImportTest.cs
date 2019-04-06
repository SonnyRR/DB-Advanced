using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SoftJail;
using SoftJail.Data;
using SoftJail.DataProcessor;

[TestFixture]
public class ImportTest
{
    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    private IServiceProvider serviceProvider;

    [SetUp]
    public void Setup()
    {
        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile(GetType("SoftJailProfile")));

        this.serviceProvider = ConfigureServices<SoftJailDbContext>("SoftJail");
    }

    [Test]
    public void ImportOfficersPrisonersZeroTest2()
    {
        var context = serviceProvider.GetService<SoftJailDbContext>();

        var inputXml = @"<Officers><Officer><Name>Riccardo Fockes</Name><Money>3623.98</Money><Position>Overseer</Position><Weapon>Pistol</Weapon><DepartmentId>3</DepartmentId><Prisoners><Prisoner id=""10"" /><Prisoner id=""16"" /><Prisoner id=""15"" /></Prisoners></Officer><Officer><Name>Arleen Zannolli</Name><Money>3539.40</Money><Position>Guard</Position><Weapon>FlashPulse</Weapon><DepartmentId>2</DepartmentId><Prisoners><Prisoner id=""2"" /></Prisoners></Officer><Officer><Name>Hailee Kennon</Name><Money>3652.49</Money><Position>Labour</Position><Weapon>Sniper</Weapon><DepartmentId>5</DepartmentId><Prisoners><Prisoner id=""3"" /><Prisoner id=""14"" /></Prisoners></Officer><Officer><Name>Lev de Chastelain</Name><Money>2442.80</Money><Position>Guard</Position><Weapon>Sniper</Weapon><DepartmentId>2</DepartmentId><Prisoners><Prisoner id=""13"" /><Prisoner id=""12"" /></Prisoners></Officer></Officers>";

        var actualOutput = Deserializer.ImportOfficersPrisoners(context, inputXml).TrimEnd();
        var expectedOutput = "Imported Riccardo Fockes (3 prisoners)\r\nImported Arleen Zannolli (1 prisoners)\r\nImported Hailee Kennon (2 prisoners)\r\nImported Lev de Chastelain (2 prisoners)";

        var assertContext = serviceProvider.GetService<SoftJailDbContext>();

        var expectedOfficersCount = 4;
        var actualOfficersCount = assertContext.Officers.Count();

        var expectedOfficersPrisonersCount = 8;
        var actualOfficersPrisonersCount = assertContext.OfficersPrisoners.Count();

        Assert.That(actualOfficersCount, Is.EqualTo(expectedOfficersCount), "Number of inserted officers is incorrect!");

        Assert.That(actualOfficersPrisonersCount, Is.EqualTo(expectedOfficersPrisonersCount), "Number of inserted officer prisoners is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip, $"{nameof(Deserializer.ImportOfficersPrisoners)} output is incorrect!");
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
            );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}