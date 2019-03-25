namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;
    using AutoMapper.QueryableExtensions;
    using FastFood.Models;
    using FastFood.Models.Enums;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.Select(x => x.Id).ToList(),
                Employees = this.context.Employees.Select(x => x.Id).ToList(),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("Error", "Home");
            }

            Order order = this.mapper.Map<Order>(model);
            order.DateTime = DateTime.Now;

            order.Type = Enum.Parse<OrderType>(model.OrderType);

            order.OrderItems.Add(new OrderItem()
            {
                Quantity = model.Quantity,
                ItemId = model.ItemId,
                Order = order
            });

            this.context.Orders.Add(order);

            this.context.SaveChanges();

            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var orders = this.context
                .Orders
                .ProjectTo<OrderAllViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(orders);
        }
    }
}
