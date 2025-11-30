using FluentValidation;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.Application.Commands.Employee.UpdateEmployee
{
    /// <summary>
    /// Validator for UpdateEmployeeCommand
    /// </summary>
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            var namePattern = "^[a-zA-Z ]+$";

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Employee ID must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .Matches(namePattern).WithMessage("Name must contain only letters and spaces");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters")
                .Matches(namePattern).WithMessage("Last name must contain only letters and spaces");

            RuleFor(x => x.Birthday)
                .NotEmpty().WithMessage("Birthday is required")
                .LessThan(DateTime.Now).WithMessage("Birthday must be in the past")
                .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday must be within the last 100 years");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid email address")
                .MaximumLength(200).WithMessage("Email must not exceed 200 characters")
                .MustAsync(async (command, email, cancellation) =>
                {
                    return !await _employeeRepository.EmailExistsAsync(email, command.Id);
                })
                .WithMessage("Email is already registered by another employee");

            RuleFor(x => x.Department)
                .IsInEnum().WithMessage($"Department must be a valid department value. Valid values: {GetValidDepartments()}");
        }

        private static string GetValidDepartments()
        {
            var departments = Enum.GetValues(typeof(Department))
                .Cast<Department>()
                .Select(d => $"{(int)d} = {d}")
                .ToList();

            return string.Join(", ", departments);
        }
    }
}
