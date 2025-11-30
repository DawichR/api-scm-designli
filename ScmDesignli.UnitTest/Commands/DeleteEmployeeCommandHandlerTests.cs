using FluentAssertions;
using Moq;
using ScmDesignli.Application.Commands.Employee.DeleteEmployee;
using ScmDesignli.Application.Interfaces.Repositories;

namespace ScmDesignli.UnitTest.Commands
{
    /// <summary>
    /// Unit tests for DeleteEmployeeCommandHandler
    /// </summary>
    public class DeleteEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly DeleteEmployeeCommandHandler _handler;

        public DeleteEmployeeCommandHandlerTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new DeleteEmployeeCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ExistingEmployee_ShouldReturnTrue()
        {
            // Arrange
            var command = new DeleteEmployeeCommand { Id = 1 };

            _mockRepository.Setup(r => r.DeleteAsync(command.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockRepository.Verify(r => r.DeleteAsync(command.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistentEmployee_ShouldReturnFalse()
        {
            // Arrange
            var command = new DeleteEmployeeCommand { Id = 999 };

            _mockRepository.Setup(r => r.DeleteAsync(command.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _mockRepository.Verify(r => r.DeleteAsync(command.Id), Times.Once);
        }
    }
}
