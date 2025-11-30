using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScmDesignli.Application.Queries.Employee.GetEmployeePaginated;
using ScmDesignli.Application.Queries.Employee.GetEmployeeById;
using ScmDesignli.Application.Commands.Employee.CreateEmployee;
using ScmDesignli.Application.Commands.Employee.UpdateEmployee;
using ScmDesignli.Application.Commands.Employee.DeleteEmployee;

namespace ScmDesignli.Api.Controllers.Employee
{
    /// <summary>
    /// Employee controller for performing employee action.
    /// </summary>
    public class EmployeesController : BaseController
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get list of employees
        /// </summary>
        /// <param name="query">paginated parameters</param>
        /// <returns>List of employee</returns>
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] GetEmployeePaginatedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get employee by Id
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>Get employee</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <param name="command">Employee creation data</param>
        /// <returns>Created employee</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="command">Employee update data</param>
        /// <returns>Updated employee</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
         
            return Ok(result);
        }

        /// <summary>
        /// Delete an employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
          

            return NoContent();
        }

    }
}
