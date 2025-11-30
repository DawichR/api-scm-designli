using MediatR;
using ScmDesignli.Application.Interfaces.Repositories;

namespace ScmDesignli.Application.Commands.Employee.DeleteEmployee
{
    /// <summary>
    /// Handler for deleting an employee
    /// </summary>
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.DeleteAsync(request.Id);
            return result;
        }
    }
}
