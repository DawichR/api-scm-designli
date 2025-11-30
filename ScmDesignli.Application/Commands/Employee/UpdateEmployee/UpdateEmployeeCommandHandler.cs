using MediatR;
using ScmDesignli.Application.Interfaces.Repositories;

namespace ScmDesignli.Application.Commands.Employee.UpdateEmployee
{
    /// <summary>
    /// Handler for updating an existing employee
    /// </summary>
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Domain.Entities.Employee?>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Domain.Entities.Employee?> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Domain.Entities.Employee
            {
                Name = request.Name,
                LastName = request.LastName,
                Birthday = request.Birthday,
                Email = request.Email,
                Department = request.Department
            };

            var updatedEmployee = await _employeeRepository.UpdateAsync(request.Id, employee);
            return updatedEmployee;
        }
    }
}
