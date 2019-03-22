namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using Models;

    using ViewModels.Positions;
    using ViewModels.Orders;
    using FastFood.Web.ViewModels.Items;
    using System;
    using FastFood.Web.ViewModels.Employees;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {

            #region POSITIONS

            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            #endregion

            #region EMPLOYEES

            this.CreateMap<RegisterEmployeeViewModel, Employee>()
                .ForMember(x => x.Name, y => y.MapFrom(o => o.)

            #endregion


            #region ORDERS

            this.CreateMap<CreateOrderInputModel, OrderItem>()
                .ForMember(x => x.ItemId, y => y.MapFrom(inputModel => inputModel.ItemId))
                .ForMember(x => x.Quantity, y => y.MapFrom(inputModel => inputModel.Quantity));


            #endregion

            #region ITEMS

            this.CreateMap<CreateItemInputModel, Item>()
                .ForMember(x => x.Name, y => y.MapFrom(inputModel => inputModel.Name))
                .ForMember(x => x.Price, y => y.MapFrom(inputModel => inputModel.Price))
                .ForMember(x => x.CategoryId, y => y.MapFrom(inputModel => inputModel.CategoryId));

            #endregion
        }
    }
}
