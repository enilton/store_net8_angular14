using Enilton.Application.Commands.Customer;
using Enilton.Application.DTOs.Customer;
using Enilton.Application.Queries.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            var command = new CreateCustomerCommand
            {
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                Phone = customerDTO.Phone
            };

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetCustomerById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Retorna um cliente pelo ID
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var query = new GetCustomerByIdQuery { Id = id };
            var customer = await _mediator.Send(query);

            if (customer == null)
            {
                return NotFound(new { message = "Customer not found." });
            }

            return Ok(customer);
        }

        /// <summary>
        /// Retorna todos os clientes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var query = new GetAllCustomersQuery();
            var customers = await _mediator.Send(query);

            return Ok(customers);
        }

        /// <summary>
        /// Atualiza um cliente existente
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerDTO customerDTO)
        {
            var command = new UpdateCustomerCommand
            {
                Id = id,
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                Phone = customerDTO.Phone
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Remove um cliente pelo ID
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var command = new DeleteCustomerCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
