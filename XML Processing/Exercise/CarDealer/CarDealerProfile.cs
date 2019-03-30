using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {

            // Ex 14 mappings
            CreateMap<Car, CarWithDistanceDto>();
        }
    }
}
