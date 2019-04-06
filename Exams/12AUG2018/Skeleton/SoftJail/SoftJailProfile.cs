namespace SoftJail
{
    using AutoMapper;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {

            // 01 Import mappings
            Department currentDepartment = null;

            CreateMap<CellImportDto, Cell>()
                .ForMember(x => x.Department, y => y.MapFrom(src => currentDepartment));

            CreateMap<DepartmentImportDto, Department>();

            // 02 Import mappings
            CreateMap<PrisonerImportDto, Prisoner>();
            CreateMap<MailInsertDto, Mail>();

        }
    }
}
