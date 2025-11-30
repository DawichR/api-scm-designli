using FluentAssertions;
using Moq;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Application.Queries.Employee.GetEmployeePaginated;
using ScmDesignli.Domain.Entities;
using ScmDesignli.Domain.Enums;

namespace ScmDesignli.UnitTest.Queries
{
    /// <summary>
    /// Unit tests for GetEmployeePaginatedQueryHandler
    /// </summary>
    public class GetEmployeePaginatedQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly GetEmployeePaginatedQueryHandler _handler;

        public GetEmployeePaginatedQueryHandlerTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new GetEmployeePaginatedQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnPaginatedResults()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Joseph",
                    LastName = "Does",
                    Email = "joseph@test.com",
                    Birthday = new DateTime(1990, 1, 1),
                    Department = Department.IT
                },
                new Employee
                {
                    Id = 2,
                    Name = "Sophia",
                    LastName = "Sampson",
                    Email = "sophia@test.com",
                    Birthday = new DateTime(1985, 5, 15),
                    Department = Department.Finance
                },
                new Employee
                {
                    Id = 3,
                    Name = "Rafael",
                    LastName = "Rammstein",
                    Email = "rafael@test.com",
                    Birthday = new DateTime(1988, 3, 20),
                    Department = Department.Sales
                }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);

            var query = new GetEmployeePaginatedQuery
            {
                Page = 1,
                PageSize = 2
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Employees.Should().HaveCount(2);
            result.TotalCount.Should().Be(3);
            result.Page.Should().Be(1);
            result.PageSize.Should().Be(2);
            result.TotalPages.Should().Be(2);
            
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WithSearchTerm_ShouldFilterResults()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Joseph",
                    LastName = "Does",
                    Email = "joseph@test.com",
                    Birthday = new DateTime(1990, 1, 1),
                    Department = Department.IT
                },
                new Employee
                {
                    Id = 2,
                    Name = "Sophia",
                    LastName = "Sampson",
                    Email = "sophia@test.com",
                    Birthday = new DateTime(1985, 5, 15),
                    Department = Department.Finance
                }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);

            var query = new GetEmployeePaginatedQuery
            {
                Page = 1,
                PageSize = 10,
                SearchTerm = "Joseph"
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Employees.Should().HaveCount(1);
            result.Employees.First().Name.Should().Be("Joseph");
            result.TotalCount.Should().Be(1);
            
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyRepository_ShouldReturnEmptyPaginatedResults()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Employee>());

            var query = new GetEmployeePaginatedQuery
            {
                Page = 1,
                PageSize = 10
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Employees.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            result.TotalPages.Should().Be(0);
            
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
