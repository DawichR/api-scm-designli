using MediatR;
using ScmDesignli.Application.Interfaces.Repositories;

namespace ScmDesignli.Application.Commands.Employee.CreateEmployee
{
    /// <summary>
    /// Handler for creating a new employee
    /// </summary>
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Domain.Entities.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Domain.Entities.Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Domain.Entities.Employee
            {
                Name = request.Name,
                LastName = request.LastName,
                Birthday = request.Birthday,
                Email = request.Email,
                Department = request.Department
            };

            var createdEmployee = await _employeeRepository.AddAsync(employee);
            return createdEmployee;
        }
    }
}
