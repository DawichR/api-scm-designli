using FluentAssertions;
using Moq;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Application.Queries.Employee.GetEmployeeById;
using ScmDesignli.Domain.Entities;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.UnitTest.Queries
{
    /// <summary>
    /// Unit tests for GetEmployeeByIdQueryHandler
    /// </summary>
    public class GetEmployeeByIdQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly GetEmployeeByIdQueryHandler _handler;

        public GetEmployeeByIdQueryHandlerTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new GetEmployeeByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ExistingEmployee_ShouldReturnEmployee()
        {
            // Arrange
            var employeeId = 1;
            var employee = new Employee
            {
                Id = employeeId,
                Name = "Juan",
                LastName = "Sarmiento",
                Email = "juan@test.com",
                Birthday = new DateTime(1990, 1, 1),
                Department = Department.IT
            };

            _mockRepository.Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync(employee);

            var query = new GetEmployeeByIdQuery(employeeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(employeeId);
            result.Name.Should().Be("Juan");
            result.LastName.Should().Be("Sarmiento");
            result.Email.Should().Be("juan@test.com");
            
            _mockRepository.Verify(r => r.GetByIdAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistentEmployee_ShouldReturnNull()
        {
            // Arrange
            var employeeId = 999;

            _mockRepository.Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync((Employee?)null);

            var query = new GetEmployeeByIdQuery(employeeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            
            _mockRepository.Verify(r => r.GetByIdAsync(employeeId), Times.Once);
        }
    }
}
