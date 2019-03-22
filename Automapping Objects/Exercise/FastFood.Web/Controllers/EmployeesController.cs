namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;    

    using Data;
    using ViewModels.Employees;
    using FastFood.Models;
    using AutoMapper.QueryableExtensions;
    using System.Linq;

    public class EmployeesController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public EmployeesController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Register()
        {

            var employees = this.context.Employees
                .ProjectTo<RegisterEmployeeViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return this.View(employees);
        }

        [HttpPost]
        public IActionResult Register(RegisterEmployeeInputModel model)
        {

            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("Error", "Home");
            }

            Employee employee = this.mapper.Map<Employee>(model);

            this.context.Employees.Add(employee);

            this.context.SaveChanges();

            return this.RedirectToAction("All", "Employees");

        }

        public IActionResult All()
        {
            var employees = this.context
                .Employees
                .ProjectTo<EmployeesAllViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(employees);
        }
    }
}
