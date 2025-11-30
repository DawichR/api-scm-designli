using Microsoft.Extensions.Logging;
using ScmDesignli.Domain.Entities;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.Infrastructure.Persistence.Seeds
{
    public class EmployeeSeeding
    {
        public static Task<List<Employee>> Seed(ILogger logger, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Initialize seeding information for employees");
            try
            {
                var employees = new List<Employee>
                {
                    new Employee
                    {
                        Name = "Juan",
                        LastName = "Pérez",
                        Birthday = new DateTime(1985, 3, 15),
                        Email = "juan.perez@designli.co",
                        Department = Department.IT
                    },
                    new Employee
                    {
                        Name = "María",
                        LastName = "García",
                        Birthday = new DateTime(1990, 7, 22),
                        Email = "maria.garcia@designli.co",
                        Department = Department.HumanResources
                    },
                    new Employee
                    {
                        Name = "Carlos",
                        LastName = "Rodríguez",
                        Birthday = new DateTime(1988, 11, 5),
                        Email = "carlos.rodriguez@designli.co",
                        Department = Department.Finance
                    }
                };

                logger.LogInformation($"Successfully created {employees.Count} employees for seeding");
                return Task.FromResult(employees);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"The seeder method for {nameof(EmployeeSeeding)} has an error.");
                return Task.FromResult(new List<Employee>());
            }
        }
    }
}
