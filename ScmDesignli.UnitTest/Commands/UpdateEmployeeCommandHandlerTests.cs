using FluentAssertions;
using Moq;
using ScmDesignli.Application.Commands.Employee.UpdateEmployee;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Domain.Entities;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.UnitTest.Commands
{
    /// <summary>
    /// Unit tests for UpdateEmployeeCommandHandler
    /// </summary>
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly UpdateEmployeeCommandHandler _handler;

        public UpdateEmployeeCommandHandlerTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new UpdateEmployeeCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldUpdateEmployee()
        {
            // Arrange
            var command = new UpdateEmployeeCommand
            {
                Id = 1,
                Name = "Juan Estrella",
                LastName = "Juan Fernandez",
                Birthday = new DateTime(1990, 1, 1),
                Email = "juan.estrella@test.com",
                Department = Department.IT
            };

            var updatedEmployee = new Employee
            {
                Id = command.Id,
                Name = command.Name,
                LastName = command.LastName,
                Birthday = command.Birthday,
                Email = command.Email,
                Department = command.Department
            };

            _mockRepository.Setup(r => r.UpdateAsync(command.Id, It.IsAny<Employee>()))
                .ReturnsAsync(updatedEmployee);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be(command.Name);
            result.LastName.Should().Be(command.LastName);
            result.Email.Should().Be(command.Email);
            
            _mockRepository.Verify(r => r.UpdateAsync(command.Id, It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistentEmployee_ShouldReturnNull()
        {
            // Arrange
            var command = new UpdateEmployeeCommand
            {
                Id = 999,
                Name = "José",
                LastName = "Gutierrez",
                Birthday = new DateTime(1990, 1, 1),
                Email = "usertest@test.com",
                Department = Department.IT
            };

            _mockRepository.Setup(r => r.UpdateAsync(command.Id, It.IsAny<Employee>()))
                .ReturnsAsync((Employee?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            _mockRepository.Verify(r => r.UpdateAsync(command.Id, It.IsAny<Employee>()), Times.Once);
        }
    }
}
