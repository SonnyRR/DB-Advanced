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
            throw new NotImplementedException();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }
    }
}