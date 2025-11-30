using FluentValidation;

namespace ScmDesignli.Application.Queries.Employee.GetEmployeeById
{
    /// <summary>
    /// Validator for GetEmployeeByIdQuery
    /// </summary>
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Employee ID must be greater than 0");
        }
    }
}
