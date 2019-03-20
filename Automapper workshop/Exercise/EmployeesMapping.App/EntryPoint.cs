namespace EmployeesMapping.App
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    using CustomMapper;
    using EmployeesMapping.App.DTO;
    using EmployeesMapping.Data;
    using EmployeesMapping.App.Core;
    using EmployeesMapping.App.Core.Contracts;
    using EmployeesMapping.App.Commands.Contracts;
    using EmployeesMapping.App.Commands;

    public class EntryPoint
    {
        public static void Main()
        {
            IServiceProvider services = ConfigureServices();

            IEngine engine = new Engine(services);
            engine.Run();
        }

        public static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<EmployeesMappingContext>();
            services.AddTransient<ICommandInterpreter, CommandInterpreter>();
            services.AddTransient<Mapper>();

            return services.BuildServiceProvider();
        }
    }
}
