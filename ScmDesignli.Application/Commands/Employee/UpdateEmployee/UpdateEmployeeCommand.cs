using MediatR;
using ScmDesignli.Domain.Enums;
using System.Text.Json.Serialization;

namespace ScmDesignli.Application.Commands.Employee.UpdateEmployee
{
    /// <summary>
    /// Command to update an existing employee
    /// </summary>
    public class UpdateEmployeeCommand : IRequest<Domain.Entities.Employee?>
    {
        /// <summary>
        /// Employee ID (from route parameter)
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Name of the employee
        /// </summary>
        /// <example>Juan</example>
        public required string Name { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        /// <example>Perez</example>
        public required string LastName { get; set; }

        /// <summary>
        /// Birthday of the employee
        /// </summary>
        /// <example>1990-01-15</example>
        public required DateTime Birthday { get; set; }

        /// <summary>
        /// Email of the employee
        /// </summary>
        /// <example>juan.perez@designli.co</example>
        public required string Email { get; set; }

        /// <summary>
        /// Department of the employee
        /// </summary>
        /// <example>IT</example>
        public required Department Department { get; set; }
    }
}
