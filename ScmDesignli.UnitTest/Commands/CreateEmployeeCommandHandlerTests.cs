using FluentAssertions;
using Moq;
using ScmDesignli.Application.Commands.Employee.CreateEmployee;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Domain.Entities;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.UnitTest.Commands
{
    /// <summary>
    /// Unit tests for CreateEmployeeCommandHandler
    /// </summary>
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly CreateEmployeeCommandHandler _handler;

        public CreateEmployeeCommandHandlerTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new CreateEmployeeCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateEmployee()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                Name = "Juan",
                LastName = "Fernandez",
                Birthday = new DateTime(1990, 1, 1),
                Email = "juan.fernandez@test.com",
                Department = Department.IT
            };

            var expectedEmployee = new Employee
            {
                Id = 1,
                Name = command.Name,
                LastName = command.LastName,
                Birthday = command.Birthday,
                Email = command.Email,
                Department = command.Department
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Employee>()))
                .ReturnsAsync(expectedEmployee);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            result.LastName.Should().Be(command.LastName);
            result.Email.Should().Be(command.Email);
            result.Department.Should().Be(command.Department);
            
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldSetCreatedAtTimestamp()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                Name = "Maria",
                LastName = "Espinoza",
                Birthday = new DateTime(1985, 5, 15),
                Email = "maria.espinoza@test.com",
                Department = Department.Finance
            };

            Employee capturedEmployee = null!;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Employee>()))
                .Callback<Employee>(e => capturedEmployee = e)
                .ReturnsAsync((Employee e) => e);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            capturedEmployee.Should().NotBeNull();
            capturedEmployee.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            capturedEmployee.IsDeleted.Should().BeFalse();
        }
    }
}
