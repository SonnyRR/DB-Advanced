namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using Models;

    using ViewModels.Positions;
    using ViewModels.Orders;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Employees;
    using FastFood.Web.ViewModels.Categories;

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

            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId, y => y.MapFrom(p => p.Id));

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position, y => y.MapFrom(emp => emp.Position.Name));

            #endregion

            #region ORDERS

            this.CreateMap<CreateOrderInputModel, Order>();
            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.Employee, y => y.MapFrom(o => o.Employee.Name))
                .ForMember(x => x.DateTime, y => y.MapFrom(d => d.DateTime.ToString("dd-MM-yyyy H:mm")))
                .ForMember(x => x.OrderId, y => y.MapFrom(o => o.Id));


            #endregion

            #region ITEMS

            this.CreateMap<CreateItemInputModel, Item>();
            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(i => i.Category.Name));

            #endregion

            #region CATEGORIES

            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(inputModel => inputModel.CategoryName));

            this.CreateMap<Category, CategoryAllViewModel>();

            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(inputModel => inputModel.Id));

            #endregion
        }
    }
}
