using MediatR;
using ScmDesignli.Application.Interfaces.Repositories;

namespace ScmDesignli.Application.Queries.Employee.GetEmployeePaginated
{
    /// <summary>
    /// Handler for getting paginated employees
    /// </summary>
    public class GetEmployeePaginatedQueryHandler : IRequestHandler<GetEmployeePaginatedQuery, PaginatedEmployeesResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeePaginatedQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<PaginatedEmployeesResponse> Handle(GetEmployeePaginatedQuery request, CancellationToken cancellationToken)
        {
            var allEmployees = await _employeeRepository.GetAllAsync();
            var filteredEmployees = allEmployees.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                filteredEmployees = filteredEmployees.Where(e =>
                    e.Name.ToLower().Contains(searchTerm) ||
                    e.LastName.ToLower().Contains(searchTerm) ||
                    e.Email.ToLower().Contains(searchTerm));
            }

            var totalCount = filteredEmployees.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            // Apply pagination
            var paginatedEmployees = filteredEmployees
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new PaginatedEmployeesResponse
            {
                Employees = paginatedEmployees,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
    }
}
