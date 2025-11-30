using MediatR;

namespace ScmDesignli.Application.Queries.Employee.GetEmployeePaginated
{
    /// <summary>
    /// Query to get paginated employees
    /// </summary>
    public class GetEmployeePaginatedQuery : IRequest<PaginatedEmployeesResponse>
    {
        /// <summary>
        /// Page number (starts from 1)
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size (number of items per page)
        /// </summary>
        /// <example>10</example>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Search term to filter employees by name, last name or email
        /// </summary>
        /// <example>John</example>
        public string? SearchTerm { get; set; }
    }

    /// <summary>
    /// Response for paginated employees
    /// </summary>
    public class PaginatedEmployeesResponse
    {
        /// <summary>
        /// List of employees for the current page
        /// </summary>
        public IEnumerable<Domain.Entities.Employee> Employees { get; set; } = new List<Domain.Entities.Employee>();

        /// <summary>
        /// Current page number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total count of employees
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Indicates if there is a previous page
        /// </summary>
        public bool HasPreviousPage => Page > 1;

        /// <summary>
        /// Indicates if there is a next page
        /// </summary>
        public bool HasNextPage => Page < TotalPages;
    }
}
