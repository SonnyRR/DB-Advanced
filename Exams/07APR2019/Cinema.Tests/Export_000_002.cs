﻿//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Cinema;
using Cinema.Data;
using Cinema.DataProcessor;

[TestFixture]
public class Export_000_002
{
    private IServiceProvider serviceProvider;
    private static Assembly CurrentAssembly;

    [SetUp]
    public void Setup()
    {
        CurrentAssembly = typeof(StartUp).Assembly;

        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile(GetType("CinemaProfile")));

        this.serviceProvider = ConfigureServices<CinemaContext>("Cinema");
    }

    [Test]
    public void ExportTopCustomersZeroTest()
    {
        var context = serviceProvider.GetService<CinemaContext>();

        SeedDatabase(context);

        var actualXml = Serializer.ExportTopCustomers(context, 16);
        var actualOutput = XDocument.Parse(actualXml);

        var expectedOutputValue = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Customers><Customer FirstName=\"Duff\" LastName=\"Honig\"><SpentMoney>61.50</SpentMoney><SpentTime>13:06:00</SpentTime></Customer><Customer FirstName=\"Henrie\" LastName=\"Jurges\"><SpentMoney>53.50</SpentMoney><SpentTime>14:09:00</SpentTime></Customer><Customer FirstName=\"Jerrie\" LastName=\"O\'Carroll\"><SpentMoney>44.00</SpentMoney><SpentTime>09:24:00</SpentTime></Customer><Customer FirstName=\"Garry\" LastName=\"Blackeby\"><SpentMoney>42.50</SpentMoney><SpentTime>10:48:00</SpentTime></Customer><Customer FirstName=\"Bondy\" LastName=\"Linsay\"><SpentMoney>40.00</SpentMoney><SpentTime>08:32:00</SpentTime></Customer><Customer FirstName=\"Mariejeanne\" LastName=\"Varden\"><SpentMoney>27.50</SpentMoney><SpentTime>05:12:00</SpentTime></Customer><Customer FirstName=\"Allayne\" LastName=\"Yankishin\"><SpentMoney>27.50</SpentMoney><SpentTime>03:18:00</SpentTime></Customer><Customer FirstName=\"Corrinne\" LastName=\"Von Oertzen\"><SpentMoney>27.50</SpentMoney><SpentTime>02:32:00</SpentTime></Customer><Customer FirstName=\"Halette\" LastName=\"Lubeck\"><SpentMoney>27.50</SpentMoney><SpentTime>04:48:00</SpentTime></Customer><Customer FirstName=\"Jillian\" LastName=\"Galvin\"><SpentMoney>27.50</SpentMoney><SpentTime>08:51:00</SpentTime></Customer></Customers>";

        var expectedOutput = XDocument.Parse(expectedOutputValue);
        var expectedOutputXml = expectedOutput.ToString(SaveOptions.DisableFormatting);
        var actualOutputXml = actualOutput.ToString(SaveOptions.DisableFormatting);

        Assert.That(actualOutputXml, Is.EqualTo(expectedOutputXml).NoClip,
            $"{nameof(Serializer.ExportTopMovies)} output is incorrect!");
    }

    private static void SeedDatabase(CinemaContext context)
    {
        var datasetsJson =
            "{\"Customer\":[{\"Id\":1,\"FirstName\":\"Duff\",\"LastName\":\"Honig\",\"Age\":89,\"Balance\":82.17},{\"Id\":2,\"FirstName\":\"Tibold\",\"LastName\":\"Briztman\",\"Age\":61,\"Balance\":125.66},{\"Id\":3,\"FirstName\":\"Belinda\",\"LastName\":\"Fraanchyonok\",\"Age\":41,\"Balance\":55.69},{\"Id\":4,\"FirstName\":\"Chanda\",\"LastName\":\"Fryer\",\"Age\":60,\"Balance\":133.24},{\"Id\":5,\"FirstName\":\"Rhoda\",\"LastName\":\"Haysom\",\"Age\":40,\"Balance\":296.44},{\"Id\":6,\"FirstName\":\"Taryn\",\"LastName\":\"Ripper\",\"Age\":96,\"Balance\":102.51},{\"Id\":7,\"FirstName\":\"Kimberlee\",\"LastName\":\"Johnston\",\"Age\":13,\"Balance\":211.86},{\"Id\":8,\"FirstName\":\"Jermayne\",\"LastName\":\"Philimore\",\"Age\":89,\"Balance\":356.45},{\"Id\":9,\"FirstName\":\"Genia\",\"LastName\":\"Elsdon\",\"Age\":50,\"Balance\":224.29},{\"Id\":10,\"FirstName\":\"Caralie\",\"LastName\":\"Longthorn\",\"Age\":36,\"Balance\":169.18},{\"Id\":11,\"FirstName\":\"Garry\",\"LastName\":\"Blackeby\",\"Age\":43,\"Balance\":123.82},{\"Id\":12,\"FirstName\":\"Tallulah\",\"LastName\":\"McVeighty\",\"Age\":99,\"Balance\":285.68},{\"Id\":13,\"FirstName\":\"Evangelia\",\"LastName\":\"Faustian\",\"Age\":105,\"Balance\":56.9},{\"Id\":14,\"FirstName\":\"Henrie\",\"LastName\":\"Jurges\",\"Age\":109,\"Balance\":170.21},{\"Id\":15,\"FirstName\":\"Artemus\",\"LastName\":\"Legate\",\"Age\":101,\"Balance\":335.71},{\"Id\":16,\"FirstName\":\"Mariejeanne\",\"LastName\":\"Varden\",\"Age\":26,\"Balance\":229.88},{\"Id\":17,\"FirstName\":\"Cornall\",\"LastName\":\"Winborn\",\"Age\":90,\"Balance\":246.08},{\"Id\":18,\"FirstName\":\"Franky\",\"LastName\":\"Condit\",\"Age\":68,\"Balance\":127.23},{\"Id\":19,\"FirstName\":\"Alex\",\"LastName\":\"Petriello\",\"Age\":99,\"Balance\":354.28},{\"Id\":20,\"FirstName\":\"Berry\",\"LastName\":\"Simononsky\",\"Age\":21,\"Balance\":283.28},{\"Id\":21,\"FirstName\":\"Jena\",\"LastName\":\"Baudinelli\",\"Age\":40,\"Balance\":106.57},{\"Id\":22,\"FirstName\":\"Denny\",\"LastName\":\"Plaice\",\"Age\":16,\"Balance\":180.73},{\"Id\":23,\"FirstName\":\"Davita\",\"LastName\":\"Lister\",\"Age\":44,\"Balance\":279.76},{\"Id\":24,\"FirstName\":\"Doe\",\"LastName\":\"Westbrook\",\"Age\":110,\"Balance\":366.35},{\"Id\":25,\"FirstName\":\"Donica\",\"LastName\":\"Fossey\",\"Age\":27,\"Balance\":230.6},{\"Id\":26,\"FirstName\":\"Sargent\",\"LastName\":\"Sandbrook\",\"Age\":81,\"Balance\":299.63},{\"Id\":27,\"FirstName\":\"Marty\",\"LastName\":\"Arson\",\"Age\":36,\"Balance\":94.16},{\"Id\":28,\"FirstName\":\"Jerrome\",\"LastName\":\"Pucknell\",\"Age\":61,\"Balance\":346.53},{\"Id\":29,\"FirstName\":\"Marje\",\"LastName\":\"Boutflour\",\"Age\":77,\"Balance\":371.46},{\"Id\":30,\"FirstName\":\"Queenie\",\"LastName\":\"Schroeder\",\"Age\":43,\"Balance\":102.12},{\"Id\":31,\"FirstName\":\"Faustina\",\"LastName\":\"Gully\",\"Age\":16,\"Balance\":154.0},{\"Id\":32,\"FirstName\":\"Arluene\",\"LastName\":\"Farman\",\"Age\":44,\"Balance\":158.33},{\"Id\":33,\"FirstName\":\"Yvor\",\"LastName\":\"Achrameev\",\"Age\":35,\"Balance\":315.01},{\"Id\":34,\"FirstName\":\"Mackenzie\",\"LastName\":\"Saddleton\",\"Age\":55,\"Balance\":209.98},{\"Id\":35,\"FirstName\":\"Chuck\",\"LastName\":\"Udall\",\"Age\":87,\"Balance\":94.09},{\"Id\":36,\"FirstName\":\"Sabina\",\"LastName\":\"Kirvell\",\"Age\":110,\"Balance\":138.18},{\"Id\":37,\"FirstName\":\"Walt\",\"LastName\":\"Prendeville\",\"Age\":84,\"Balance\":245.27},{\"Id\":38,\"FirstName\":\"Ofilia\",\"LastName\":\"Binford\",\"Age\":105,\"Balance\":347.48},{\"Id\":39,\"FirstName\":\"Ray\",\"LastName\":\"MacDearmid\",\"Age\":31,\"Balance\":77.69},{\"Id\":40,\"FirstName\":\"Glenna\",\"LastName\":\"Gibbs\",\"Age\":71,\"Balance\":113.31},{\"Id\":41,\"FirstName\":\"Grace\",\"LastName\":\"Skerritt\",\"Age\":87,\"Balance\":216.74},{\"Id\":42,\"FirstName\":\"Bessie\",\"LastName\":\"Frail\",\"Age\":35,\"Balance\":240.11},{\"Id\":43,\"FirstName\":\"Liane\",\"LastName\":\"Caherny\",\"Age\":79,\"Balance\":59.87},{\"Id\":44,\"FirstName\":\"Base\",\"LastName\":\"Feander\",\"Age\":73,\"Balance\":253.94},{\"Id\":45,\"FirstName\":\"Gretna\",\"LastName\":\"Bellham\",\"Age\":42,\"Balance\":299.37},{\"Id\":46,\"FirstName\":\"Marlyn\",\"LastName\":\"Westcar\",\"Age\":20,\"Balance\":94.04},{\"Id\":47,\"FirstName\":\"Allayne\",\"LastName\":\"Yankishin\",\"Age\":108,\"Balance\":141.41},{\"Id\":48,\"FirstName\":\"Caritta\",\"LastName\":\"MacNeilley\",\"Age\":88,\"Balance\":312.56},{\"Id\":49,\"FirstName\":\"Obed\",\"LastName\":\"Rennles\",\"Age\":97,\"Balance\":173.47},{\"Id\":50,\"FirstName\":\"Janet\",\"LastName\":\"Matchett\",\"Age\":93,\"Balance\":245.51},{\"Id\":51,\"FirstName\":\"Karyn\",\"LastName\":\"Sunshine\",\"Age\":108,\"Balance\":130.45},{\"Id\":52,\"FirstName\":\"Merle\",\"LastName\":\"Pinnell\",\"Age\":55,\"Balance\":133.05},{\"Id\":53,\"FirstName\":\"Tilda\",\"LastName\":\"Laugherane\",\"Age\":56,\"Balance\":310.86},{\"Id\":54,\"FirstName\":\"Garreth\",\"LastName\":\"Leys\",\"Age\":26,\"Balance\":221.91},{\"Id\":55,\"FirstName\":\"Trudi\",\"LastName\":\"Whitlock\",\"Age\":55,\"Balance\":358.47},{\"Id\":56,\"FirstName\":\"Lloyd\",\"LastName\":\"Guerrieri\",\"Age\":52,\"Balance\":243.23},{\"Id\":57,\"FirstName\":\"Boris\",\"LastName\":\"Bennike\",\"Age\":82,\"Balance\":208.54},{\"Id\":58,\"FirstName\":\"Bondy\",\"LastName\":\"Linsay\",\"Age\":42,\"Balance\":320.43},{\"Id\":59,\"FirstName\":\"Jerrie\",\"LastName\":\"O\'Carroll\",\"Age\":44,\"Balance\":306.9},{\"Id\":60,\"FirstName\":\"Hyatt\",\"LastName\":\"Lorking\",\"Age\":25,\"Balance\":373.22},{\"Id\":61,\"FirstName\":\"Timoteo\",\"LastName\":\"Achromov\",\"Age\":17,\"Balance\":319.59},{\"Id\":62,\"FirstName\":\"Corrinne\",\"LastName\":\"Von Oertzen\",\"Age\":16,\"Balance\":138.94},{\"Id\":63,\"FirstName\":\"Mercedes\",\"LastName\":\"Gertray\",\"Age\":24,\"Balance\":179.98},{\"Id\":64,\"FirstName\":\"Dee dee\",\"LastName\":\"Likly\",\"Age\":45,\"Balance\":241.85},{\"Id\":65,\"FirstName\":\"Celestine\",\"LastName\":\"Skells\",\"Age\":109,\"Balance\":143.33},{\"Id\":66,\"FirstName\":\"Eula\",\"LastName\":\"Bettlestone\",\"Age\":69,\"Balance\":166.53},{\"Id\":67,\"FirstName\":\"Josselyn\",\"LastName\":\"Licciardi\",\"Age\":35,\"Balance\":325.46},{\"Id\":68,\"FirstName\":\"Blinnie\",\"LastName\":\"Kundert\",\"Age\":21,\"Balance\":218.98},{\"Id\":69,\"FirstName\":\"Marcellus\",\"LastName\":\"Stove\",\"Age\":68,\"Balance\":132.79},{\"Id\":70,\"FirstName\":\"Zack\",\"LastName\":\"Sellers\",\"Age\":74,\"Balance\":125.27},{\"Id\":71,\"FirstName\":\"Elmo\",\"LastName\":\"Mehew\",\"Age\":14,\"Balance\":50.55},{\"Id\":72,\"FirstName\":\"Marjy\",\"LastName\":\"Starbeck\",\"Age\":15,\"Balance\":264.43},{\"Id\":73,\"FirstName\":\"Halette\",\"LastName\":\"Lubeck\",\"Age\":71,\"Balance\":237.85},{\"Id\":74,\"FirstName\":\"Whit\",\"LastName\":\"Bidwell\",\"Age\":72,\"Balance\":353.43},{\"Id\":75,\"FirstName\":\"Jillian\",\"LastName\":\"Galvin\",\"Age\":92,\"Balance\":260.05},{\"Id\":76,\"FirstName\":\"Kalle\",\"LastName\":\"Beneze\",\"Age\":49,\"Balance\":69.36},{\"Id\":77,\"FirstName\":\"Sheela\",\"LastName\":\"Ewenson\",\"Age\":50,\"Balance\":299.43},{\"Id\":78,\"FirstName\":\"Etty\",\"LastName\":\"Stockow\",\"Age\":33,\"Balance\":372.02},{\"Id\":79,\"FirstName\":\"Rorie\",\"LastName\":\"La Torre\",\"Age\":103,\"Balance\":286.6},{\"Id\":80,\"FirstName\":\"Bruno\",\"LastName\":\"Doone\",\"Age\":104,\"Balance\":287.56},{\"Id\":81,\"FirstName\":\"Millisent\",\"LastName\":\"Hoyt\",\"Age\":24,\"Balance\":258.96}],\"Hall\":[{\"Id\":1,\"Name\":\"Methocarbamol\",\"Is4Dx\":false,\"Is3D\":true},{\"Id\":2,\"Name\":\"Glycopyrrolate\",\"Is4Dx\":true,\"Is3D\":false},{\"Id\":3,\"Name\":\"Corn Grass\",\"Is4Dx\":true,\"Is3D\":true},{\"Id\":4,\"Name\":\"Aminophylline\",\"Is4Dx\":false,\"Is3D\":false},{\"Id\":5,\"Name\":\"Aspergillus flavus\",\"Is4Dx\":false,\"Is3D\":false},{\"Id\":6,\"Name\":\"Pain Relief Plus\",\"Is4Dx\":true,\"Is3D\":false},{\"Id\":7,\"Name\":\"Vitalizer\",\"Is4Dx\":false,\"Is3D\":false},{\"Id\":8,\"Name\":\"CVS SPF 8\",\"Is4Dx\":true,\"Is3D\":true},{\"Id\":9,\"Name\":\"CYZONE\",\"Is4Dx\":false,\"Is3D\":false}],\"Movie\":[{\"Id\":1,\"Title\":\"Gui Si (Silk)\",\"Genre\":1,\"Duration\":\"02:21:00\",\"Rating\":9.0,\"Director\":\"Perl Swyne\"},{\"Id\":2,\"Title\":\"Absurdistan\",\"Genre\":2,\"Duration\":\"02:34:00\",\"Rating\":9.0,\"Director\":\"Emelia Weagener\"},{\"Id\":3,\"Title\":\"Cranford\",\"Genre\":1,\"Duration\":\"02:24:00\",\"Rating\":2.0,\"Director\":\"Avivah Westcot\"},{\"Id\":4,\"Title\":\"Living \'til the End\",\"Genre\":1,\"Duration\":\"02:55:00\",\"Rating\":5.0,\"Director\":\"Doralin Pray\"},{\"Id\":5,\"Title\":\"Creator\",\"Genre\":2,\"Duration\":\"01:05:00\",\"Rating\":6.0,\"Director\":\"Konstantine Kierans\"},{\"Id\":6,\"Title\":\"Trojan Eddie\",\"Genre\":3,\"Duration\":\"02:57:00\",\"Rating\":5.0,\"Director\":\"Mark Frany\"},{\"Id\":7,\"Title\":\"One Day\",\"Genre\":1,\"Duration\":\"01:02:00\",\"Rating\":3.0,\"Director\":\"Marcelle Huggett\"},{\"Id\":8,\"Title\":\"Stroker Ace\",\"Genre\":0,\"Duration\":\"01:55:00\",\"Rating\":3.0,\"Director\":\"Inessa Mertsching\"},{\"Id\":9,\"Title\":\"White Man\'s Burden\",\"Genre\":1,\"Duration\":\"02:02:00\",\"Rating\":7.0,\"Director\":\"Joannes Alekseev\"},{\"Id\":10,\"Title\":\"Fahrenhype 9/11\",\"Genre\":6,\"Duration\":\"02:36:00\",\"Rating\":8.0,\"Director\":\"Rayna Forsyth\"},{\"Id\":11,\"Title\":\"Sorcerer\",\"Genre\":0,\"Duration\":\"02:42:00\",\"Rating\":6.0,\"Director\":\"Clifford Ramelet\"},{\"Id\":12,\"Title\":\"Silent Partner, The\",\"Genre\":3,\"Duration\":\"02:20:00\",\"Rating\":7.0,\"Director\":\"Cally Beals\"},{\"Id\":13,\"Title\":\"Shaggy D.A., The\",\"Genre\":7,\"Duration\":\"01:25:00\",\"Rating\":5.0,\"Director\":\"Tallia Siveyer\"},{\"Id\":14,\"Title\":\"Host, The (Gwoemul)\",\"Genre\":2,\"Duration\":\"01:00:00\",\"Rating\":9.0,\"Director\":\"Harmonia Gannon\"},{\"Id\":15,\"Title\":\"T.N.T.\",\"Genre\":0,\"Duration\":\"02:14:00\",\"Rating\":8.0,\"Director\":\"Inesita MacGlory\"},{\"Id\":16,\"Title\":\"Free Willy\",\"Genre\":1,\"Duration\":\"02:51:00\",\"Rating\":1.0,\"Director\":\"Sheree Lindenman\"},{\"Id\":17,\"Title\":\"Moog\",\"Genre\":9,\"Duration\":\"01:06:00\",\"Rating\":4.0,\"Director\":\"Wash Couth\"},{\"Id\":18,\"Title\":\"SIS\",\"Genre\":0,\"Duration\":\"02:14:00\",\"Rating\":10.0,\"Director\":\"Tuesday Scothern\"},{\"Id\":19,\"Title\":\"Prey, The (La proie)\",\"Genre\":0,\"Duration\":\"02:08:00\",\"Rating\":5.0,\"Director\":\"Israel Sircomb\"},{\"Id\":20,\"Title\":\"Best Worst Movie\",\"Genre\":6,\"Duration\":\"02:59:00\",\"Rating\":3.0,\"Director\":\"Hamel Della Scala\"},{\"Id\":21,\"Title\":\"Gloriously Wasted\",\"Genre\":2,\"Duration\":\"01:16:00\",\"Rating\":5.0,\"Director\":\"Shaughn Sattin\"}],\"Projection\":[{\"Id\":1,\"MovieId\":6,\"HallId\":4,\"DateTime\":\"2019-05-12T05:51:29\"},{\"Id\":2,\"MovieId\":3,\"HallId\":7,\"DateTime\":\"2019-05-08T18:21:11\"},{\"Id\":3,\"MovieId\":19,\"HallId\":8,\"DateTime\":\"2019-04-20T02:16:22\"},{\"Id\":4,\"MovieId\":1,\"HallId\":2,\"DateTime\":\"2019-05-03T11:50:44\"},{\"Id\":5,\"MovieId\":1,\"HallId\":7,\"DateTime\":\"2019-05-03T21:27:41\"},{\"Id\":6,\"MovieId\":8,\"HallId\":8,\"DateTime\":\"2019-05-18T22:58:37\"},{\"Id\":7,\"MovieId\":5,\"HallId\":3,\"DateTime\":\"2019-05-14T00:04:12\"},{\"Id\":8,\"MovieId\":3,\"HallId\":6,\"DateTime\":\"2019-04-30T19:12:17\"},{\"Id\":9,\"MovieId\":17,\"HallId\":4,\"DateTime\":\"2019-04-20T03:48:11\"},{\"Id\":10,\"MovieId\":1,\"HallId\":1,\"DateTime\":\"2019-04-19T18:54:51\"},{\"Id\":11,\"MovieId\":4,\"HallId\":1,\"DateTime\":\"2019-05-10T15:36:16\"},{\"Id\":12,\"MovieId\":9,\"HallId\":5,\"DateTime\":\"2019-05-10T11:08:44\"},{\"Id\":13,\"MovieId\":12,\"HallId\":6,\"DateTime\":\"2019-04-24T05:08:44\"},{\"Id\":14,\"MovieId\":11,\"HallId\":1,\"DateTime\":\"2019-05-22T14:31:29\"},{\"Id\":15,\"MovieId\":6,\"HallId\":6,\"DateTime\":\"2019-05-14T02:11:11\"},{\"Id\":16,\"MovieId\":8,\"HallId\":3,\"DateTime\":\"2019-05-23T20:22:41\"},{\"Id\":17,\"MovieId\":11,\"HallId\":6,\"DateTime\":\"2019-05-19T08:00:32\"},{\"Id\":18,\"MovieId\":6,\"HallId\":5,\"DateTime\":\"2019-04-25T02:56:36\"},{\"Id\":19,\"MovieId\":11,\"HallId\":2,\"DateTime\":\"2019-05-09T06:08:57\"},{\"Id\":20,\"MovieId\":18,\"HallId\":1,\"DateTime\":\"2019-04-29T22:49:17\"},{\"Id\":21,\"MovieId\":17,\"HallId\":3,\"DateTime\":\"2019-04-29T08:09:47\"},{\"Id\":22,\"MovieId\":7,\"HallId\":1,\"DateTime\":\"2019-04-23T12:04:05\"},{\"Id\":23,\"MovieId\":6,\"HallId\":7,\"DateTime\":\"2019-05-01T19:43:03\"},{\"Id\":24,\"MovieId\":15,\"HallId\":8,\"DateTime\":\"2019-04-21T13:12:29\"},{\"Id\":25,\"MovieId\":18,\"HallId\":3,\"DateTime\":\"2019-05-16T16:23:07\"},{\"Id\":26,\"MovieId\":10,\"HallId\":5,\"DateTime\":\"2019-05-01T00:11:21\"},{\"Id\":27,\"MovieId\":21,\"HallId\":5,\"DateTime\":\"2019-05-03T16:56:12\"},{\"Id\":28,\"MovieId\":9,\"HallId\":2,\"DateTime\":\"2019-05-01T13:12:56\"},{\"Id\":29,\"MovieId\":14,\"HallId\":3,\"DateTime\":\"2019-04-28T19:53:01\"}],\"Seat\":[{\"Id\":1,\"HallId\":1},{\"Id\":160,\"HallId\":1},{\"Id\":161,\"HallId\":1},{\"Id\":162,\"HallId\":1},{\"Id\":163,\"HallId\":1},{\"Id\":164,\"HallId\":1},{\"Id\":165,\"HallId\":1},{\"Id\":166,\"HallId\":1},{\"Id\":167,\"HallId\":1},{\"Id\":168,\"HallId\":1},{\"Id\":169,\"HallId\":1},{\"Id\":170,\"HallId\":1},{\"Id\":171,\"HallId\":1},{\"Id\":173,\"HallId\":1},{\"Id\":192,\"HallId\":1},{\"Id\":193,\"HallId\":1},{\"Id\":194,\"HallId\":1},{\"Id\":195,\"HallId\":1},{\"Id\":196,\"HallId\":1},{\"Id\":197,\"HallId\":1},{\"Id\":198,\"HallId\":1},{\"Id\":199,\"HallId\":1},{\"Id\":200,\"HallId\":1},{\"Id\":201,\"HallId\":1},{\"Id\":202,\"HallId\":1},{\"Id\":203,\"HallId\":1},{\"Id\":204,\"HallId\":1},{\"Id\":205,\"HallId\":1},{\"Id\":206,\"HallId\":1},{\"Id\":207,\"HallId\":1},{\"Id\":208,\"HallId\":1},{\"Id\":209,\"HallId\":1},{\"Id\":210,\"HallId\":1},{\"Id\":211,\"HallId\":1},{\"Id\":212,\"HallId\":1},{\"Id\":213,\"HallId\":1},{\"Id\":214,\"HallId\":1},{\"Id\":215,\"HallId\":1},{\"Id\":216,\"HallId\":1},{\"Id\":217,\"HallId\":1},{\"Id\":218,\"HallId\":1},{\"Id\":219,\"HallId\":1},{\"Id\":220,\"HallId\":1},{\"Id\":222,\"HallId\":1},{\"Id\":223,\"HallId\":1},{\"Id\":224,\"HallId\":1},{\"Id\":225,\"HallId\":1},{\"Id\":226,\"HallId\":1},{\"Id\":227,\"HallId\":1},{\"Id\":228,\"HallId\":1},{\"Id\":229,\"HallId\":1},{\"Id\":230,\"HallId\":1},{\"Id\":157,\"HallId\":2},{\"Id\":158,\"HallId\":2},{\"Id\":159,\"HallId\":2},{\"Id\":172,\"HallId\":2},{\"Id\":174,\"HallId\":2},{\"Id\":175,\"HallId\":2},{\"Id\":176,\"HallId\":2},{\"Id\":177,\"HallId\":2},{\"Id\":178,\"HallId\":2},{\"Id\":179,\"HallId\":2},{\"Id\":180,\"HallId\":2},{\"Id\":181,\"HallId\":2},{\"Id\":182,\"HallId\":2},{\"Id\":183,\"HallId\":2},{\"Id\":184,\"HallId\":2},{\"Id\":185,\"HallId\":2},{\"Id\":186,\"HallId\":2},{\"Id\":187,\"HallId\":2},{\"Id\":188,\"HallId\":2},{\"Id\":189,\"HallId\":2},{\"Id\":190,\"HallId\":2},{\"Id\":191,\"HallId\":2},{\"Id\":221,\"HallId\":2},{\"Id\":231,\"HallId\":2},{\"Id\":232,\"HallId\":2},{\"Id\":233,\"HallId\":2},{\"Id\":278,\"HallId\":2},{\"Id\":279,\"HallId\":2},{\"Id\":280,\"HallId\":2},{\"Id\":281,\"HallId\":2},{\"Id\":282,\"HallId\":2},{\"Id\":283,\"HallId\":2},{\"Id\":284,\"HallId\":2},{\"Id\":285,\"HallId\":2},{\"Id\":286,\"HallId\":2},{\"Id\":287,\"HallId\":2},{\"Id\":238,\"HallId\":3},{\"Id\":239,\"HallId\":3},{\"Id\":240,\"HallId\":3},{\"Id\":241,\"HallId\":3},{\"Id\":242,\"HallId\":3},{\"Id\":243,\"HallId\":3},{\"Id\":244,\"HallId\":3},{\"Id\":245,\"HallId\":3},{\"Id\":246,\"HallId\":3},{\"Id\":247,\"HallId\":3},{\"Id\":248,\"HallId\":3},{\"Id\":260,\"HallId\":3},{\"Id\":271,\"HallId\":3},{\"Id\":272,\"HallId\":3},{\"Id\":273,\"HallId\":3},{\"Id\":274,\"HallId\":3},{\"Id\":275,\"HallId\":3},{\"Id\":276,\"HallId\":3},{\"Id\":277,\"HallId\":3},{\"Id\":288,\"HallId\":3},{\"Id\":289,\"HallId\":3},{\"Id\":290,\"HallId\":3},{\"Id\":291,\"HallId\":3},{\"Id\":292,\"HallId\":3},{\"Id\":293,\"HallId\":3},{\"Id\":294,\"HallId\":3},{\"Id\":295,\"HallId\":3},{\"Id\":296,\"HallId\":3},{\"Id\":297,\"HallId\":3},{\"Id\":298,\"HallId\":3},{\"Id\":299,\"HallId\":3},{\"Id\":300,\"HallId\":3},{\"Id\":301,\"HallId\":3},{\"Id\":302,\"HallId\":3},{\"Id\":303,\"HallId\":3},{\"Id\":304,\"HallId\":3},{\"Id\":305,\"HallId\":3},{\"Id\":306,\"HallId\":3},{\"Id\":307,\"HallId\":3},{\"Id\":308,\"HallId\":3},{\"Id\":52,\"HallId\":4},{\"Id\":53,\"HallId\":4},{\"Id\":54,\"HallId\":4},{\"Id\":115,\"HallId\":4},{\"Id\":155,\"HallId\":4},{\"Id\":156,\"HallId\":4},{\"Id\":234,\"HallId\":4},{\"Id\":235,\"HallId\":4},{\"Id\":236,\"HallId\":4},{\"Id\":237,\"HallId\":4},{\"Id\":249,\"HallId\":4},{\"Id\":250,\"HallId\":4},{\"Id\":251,\"HallId\":4},{\"Id\":252,\"HallId\":4},{\"Id\":253,\"HallId\":4},{\"Id\":254,\"HallId\":4},{\"Id\":255,\"HallId\":4},{\"Id\":256,\"HallId\":4},{\"Id\":257,\"HallId\":4},{\"Id\":258,\"HallId\":4},{\"Id\":259,\"HallId\":4},{\"Id\":261,\"HallId\":4},{\"Id\":262,\"HallId\":4},{\"Id\":263,\"HallId\":4},{\"Id\":264,\"HallId\":4},{\"Id\":265,\"HallId\":4},{\"Id\":266,\"HallId\":4},{\"Id\":267,\"HallId\":4},{\"Id\":268,\"HallId\":4},{\"Id\":269,\"HallId\":4},{\"Id\":270,\"HallId\":4},{\"Id\":38,\"HallId\":5},{\"Id\":39,\"HallId\":5},{\"Id\":40,\"HallId\":5},{\"Id\":41,\"HallId\":5},{\"Id\":42,\"HallId\":5},{\"Id\":43,\"HallId\":5},{\"Id\":44,\"HallId\":5},{\"Id\":45,\"HallId\":5},{\"Id\":46,\"HallId\":5},{\"Id\":47,\"HallId\":5},{\"Id\":48,\"HallId\":5},{\"Id\":49,\"HallId\":5},{\"Id\":50,\"HallId\":5},{\"Id\":51,\"HallId\":5},{\"Id\":55,\"HallId\":5},{\"Id\":56,\"HallId\":5},{\"Id\":57,\"HallId\":5},{\"Id\":58,\"HallId\":5},{\"Id\":59,\"HallId\":5},{\"Id\":60,\"HallId\":5},{\"Id\":61,\"HallId\":5},{\"Id\":62,\"HallId\":5},{\"Id\":63,\"HallId\":5},{\"Id\":64,\"HallId\":5},{\"Id\":65,\"HallId\":5},{\"Id\":67,\"HallId\":5},{\"Id\":68,\"HallId\":5},{\"Id\":69,\"HallId\":5},{\"Id\":70,\"HallId\":5},{\"Id\":71,\"HallId\":5},{\"Id\":72,\"HallId\":5},{\"Id\":73,\"HallId\":5},{\"Id\":74,\"HallId\":5},{\"Id\":75,\"HallId\":5},{\"Id\":2,\"HallId\":6},{\"Id\":3,\"HallId\":6},{\"Id\":4,\"HallId\":6},{\"Id\":5,\"HallId\":6},{\"Id\":6,\"HallId\":6},{\"Id\":7,\"HallId\":6},{\"Id\":8,\"HallId\":6},{\"Id\":9,\"HallId\":6},{\"Id\":10,\"HallId\":6},{\"Id\":11,\"HallId\":6},{\"Id\":12,\"HallId\":6},{\"Id\":13,\"HallId\":6},{\"Id\":14,\"HallId\":6},{\"Id\":15,\"HallId\":6},{\"Id\":16,\"HallId\":6},{\"Id\":17,\"HallId\":6},{\"Id\":18,\"HallId\":6},{\"Id\":19,\"HallId\":6},{\"Id\":20,\"HallId\":6},{\"Id\":33,\"HallId\":6},{\"Id\":34,\"HallId\":6},{\"Id\":35,\"HallId\":6},{\"Id\":36,\"HallId\":6},{\"Id\":37,\"HallId\":6},{\"Id\":21,\"HallId\":7},{\"Id\":22,\"HallId\":7},{\"Id\":23,\"HallId\":7},{\"Id\":24,\"HallId\":7},{\"Id\":25,\"HallId\":7},{\"Id\":26,\"HallId\":7},{\"Id\":27,\"HallId\":7},{\"Id\":28,\"HallId\":7},{\"Id\":29,\"HallId\":7},{\"Id\":30,\"HallId\":7},{\"Id\":31,\"HallId\":7},{\"Id\":32,\"HallId\":7},{\"Id\":66,\"HallId\":7},{\"Id\":76,\"HallId\":7},{\"Id\":77,\"HallId\":7},{\"Id\":78,\"HallId\":7},{\"Id\":118,\"HallId\":7},{\"Id\":119,\"HallId\":7},{\"Id\":120,\"HallId\":7},{\"Id\":121,\"HallId\":7},{\"Id\":122,\"HallId\":7},{\"Id\":123,\"HallId\":7},{\"Id\":124,\"HallId\":7},{\"Id\":125,\"HallId\":7},{\"Id\":126,\"HallId\":7},{\"Id\":127,\"HallId\":7},{\"Id\":128,\"HallId\":7},{\"Id\":129,\"HallId\":7},{\"Id\":130,\"HallId\":7},{\"Id\":131,\"HallId\":7},{\"Id\":132,\"HallId\":7},{\"Id\":133,\"HallId\":7},{\"Id\":134,\"HallId\":7},{\"Id\":135,\"HallId\":7},{\"Id\":136,\"HallId\":7},{\"Id\":140,\"HallId\":7},{\"Id\":141,\"HallId\":7},{\"Id\":142,\"HallId\":7},{\"Id\":143,\"HallId\":7},{\"Id\":145,\"HallId\":7},{\"Id\":146,\"HallId\":7},{\"Id\":147,\"HallId\":7},{\"Id\":148,\"HallId\":7},{\"Id\":149,\"HallId\":7},{\"Id\":150,\"HallId\":7},{\"Id\":151,\"HallId\":7},{\"Id\":152,\"HallId\":7},{\"Id\":153,\"HallId\":7},{\"Id\":79,\"HallId\":8},{\"Id\":80,\"HallId\":8},{\"Id\":81,\"HallId\":8},{\"Id\":82,\"HallId\":8},{\"Id\":83,\"HallId\":8},{\"Id\":84,\"HallId\":8},{\"Id\":85,\"HallId\":8},{\"Id\":86,\"HallId\":8},{\"Id\":87,\"HallId\":8},{\"Id\":88,\"HallId\":8},{\"Id\":89,\"HallId\":8},{\"Id\":90,\"HallId\":8},{\"Id\":91,\"HallId\":8},{\"Id\":92,\"HallId\":8},{\"Id\":93,\"HallId\":8},{\"Id\":105,\"HallId\":8},{\"Id\":116,\"HallId\":8},{\"Id\":117,\"HallId\":8},{\"Id\":137,\"HallId\":8},{\"Id\":138,\"HallId\":8},{\"Id\":139,\"HallId\":8},{\"Id\":144,\"HallId\":8},{\"Id\":94,\"HallId\":9},{\"Id\":95,\"HallId\":9},{\"Id\":96,\"HallId\":9},{\"Id\":97,\"HallId\":9},{\"Id\":98,\"HallId\":9},{\"Id\":99,\"HallId\":9},{\"Id\":100,\"HallId\":9},{\"Id\":101,\"HallId\":9},{\"Id\":102,\"HallId\":9},{\"Id\":103,\"HallId\":9},{\"Id\":104,\"HallId\":9},{\"Id\":106,\"HallId\":9},{\"Id\":107,\"HallId\":9},{\"Id\":108,\"HallId\":9},{\"Id\":109,\"HallId\":9},{\"Id\":110,\"HallId\":9},{\"Id\":111,\"HallId\":9},{\"Id\":112,\"HallId\":9},{\"Id\":113,\"HallId\":9},{\"Id\":114,\"HallId\":9},{\"Id\":154,\"HallId\":9},{\"Id\":309,\"HallId\":9}],\"Ticket\":[{\"Id\":1,\"Price\":12.50,\"CustomerId\":1,\"ProjectionId\":25},{\"Id\":2,\"Price\":15.0,\"CustomerId\":14,\"ProjectionId\":23},{\"Id\":3,\"Price\":7.0,\"CustomerId\":14,\"ProjectionId\":23},{\"Id\":4,\"Price\":9.50,\"CustomerId\":14,\"ProjectionId\":23},{\"Id\":5,\"Price\":11.0,\"CustomerId\":14,\"ProjectionId\":23},{\"Id\":6,\"Price\":15.0,\"CustomerId\":15,\"ProjectionId\":16},{\"Id\":7,\"Price\":7.0,\"CustomerId\":15,\"ProjectionId\":16},{\"Id\":8,\"Price\":12.50,\"CustomerId\":16,\"ProjectionId\":26},{\"Id\":9,\"Price\":15.0,\"CustomerId\":16,\"ProjectionId\":26},{\"Id\":10,\"Price\":7.0,\"CustomerId\":39,\"ProjectionId\":14},{\"Id\":11,\"Price\":9.50,\"CustomerId\":39,\"ProjectionId\":14},{\"Id\":12,\"Price\":11.0,\"CustomerId\":63,\"ProjectionId\":28},{\"Id\":13,\"Price\":12.50,\"CustomerId\":63,\"ProjectionId\":28},{\"Id\":14,\"Price\":9.50,\"CustomerId\":64,\"ProjectionId\":13},{\"Id\":15,\"Price\":11.0,\"CustomerId\":64,\"ProjectionId\":13},{\"Id\":16,\"Price\":7.0,\"CustomerId\":65,\"ProjectionId\":28},{\"Id\":17,\"Price\":9.50,\"CustomerId\":65,\"ProjectionId\":28},{\"Id\":18,\"Price\":15.0,\"CustomerId\":66,\"ProjectionId\":12},{\"Id\":19,\"Price\":7.0,\"CustomerId\":66,\"ProjectionId\":12},{\"Id\":20,\"Price\":7.0,\"CustomerId\":70,\"ProjectionId\":28},{\"Id\":21,\"Price\":11.0,\"CustomerId\":14,\"ProjectionId\":10},{\"Id\":22,\"Price\":9.50,\"CustomerId\":12,\"ProjectionId\":5},{\"Id\":23,\"Price\":7.0,\"CustomerId\":12,\"ProjectionId\":5},{\"Id\":24,\"Price\":11.0,\"CustomerId\":11,\"ProjectionId\":14},{\"Id\":25,\"Price\":9.50,\"CustomerId\":24,\"ProjectionId\":11},{\"Id\":26,\"Price\":15.0,\"CustomerId\":25,\"ProjectionId\":10},{\"Id\":27,\"Price\":11.0,\"CustomerId\":30,\"ProjectionId\":28},{\"Id\":28,\"Price\":12.50,\"CustomerId\":30,\"ProjectionId\":28},{\"Id\":29,\"Price\":9.50,\"CustomerId\":31,\"ProjectionId\":17},{\"Id\":30,\"Price\":15.0,\"CustomerId\":32,\"ProjectionId\":25},{\"Id\":31,\"Price\":11.0,\"CustomerId\":33,\"ProjectionId\":1},{\"Id\":32,\"Price\":7.0,\"CustomerId\":34,\"ProjectionId\":12},{\"Id\":33,\"Price\":9.50,\"CustomerId\":38,\"ProjectionId\":11},{\"Id\":34,\"Price\":9.50,\"CustomerId\":70,\"ProjectionId\":28},{\"Id\":35,\"Price\":15.0,\"CustomerId\":21,\"ProjectionId\":27},{\"Id\":36,\"Price\":9.50,\"CustomerId\":19,\"ProjectionId\":2},{\"Id\":37,\"Price\":12.50,\"CustomerId\":4,\"ProjectionId\":13},{\"Id\":38,\"Price\":9.50,\"CustomerId\":5,\"ProjectionId\":9},{\"Id\":39,\"Price\":11.0,\"CustomerId\":7,\"ProjectionId\":16},{\"Id\":40,\"Price\":7.0,\"CustomerId\":18,\"ProjectionId\":3},{\"Id\":41,\"Price\":9.50,\"CustomerId\":18,\"ProjectionId\":3},{\"Id\":42,\"Price\":15.0,\"CustomerId\":11,\"ProjectionId\":14},{\"Id\":43,\"Price\":7.0,\"CustomerId\":11,\"ProjectionId\":14},{\"Id\":44,\"Price\":9.50,\"CustomerId\":11,\"ProjectionId\":14},{\"Id\":45,\"Price\":7.0,\"CustomerId\":19,\"ProjectionId\":2},{\"Id\":46,\"Price\":11.0,\"CustomerId\":22,\"ProjectionId\":26},{\"Id\":47,\"Price\":15.0,\"CustomerId\":71,\"ProjectionId\":29},{\"Id\":48,\"Price\":12.50,\"CustomerId\":72,\"ProjectionId\":4},{\"Id\":49,\"Price\":7.0,\"CustomerId\":48,\"ProjectionId\":10},{\"Id\":50,\"Price\":9.50,\"CustomerId\":48,\"ProjectionId\":10},{\"Id\":51,\"Price\":11.0,\"CustomerId\":59,\"ProjectionId\":4},{\"Id\":52,\"Price\":11.0,\"CustomerId\":59,\"ProjectionId\":4},{\"Id\":53,\"Price\":11.0,\"CustomerId\":59,\"ProjectionId\":4},{\"Id\":54,\"Price\":11.0,\"CustomerId\":59,\"ProjectionId\":4},{\"Id\":55,\"Price\":11.0,\"CustomerId\":55,\"ProjectionId\":3},{\"Id\":56,\"Price\":12.50,\"CustomerId\":55,\"ProjectionId\":3},{\"Id\":57,\"Price\":9.50,\"CustomerId\":56,\"ProjectionId\":21},{\"Id\":58,\"Price\":11.0,\"CustomerId\":56,\"ProjectionId\":21},{\"Id\":59,\"Price\":7.0,\"CustomerId\":58,\"ProjectionId\":3},{\"Id\":60,\"Price\":9.50,\"CustomerId\":58,\"ProjectionId\":3},{\"Id\":61,\"Price\":11.0,\"CustomerId\":58,\"ProjectionId\":3},{\"Id\":62,\"Price\":12.50,\"CustomerId\":58,\"ProjectionId\":3},{\"Id\":63,\"Price\":9.50,\"CustomerId\":1,\"ProjectionId\":3},{\"Id\":64,\"Price\":9.50,\"CustomerId\":1,\"ProjectionId\":3},{\"Id\":65,\"Price\":9.50,\"CustomerId\":1,\"ProjectionId\":3},{\"Id\":66,\"Price\":9.50,\"CustomerId\":1,\"ProjectionId\":25},{\"Id\":67,\"Price\":11.0,\"CustomerId\":1,\"ProjectionId\":25},{\"Id\":68,\"Price\":11.0,\"CustomerId\":47,\"ProjectionId\":9},{\"Id\":69,\"Price\":9.50,\"CustomerId\":47,\"ProjectionId\":9},{\"Id\":70,\"Price\":7.0,\"CustomerId\":47,\"ProjectionId\":9},{\"Id\":71,\"Price\":7.0,\"CustomerId\":45,\"ProjectionId\":26},{\"Id\":72,\"Price\":12.50,\"CustomerId\":72,\"ProjectionId\":4},{\"Id\":73,\"Price\":12.50,\"CustomerId\":72,\"ProjectionId\":3},{\"Id\":74,\"Price\":12.50,\"CustomerId\":72,\"ProjectionId\":3},{\"Id\":75,\"Price\":12.50,\"CustomerId\":72,\"ProjectionId\":17},{\"Id\":76,\"Price\":15.0,\"CustomerId\":72,\"ProjectionId\":17},{\"Id\":77,\"Price\":7.0,\"CustomerId\":72,\"ProjectionId\":17},{\"Id\":78,\"Price\":12.50,\"CustomerId\":73,\"ProjectionId\":2},{\"Id\":79,\"Price\":15.0,\"CustomerId\":73,\"ProjectionId\":2},{\"Id\":80,\"Price\":7.0,\"CustomerId\":75,\"ProjectionId\":1},{\"Id\":81,\"Price\":7.0,\"CustomerId\":71,\"ProjectionId\":29},{\"Id\":82,\"Price\":9.50,\"CustomerId\":75,\"ProjectionId\":1},{\"Id\":83,\"Price\":12.50,\"CustomerId\":62,\"ProjectionId\":27},{\"Id\":84,\"Price\":15.0,\"CustomerId\":62,\"ProjectionId\":27},{\"Id\":85,\"Price\":11.0,\"CustomerId\":61,\"ProjectionId\":3},{\"Id\":86,\"Price\":12.50,\"CustomerId\":61,\"ProjectionId\":3},{\"Id\":87,\"Price\":9.50,\"CustomerId\":60,\"ProjectionId\":27},{\"Id\":88,\"Price\":11.0,\"CustomerId\":60,\"ProjectionId\":27},{\"Id\":89,\"Price\":15.0,\"CustomerId\":43,\"ProjectionId\":12},{\"Id\":90,\"Price\":7.0,\"CustomerId\":43,\"ProjectionId\":12},{\"Id\":91,\"Price\":15.0,\"CustomerId\":45,\"ProjectionId\":26},{\"Id\":92,\"Price\":11.0,\"CustomerId\":75,\"ProjectionId\":1},{\"Id\":93,\"Price\":9.50,\"CustomerId\":81,\"ProjectionId\":3}]}";

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