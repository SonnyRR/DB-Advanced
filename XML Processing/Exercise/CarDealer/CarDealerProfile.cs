using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Models;
using System.Linq;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {

            // Ex 14 mappings
            CreateMap<Car, CarWithDistanceDto>();

            // Ex 15 mappings
            CreateMap<Car, BmwCarsDto>();

            // Ex 16 mappings
            CreateMap<Supplier, LocalSuppliersDto>();

            // Ex 17 mappings
            CreateMap<Car, CarAndPartsDto>()
                .ForMember(x => x.Parts, y => y.MapFrom(obj => obj.PartCars.Select(z => z.Part)));

            CreateMap<Part, PartDto>();
        }
    }
}
