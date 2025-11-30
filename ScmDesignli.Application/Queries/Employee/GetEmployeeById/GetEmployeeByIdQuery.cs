using MediatR;

namespace ScmDesignli.Application.Queries.Employee.GetEmployeeById
{
    /// <summary>
    /// Query to get an employee by ID
    /// </summary>
    public class GetEmployeeByIdQuery : IRequest<Domain.Entities.Employee?>
    {
        /// <summary>
        /// Employee ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
