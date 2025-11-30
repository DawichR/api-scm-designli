using MediatR;

namespace ScmDesignli.Application.Queries.Employee.GetAllEmployees
{
    /// <summary>
    /// Query to get all employees
    /// </summary>
    public class GetAllEmployeesQuery : IRequest<IEnumerable<Domain.Entities.Employee>>
    {
    }
}
