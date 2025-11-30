using FluentValidation;

namespace ScmDesignli.Application.Commands.Employee.DeleteEmployee
{
    /// <summary>
    /// Validator for DeleteEmployeeCommand
    /// </summary>
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Employee Id must be greater than 0");
        }
    }
}
