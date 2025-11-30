using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Application.Queries.Employee.GetAllEmployees;
using ScmDesignli.Domain.Entities;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.UnitTest.Queries
{
    /// <summary>
    /// Unit tests for GetAllEmployeesQueryHandler
    /// </summary>
    public class GetAllEmployeesQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly GetAllEmployeesQueryHandler _handler;

        public GetAllEmployeesQueryHandlerTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new GetAllEmployeesQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Juan",
                    LastName = "Quilmes",
                    Email = "juan@test.com",
                    Birthday = new DateTime(1990, 1, 1),
                    Department = Department.IT
                },
                new Employee
                {
                    Id = 2,
                    Name = "Sophia",
                    LastName = "Melbourne",
                    Email = "sophia@test.com",
                    Birthday = new DateTime(1985, 5, 15),
                    Department = Department.Finance
                }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);

            var query = new GetAllEmployeesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(employees);
            
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenNoEmployees_ShouldReturnEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Employee>());

            var query = new GetAllEmployeesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
