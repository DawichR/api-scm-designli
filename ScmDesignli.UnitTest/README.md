# ScmDesignli Unit Tests

This project contains unit tests for the Employee Management API, covering all Commands and Queries.

## 📦 Test Framework

- **xUnit** - Testing framework
- **Moq** - Mocking library for repository dependencies
- **FluentAssertions** - Fluent assertion library for readable test assertions

## 🧪 Test Coverage

### Commands (3 test classes)

#### CreateEmployeeCommandHandlerTests
Tests for creating new employees:
- ✅ `Handle_ValidCommand_ShouldCreateEmployee` - Verifies employee creation with valid data
- ✅ `Handle_ValidCommand_ShouldSetCreatedAtTimestamp` - Verifies timestamp and deleted flag are set correctly

#### UpdateEmployeeCommandHandlerTests
Tests for updating existing employees:
- ✅ `Handle_ValidCommand_ShouldUpdateEmployee` - Verifies employee update with valid data
- ✅ `Handle_NonExistentEmployee_ShouldReturnNull` - Verifies null return when employee doesn't exist

#### DeleteEmployeeCommandHandlerTests
Tests for deleting employees:
- ✅ `Handle_ExistingEmployee_ShouldReturnTrue` - Verifies successful deletion
- ✅ `Handle_NonExistentEmployee_ShouldReturnFalse` - Verifies false return when employee doesn't exist

### Queries (3 test classes)

#### GetAllEmployeesQueryHandlerTests
Tests for retrieving all employees:
- ✅ `Handle_ShouldReturnAllEmployees` - Verifies all employees are returned
- ✅ `Handle_WhenNoEmployees_ShouldReturnEmptyList` - Verifies empty list when no employees exist

#### GetEmployeeByIdQueryHandlerTests
Tests for retrieving a single employee:
- ✅ `Handle_ExistingEmployee_ShouldReturnEmployee` - Verifies employee is returned when found
- ✅ `Handle_NonExistentEmployee_ShouldReturnNull` - Verifies null return when employee doesn't exist

#### GetEmployeePaginatedQueryHandlerTests
Tests for paginated employee retrieval:
- ✅ `Handle_ValidQuery_ShouldReturnPaginatedResults` - Verifies pagination works correctly
- ✅ `Handle_WithSearchTerm_ShouldFilterResults` - Verifies search filtering works
- ✅ `Handle_EmptyRepository_ShouldReturnEmptyPaginatedResults` - Verifies empty results when no employees

## 🏃 Running Tests

### Run all tests
```bash
dotnet test
```

### Run with detailed output
```bash
dotnet test --verbosity normal
```

### Run specific test class
```bash
dotnet test --filter "FullyQualifiedName~CreateEmployeeCommandHandlerTests"
```

### Run with coverage (requires coverlet)
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## 📊 Test Results

All 13 tests pass successfully:
- **Total Tests**: 13
- **Passed**: 13 ✅
- **Failed**: 0
- **Skipped**: 0

## 🎯 Test Patterns

All tests follow the **AAA pattern**:
1. **Arrange** - Setup test data and mock dependencies
2. **Act** - Execute the handler method
3. **Assert** - Verify the results using FluentAssertions

### Example Test Structure
```csharp
[Fact]
public async Task Handle_ValidCommand_ShouldCreateEmployee()
{
    // Arrange - Setup mocks and test data
    var command = new CreateEmployeeCommand { /* ... */ };
    _mockRepository.Setup(r => r.AddAsync(It.IsAny<Employee>()))
        .ReturnsAsync(expectedEmployee);

    // Act - Execute the handler
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert - Verify results
    result.Should().NotBeNull();
    result.Name.Should().Be(command.Name);
    _mockRepository.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
}
```

## 🔍 Mocking Strategy

- **IEmployeeRepository** is mocked in all tests to isolate business logic from data access
- **ILogger** is mocked where needed (GetAllEmployeesQueryHandler)
- Mocks are configured using **Moq** with setup and verification
- Repository behavior is simulated for both success and failure scenarios

## 📝 Notes

- Tests are **unit tests** - they test individual handlers in isolation
- External dependencies (repositories, loggers) are **mocked**
- Tests cover both **happy path** and **edge cases** (null returns, empty collections)
- All tests are **fast** and **deterministic** - no external dependencies or I/O operations
