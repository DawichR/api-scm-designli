using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScmDesignli.Application.Queries.Employee.GetEmployeePaginated;
using ScmDesignli.Application.Queries.Employee.GetEmployeeById;
using ScmDesignli.Application.Queries.Employee.GetAllEmployees;
using ScmDesignli.Application.Commands.Employee.CreateEmployee;
using ScmDesignli.Application.Commands.Employee.UpdateEmployee;
using ScmDesignli.Application.Commands.Employee.DeleteEmployee;

namespace ScmDesignli.Api.Controllers.Employee
{
    /// <summary>
    /// Employee controller for performing employee actions
    /// </summary>
    public class EmployeesController : BaseController
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>List of all employees</returns>
        /// <response code="200">Returns the list of employees</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get paginated list of employees
        /// </summary>
        /// <param name="query">Pagination parameters</param>
        /// <returns>Paginated list of employees</returns>
        /// <response code="200">Returns the paginated list of employees</response>
        /// <response code="400">If the request parameters are invalid</response>
        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaginated([FromQuery] GetEmployeePaginatedQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get employee by ID
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee details</returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="404">If the employee is not found</response>
        /// <response code="400">If the ID is invalid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if (result == null)
            {
                return NotFound(new { message = $"Employee with ID {id} not found" });
            }

            return Ok(result);
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <param name="command">Employee creation data</param>
        /// <returns>Created employee</returns>
        /// <response code="201">Returns the newly created employee</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="command">Employee update data</param>
        /// <returns>Updated employee</returns>
        /// <response code="200">Returns the updated employee</response>
        /// <response code="404">If the employee is not found</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound(new { message = $"Employee with ID {id} not found" });
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete an employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the employee was successfully deleted</response>
        /// <response code="404">If the employee is not found</response>
        /// <response code="400">If the ID is invalid</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id });

            if (!result)
            {
                return NotFound(new { message = $"Employee with ID {id} not found" });
            }

            return NoContent();
        }
    }
}
