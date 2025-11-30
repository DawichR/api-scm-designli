using Microsoft.Extensions.Logging;
using ScmDesignli.Application.Interfaces;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Domain.Entities;
using ScmDesignli.Infrastructure.Persistence.Seeds;

namespace ScmDesignli.Infrastructure.Persistence
{
    /// <summary>
    /// Data seeder
    /// </summary>
    public class DataSeeder : IDataSeeder
    {
        private readonly ILogger<DataSeeder> _logger;
        private readonly IRepository<Employee> _employeeRepository;

        public DataSeeder(ILogger<DataSeeder> logger, IRepository<Employee> employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Seed generic method for Seeding information around the app.
        /// </summary>
        /// <param name="cancellationToken">cancelation tokens</param>
        /// <returns>Success of seeding data</returns>
        /// <exception cref="Exception">exception</exception>
        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Get seed employees
                var employees = await EmployeeSeeding.Seed(_logger, cancellationToken);
                // Add to repository
                await _employeeRepository.AddRangeAsync(employees);

                _logger.LogInformation($"Successfully seeded {employees.Count} employees to the application.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The seeder method has an error.");
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
