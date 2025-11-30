using MediatR;
using Microsoft.Extensions.Logging;
using ScmDesignli.Application.Interfaces.Repositories;

namespace ScmDesignli.Application.Queries.Employee.GetAllEmployees
{
    /// <summary>
    /// Handler for getting all employees
    /// </summary>
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Domain.Entities.Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();
            var count = await _employeeRepository.CountAsync();
            return employees;
        }
    }
}
