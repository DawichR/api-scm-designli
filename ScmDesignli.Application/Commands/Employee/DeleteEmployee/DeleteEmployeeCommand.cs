using MediatR;

namespace ScmDesignli.Application.Commands.Employee.DeleteEmployee
{
    /// <summary>
    /// Command to delete an employee
    /// </summary>
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        /// <summary>
        /// Employee ID to delete
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
