using FluentValidation;

namespace ScmDesignli.Application.Queries.Employee.GetEmployeePaginated
{
    /// <summary>
    /// Validator for GetEmployeePaginatedQuery
    /// </summary>
    public class GetEmployeePaginatedQueryValidator : AbstractValidator<GetEmployeePaginatedQuery>
    {
        public GetEmployeePaginatedQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100");

            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage("Search term must not exceed 100 characters")
                .When(x => !string.IsNullOrEmpty(x.SearchTerm));
        }
    }
}
