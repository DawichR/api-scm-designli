using MediatR;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.Application.Commands.Employee.CreateEmployee
{
    /// <summary>
    /// Command to create a new employee
    /// </summary>
    public class CreateEmployeeCommand : IRequest<Domain.Entities.Employee>
    {
        /// <summary>
        /// Name of the employee
        /// </summary>
        /// <example>John</example>
        public required string Name { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        /// <example>Doe</example>
        public required string LastName { get; set; }

        /// <summary>
        /// Birthday of the employee
        /// </summary>
        /// <example>1990-01-15</example>
        public required DateTime Birthday { get; set; }

        /// <summary>
        /// Email of the employee
        /// </summary>
        /// <example>john.doe@designli.co</example>
        public required string Email { get; set; }

        /// <summary>
        /// Department of the employee
        /// </summary>
        /// <example>IT</example>
        public required Department Department { get; set; }
    }
}
