using FluentValidation;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.Application.Commands.Employee.CreateEmployee
{
    /// <summary>
    /// Validator for CreateEmployeeCommand
    /// </summary>
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            var namePattern = "^[a-zA-Z ]+$";
            _employeeRepository = employeeRepository;

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
                .MustAsync(async (email, cancellation) =>
                {
                    return !await _employeeRepository.EmailExistsAsync(email);
                })
                .WithMessage("Email is already registered in the system");

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
