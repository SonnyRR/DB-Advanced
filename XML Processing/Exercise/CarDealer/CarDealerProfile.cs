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
                .ForMember(x => x.Parts, y => y.MapFrom(obj => obj.PartCars
                    .Select(z => z.Part).OrderByDescending(z => z.Price)));

            CreateMap<Part, PartDto>();

            // Ex 18 mappings
            CreateMap<Customer, CustomerTotalSale>()
                .ForMember(x => x.FullName, y => y.MapFrom(obj => obj.Name))
                .ForMember(x => x.BoughtCars, y => y.MapFrom(obj => obj.Sales.Count))
                .ForMember(x => x.SpentMoney,
                    y => y.MapFrom(obj => obj.Sales.Sum(z => z.Car.PartCars.Sum(w => w.Part.Price))));

            // Ex 19 mappings
            CreateMap<Sale, FullSaleDto>()
                .ForMember(x => x.CustomerName, y => y.MapFrom(obj => obj.Customer.Name))
                .ForMember(x => x.Car, y => y.MapFrom(obj => obj.Car))
                .ForMember(x => x.Price, y => y.MapFrom(obj => obj.Car.PartCars.Sum(z => z.Part.Price)))
                .ForMember(x => x.PriceWithDiscount,
                    y => y.MapFrom(
                        obj => $"{obj.Car.PartCars.Sum(z => z.Part.Price) - (obj.Car.PartCars.Sum(w => w.Part.Price) * (obj.Discount / 100))}".TrimEnd('0')));

            CreateMap<Car, CarDto>()
                .ForMember(x => x.Make, y => y.MapFrom(obj => obj.Make))
                .ForMember(x => x.Model, y => y.MapFrom(obj => obj.Model))
                .ForMember(x => x.TravelledDistance, y => y.MapFrom(obj => obj.TravelledDistance));

        }
    }
}
