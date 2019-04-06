namespace SoftJail.DataProcessor
{

    using Data;
    using KotsevHelper;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper.QueryableExtensions;
    using AutoMapper;
    using System.Text;
    using System.Xml.Serialization;
    using System.Xml.Linq;
    using SoftJail.Data.Models.Enums;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var parsed = KotsevExamHelper.DeserializeObjectFromJson<DepartmentImportDto[]>(jsonString)
                .ToList();

            StringBuilder builder = new StringBuilder();

            var mapped = new List<Department>();

            foreach (var x in parsed)
            {

                if (!KotsevExamHelper.IsValid(x) || x.Cells.Any(y => KotsevExamHelper.IsValid(y) == false))
                {
                    builder.AppendLine("Invalid Data");
                    continue;
                }

                var department = Mapper.Map<Department>(x);

                //var cells = x.Cells
                //    .Distinct()
                //    .AsQueryable()
                //    .ProjectTo<Cell>(new { currentDepartment = department })
                //    .ToList();

                //cells.ForEach(c => department.Cells.Add(c));

                mapped.Add(department);
                builder.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");

            }

            context.Departments.AddRange(mapped);
            context.SaveChanges();

            return builder.ToString().TrimEnd();

        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var parsed = KotsevExamHelper.DeserializeObjectFromJson<List<PrisonerImportDto>>(jsonString);
            var mapped = new List<Prisoner>();

            var builder = new StringBuilder();

            foreach (var dto in parsed)
            {

                if (!KotsevExamHelper.IsValid(dto) || dto.Mails.Any(x => KotsevExamHelper.IsValid(x) == false))
                {
                    builder.AppendLine("Invalid Data");
                    continue;
                }

                var currentPrisoner = Mapper.Map<Prisoner>(dto);
                mapped.Add(currentPrisoner);

                builder.AppendLine($"Imported {currentPrisoner.FullName} {currentPrisoner.Age} years old");

            }

            context.Prisoners.AddRange(mapped);
            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var doc = XDocument.Parse(xmlString);

            var elements = doc.Root
                .Elements()
                .ToList();

            var builder = new StringBuilder();
            var mapped = new List<Officer>();

            foreach (var element in elements)
            {
                var currentOfficer = new Officer()
                {
                    FullName = element.Element("Name").Value ?? null,
                    Salary = decimal.Parse(element.Element("Money").Value ?? "-0.01"),
                };

                Weapon weaponType;
                bool isWeaponParseSuccessfull = Enum.TryParse<Weapon>(element.Element("Weapon").Value, out weaponType);

                Position positionType;
                bool isPositionParseSuccessfull = Enum.TryParse<Position>(element.Element("Position").Value, out positionType);

                if (currentOfficer.FullName == null || currentOfficer.Salary < 0
                    || !isWeaponParseSuccessfull || !isPositionParseSuccessfull)
                {
                    builder.AppendLine("Invalid Data");
                    continue;
                }

                currentOfficer.Position = positionType;
                currentOfficer.Weapon = weaponType;

                var desiredDepartmentId = int.Parse(element.Element("DepartmentId").Value);

                var department = KotsevExamHelper.GetObjectFromSet<Department, SoftJailDbContext>(x => x.Id == desiredDepartmentId, context);

                //if (department == null)
                //{
                //    builder.AppendLine("Invalid Data");
                //    continue;
                //}

                //currentOfficer.Department = department;

                currentOfficer.DepartmentId = desiredDepartmentId;

                foreach (var prisoner in element.Element("Prisoners").Elements().ToList())
                {

                    var id = int.Parse(prisoner.Attribute("id").Value ?? "-1");

                    if (id < 0 || currentOfficer.OfficerPrisoners.Any(x => x.PrisonerId == id))
                        continue;

                    //var prisonerInstance = KotsevExamHelper.GetObjectFromSet<Prisoner, SoftJailDbContext>(x => x.Id == id, context);

                    //if (prisonerInstance == null)
                    //continue;

                    currentOfficer.OfficerPrisoners.Add(new OfficerPrisoner { PrisonerId = id, Officer = currentOfficer });
                }

                mapped.Add(currentOfficer);
                builder.AppendLine($"Imported {currentOfficer.FullName} ({currentOfficer.OfficerPrisoners.Count} prisoners)");
            }

            //var count = mapped.SelectMany(x => x.OfficerPrisoners).Count();
            context.Officers.AddRange(mapped);
            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }
    }
}