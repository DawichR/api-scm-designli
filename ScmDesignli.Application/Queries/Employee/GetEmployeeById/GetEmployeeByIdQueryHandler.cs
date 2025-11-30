using MediatR;
using ScmDesignli.Application.Interfaces.Repositories;

namespace ScmDesignli.Application.Queries.Employee.GetEmployeeById
{
    /// <summary>
    /// Handler for getting an employee by ID
    /// </summary>
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Domain.Entities.Employee?>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Domain.Entities.Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            return employee;
        }
    }
}
