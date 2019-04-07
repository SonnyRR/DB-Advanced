//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Cinema;
using Cinema.Data;
using Cinema.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[TestFixture]
public class Import_000_003
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
    public void ImportProjectionsZeroTest()
    {
        var context = this.serviceProvider.GetService<CinemaContext>();

        SeedDatabase(context);

        var inputXml =
            "<?xml version=\'1.0\' encoding=\'UTF-8\'?><Projections><Projection><MovieId>38</MovieId><HallId>4</HallId><DateTime>2019-04-27 13:33:20</DateTime></Projection><Projection><MovieId>6</MovieId><HallId>4</HallId><DateTime>2019-05-12 05:51:29</DateTime></Projection><Projection><MovieId>21</MovieId><HallId>5</HallId><DateTime>2019-05-03 16:56:12</DateTime></Projection><Projection><MovieId>10</MovieId><HallId>5</HallId><DateTime>2019-05-01 00:11:21</DateTime></Projection><Projection><MovieId>40</MovieId><HallId>4</HallId><DateTime>2019-04-26 08:56:57</DateTime></Projection><Projection><MovieId>18</MovieId><HallId>3</HallId><DateTime>2019-05-16 16:23:07</DateTime></Projection><Projection><MovieId>34</MovieId><HallId>7</HallId><DateTime>2019-04-23 09:17:46</DateTime></Projection><Projection><MovieId>15</MovieId><HallId>8</HallId><DateTime>2019-04-21 13:12:29</DateTime></Projection><Projection><MovieId>6</MovieId><HallId>7</HallId><DateTime>2019-05-01 19:43:03</DateTime></Projection><Projection><MovieId>7</MovieId><HallId>1</HallId><DateTime>2019-04-23 12:04:05</DateTime></Projection><Projection><MovieId>17</MovieId><HallId>3</HallId><DateTime>2019-04-29 08:09:47</DateTime></Projection><Projection><MovieId>18</MovieId><HallId>1</HallId><DateTime>2019-04-29 22:49:17</DateTime></Projection><Projection><MovieId>17</MovieId><HallId>10</HallId><DateTime>2019-04-21 05:43:19</DateTime></Projection><Projection><MovieId>29</MovieId><HallId>10</HallId><DateTime>2019-04-22 08:41:07</DateTime></Projection><Projection><MovieId>37</MovieId><HallId>5</HallId><DateTime>2019-05-05 02:12:49</DateTime></Projection><Projection><MovieId>11</MovieId><HallId>2</HallId><DateTime>2019-05-09 06:08:57</DateTime></Projection><Projection><MovieId>36</MovieId><HallId>10</HallId><DateTime>2019-05-05 22:49:39</DateTime></Projection><Projection><MovieId>28</MovieId><HallId>5</HallId><DateTime>2019-04-15 04:55:01</DateTime></Projection><Projection><MovieId>28</MovieId><HallId>5</HallId><DateTime>2019-05-15 20:33:47</DateTime></Projection><Projection><MovieId>6</MovieId><HallId>5</HallId><DateTime>2019-04-25 02:56:36</DateTime></Projection><Projection><MovieId>11</MovieId><HallId>6</HallId><DateTime>2019-05-19 08:00:32</DateTime></Projection><Projection><MovieId>8</MovieId><HallId>3</HallId><DateTime>2019-05-23 20:22:41</DateTime></Projection><Projection><MovieId>9</MovieId><HallId>2</HallId><DateTime>2019-05-01 13:12:56</DateTime></Projection><Projection><MovieId>6</MovieId><HallId>6</HallId><DateTime>2019-05-14 02:11:11</DateTime></Projection><Projection><MovieId>12</MovieId><HallId>6</HallId><DateTime>2019-04-24 05:08:44</DateTime></Projection><Projection><MovieId>9</MovieId><HallId>5</HallId><DateTime>2019-05-10 11:08:44</DateTime></Projection><Projection><MovieId>4</MovieId><HallId>1</HallId><DateTime>2019-05-10 15:36:16</DateTime></Projection><Projection><MovieId>29</MovieId><HallId>8</HallId><DateTime>2019-05-22 13:00:40</DateTime></Projection><Projection><MovieId>1</MovieId><HallId>1</HallId><DateTime>2019-04-19 18:54:51</DateTime></Projection><Projection><MovieId>24</MovieId><HallId>1</HallId><DateTime>2019-04-23 03:43:49</DateTime></Projection><Projection><MovieId>31</MovieId><HallId>6</HallId><DateTime>2019-05-17 14:59:40</DateTime></Projection><Projection><MovieId>28</MovieId><HallId>4</HallId><DateTime>2019-05-02 14:21:07</DateTime></Projection><Projection><MovieId>31</MovieId><HallId>1</HallId><DateTime>2019-05-08 02:34:10</DateTime></Projection><Projection><MovieId>40</MovieId><HallId>10</HallId><DateTime>2019-05-10 23:10:32</DateTime></Projection><Projection><MovieId>17</MovieId><HallId>4</HallId><DateTime>2019-04-20 03:48:11</DateTime></Projection><Projection><MovieId>3</MovieId><HallId>6</HallId><DateTime>2019-04-30 19:12:17</DateTime></Projection><Projection><MovieId>22</MovieId><HallId>10</HallId><DateTime>2019-04-17 02:58:38</DateTime></Projection><Projection><MovieId>27</MovieId><HallId>1</HallId><DateTime>2019-04-25 06:59:11</DateTime></Projection><Projection><MovieId>5</MovieId><HallId>3</HallId><DateTime>2019-05-14 00:04:12</DateTime></Projection><Projection><MovieId>29</MovieId><HallId>4</HallId><DateTime>2019-05-08 23:22:41</DateTime></Projection><Projection><MovieId>8</MovieId><HallId>8</HallId><DateTime>2019-05-18 22:58:37</DateTime></Projection><Projection><MovieId>36</MovieId><HallId>3</HallId><DateTime>2019-05-15 20:07:25</DateTime></Projection><Projection><MovieId>30</MovieId><HallId>10</HallId><DateTime>2019-05-08 04:54:33</DateTime></Projection><Projection><MovieId>2</MovieId><HallId>10</HallId><DateTime>2019-05-11 10:10:01</DateTime></Projection><Projection><MovieId>1</MovieId><HallId>7</HallId><DateTime>2019-05-03 21:27:41</DateTime></Projection><Projection><MovieId>28</MovieId><HallId>7</HallId><DateTime>2019-05-17 06:28:22</DateTime></Projection><Projection><MovieId>32</MovieId><HallId>7</HallId><DateTime>2019-05-12 06:31:14</DateTime></Projection><Projection><MovieId>25</MovieId><HallId>8</HallId><DateTime>2019-04-19 04:21:24</DateTime></Projection><Projection><MovieId>1</MovieId><HallId>2</HallId><DateTime>2019-05-03 11:50:44</DateTime></Projection><Projection><MovieId>34</MovieId><HallId>6</HallId><DateTime>2019-05-03 01:36:58</DateTime></Projection><Projection><MovieId>19</MovieId><HallId>8</HallId><DateTime>2019-04-20 02:16:22</DateTime></Projection><Projection><MovieId>36</MovieId><HallId>10</HallId><DateTime>2019-04-15 14:54:56</DateTime></Projection><Projection><MovieId>3</MovieId><HallId>7</HallId><DateTime>2019-05-08 18:21:11</DateTime></Projection><Projection><MovieId>22</MovieId><HallId>1</HallId><DateTime>2019-05-13 00:38:18</DateTime></Projection><Projection><MovieId>11</MovieId><HallId>10</HallId><DateTime>2019-05-20 14:29:19</DateTime></Projection><Projection><MovieId>23</MovieId><HallId>9</HallId><DateTime>2019-05-11 21:26:14</DateTime></Projection><Projection><MovieId>11</MovieId><HallId>1</HallId><DateTime>2019-05-22 14:31:29</DateTime></Projection><Projection><MovieId>30</MovieId><HallId>7</HallId><DateTime>2019-05-17 12:40:07</DateTime></Projection><Projection><MovieId>14</MovieId><HallId>3</HallId><DateTime>2019-04-28 19:53:01</DateTime></Projection><Projection><MovieId>35</MovieId><HallId>1</HallId><DateTime>2019-04-22 03:19:34</DateTime></Projection></Projections>";

        var actualOutput =
            Cinema.DataProcessor.Deserializer.ImportProjections(context, inputXml).TrimEnd();

        ;
        var expectedOutput =
            "Invalid data!\r\nSuccessfully imported projection T.N.T. on 05/12/2019!\r\nSuccessfully imported projection Gloriously Wasted on 05/03/2019!\r\nSuccessfully imported projection Best Worst Movie on 05/01/2019!\r\nInvalid data!\r\nSuccessfully imported projection Cranford on 05/16/2019!\r\nInvalid data!\r\nSuccessfully imported projection Trojan Eddie on 04/21/2019!\r\nSuccessfully imported projection T.N.T. on 05/01/2019!\r\nSuccessfully imported projection Host, The (Gwoemul) on 04/23/2019!\r\nSuccessfully imported projection Living \'til the End on 04/29/2019!\r\nSuccessfully imported projection Cranford on 04/29/2019!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported projection Sorcerer on 05/09/2019!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported projection T.N.T. on 04/25/2019!\r\nSuccessfully imported projection Sorcerer on 05/19/2019!\r\nSuccessfully imported projection Shaggy D.A., The on 05/23/2019!\r\nSuccessfully imported projection Silent Partner, The on 05/01/2019!\r\nSuccessfully imported projection T.N.T. on 05/14/2019!\r\nSuccessfully imported projection White Man\'s Burden on 04/24/2019!\r\nSuccessfully imported projection Silent Partner, The on 05/10/2019!\r\nSuccessfully imported projection Moog on 05/10/2019!\r\nInvalid data!\r\nSuccessfully imported projection Gui Si (Silk) on 04/19/2019!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported projection Living \'til the End on 04/20/2019!\r\nSuccessfully imported projection SIS on 04/30/2019!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported projection Free Willy on 05/14/2019!\r\nInvalid data!\r\nSuccessfully imported projection Shaggy D.A., The on 05/18/2019!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported projection Gui Si (Silk) on 05/03/2019!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported projection Gui Si (Silk) on 05/03/2019!\r\nInvalid data!\r\nSuccessfully imported projection Absurdistan on 04/20/2019!\r\nInvalid data!\r\nSuccessfully imported projection SIS on 05/08/2019!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported projection Sorcerer on 05/22/2019!\r\nInvalid data!\r\nSuccessfully imported projection One Day on 04/28/2019!\r\nInvalid data!";

        var assertContext = this.serviceProvider.GetService<CinemaContext>();

        const int expectedProjectionCount = 29;
        var actualProjectionCount = assertContext.Projections.Count();

        Assert.That(actualProjectionCount, Is.EqualTo(expectedProjectionCount),
            $"Inserted {nameof(context.Projections)} count is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(Cinema.DataProcessor.Deserializer.ImportProjections)} output is incorrect!");


    }

    private static void SeedDatabase(CinemaContext context)
    {
        var datasetsJson =
            "{\"Movie\":[{\"Id\":1,\"Title\":\"Gui Si (Silk)\",\"Genre\":1,\"Duration\":\"02:21:00\",\"Rating\":9.0,\"Director\":\"Perl Swyne\",\"Projections\":[]},{\"Id\":2,\"Title\":\"Prey, The (La proie)\",\"Genre\":0,\"Duration\":\"02:08:00\",\"Rating\":5.0,\"Director\":\"Israel Sircomb\",\"Projections\":[]},{\"Id\":3,\"Title\":\"SIS\",\"Genre\":0,\"Duration\":\"02:14:00\",\"Rating\":10.0,\"Director\":\"Tuesday Scothern\",\"Projections\":[]},{\"Id\":4,\"Title\":\"Moog\",\"Genre\":9,\"Duration\":\"01:06:00\",\"Rating\":4.0,\"Director\":\"Wash Couth\",\"Projections\":[]},{\"Id\":5,\"Title\":\"Free Willy\",\"Genre\":1,\"Duration\":\"02:51:00\",\"Rating\":1.0,\"Director\":\"Sheree Lindenman\",\"Projections\":[]},{\"Id\":6,\"Title\":\"T.N.T.\",\"Genre\":0,\"Duration\":\"02:14:00\",\"Rating\":8.0,\"Director\":\"Inesita MacGlory\",\"Projections\":[]},{\"Id\":7,\"Title\":\"Host, The (Gwoemul)\",\"Genre\":2,\"Duration\":\"01:00:00\",\"Rating\":9.0,\"Director\":\"Harmonia Gannon\",\"Projections\":[]},{\"Id\":8,\"Title\":\"Shaggy D.A., The\",\"Genre\":7,\"Duration\":\"01:25:00\",\"Rating\":5.0,\"Director\":\"Tallia Siveyer\",\"Projections\":[]},{\"Id\":9,\"Title\":\"Silent Partner, The\",\"Genre\":3,\"Duration\":\"02:20:00\",\"Rating\":7.0,\"Director\":\"Cally Beals\",\"Projections\":[]},{\"Id\":10,\"Title\":\"Best Worst Movie\",\"Genre\":6,\"Duration\":\"02:59:00\",\"Rating\":3.0,\"Director\":\"Hamel Della Scala\",\"Projections\":[]},{\"Id\":11,\"Title\":\"Sorcerer\",\"Genre\":0,\"Duration\":\"02:42:00\",\"Rating\":6.0,\"Director\":\"Clifford Ramelet\",\"Projections\":[]},{\"Id\":12,\"Title\":\"White Man\'s Burden\",\"Genre\":1,\"Duration\":\"02:02:00\",\"Rating\":7.0,\"Director\":\"Joannes Alekseev\",\"Projections\":[]},{\"Id\":13,\"Title\":\"Stroker Ace\",\"Genre\":0,\"Duration\":\"01:55:00\",\"Rating\":3.0,\"Director\":\"Inessa Mertsching\",\"Projections\":[]},{\"Id\":14,\"Title\":\"One Day\",\"Genre\":1,\"Duration\":\"01:02:00\",\"Rating\":3.0,\"Director\":\"Marcelle Huggett\",\"Projections\":[]},{\"Id\":15,\"Title\":\"Trojan Eddie\",\"Genre\":3,\"Duration\":\"02:57:00\",\"Rating\":5.0,\"Director\":\"Mark Frany\",\"Projections\":[]},{\"Id\":16,\"Title\":\"Creator\",\"Genre\":2,\"Duration\":\"01:05:00\",\"Rating\":6.0,\"Director\":\"Konstantine Kierans\",\"Projections\":[]},{\"Id\":17,\"Title\":\"Living \'til the End\",\"Genre\":1,\"Duration\":\"02:55:00\",\"Rating\":5.0,\"Director\":\"Doralin Pray\",\"Projections\":[]},{\"Id\":18,\"Title\":\"Cranford\",\"Genre\":1,\"Duration\":\"02:24:00\",\"Rating\":2.0,\"Director\":\"Avivah Westcot\",\"Projections\":[]},{\"Id\":19,\"Title\":\"Absurdistan\",\"Genre\":2,\"Duration\":\"02:34:00\",\"Rating\":9.0,\"Director\":\"Emelia Weagener\",\"Projections\":[]},{\"Id\":20,\"Title\":\"Fahrenhype 9/11\",\"Genre\":6,\"Duration\":\"02:36:00\",\"Rating\":8.0,\"Director\":\"Rayna Forsyth\",\"Projections\":[]},{\"Id\":21,\"Title\":\"Gloriously Wasted\",\"Genre\":2,\"Duration\":\"01:16:00\",\"Rating\":5.0,\"Director\":\"Shaughn Sattin\",\"Projections\":[]}], \"Hall\":[{\"Id\":1,\"Name\":\"Methocarbamol\",\"Is4Dx\":false,\"Is3D\":true},{\"Id\":2,\"Name\":\"Glycopyrrolate\",\"Is4Dx\":true,\"Is3D\":false},{\"Id\":3,\"Name\":\"Corn Grass\",\"Is4Dx\":true,\"Is3D\":true},{\"Id\":4,\"Name\":\"Aminophylline\",\"Is4Dx\":false,\"Is3D\":false},{\"Id\":5,\"Name\":\"Aspergillus flavus\",\"Is4Dx\":false,\"Is3D\":false},{\"Id\":6,\"Name\":\"Pain Relief Plus\",\"Is4Dx\":true,\"Is3D\":false},{\"Id\":7,\"Name\":\"Vitalizer\",\"Is4Dx\":false,\"Is3D\":false},{\"Id\":8,\"Name\":\"CVS SPF 8\",\"Is4Dx\":true,\"Is3D\":true},{\"Id\":9,\"Name\":\"CYZONE\",\"Is4Dx\":false,\"Is3D\":false}]}";

        var datasets = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<JObject>>>(datasetsJson);


        foreach (var dataset in datasets)
        {
            var entityType = GetType(dataset.Key);
            var entities = dataset.Value
                .Select(j => j.ToObject(entityType))
                .ToArray();

            context.AddRange(entities);
        }

        context.SaveChanges();
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