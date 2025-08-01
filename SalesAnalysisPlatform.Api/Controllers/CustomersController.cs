using Microsoft.AspNetCore.Mvc;
using SalesAnalysisPlatform.Application.Interfaces;
using SalesAnalysisPlatform.Domain.DTOs;
using SalesAnalysisPlatform.Application.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAnalysisPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomer()
        {
            var customer = await _customerService.GetAllCustomersAsync();
            var dtos = customer.Select(CustomerMapper.ToDTO);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(CustomerMapper.ToDTO(customer));
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerDTO dto)
        {
            var customer = CustomerMapper.ToEntity(dto);
            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, CustomerMapper.ToDTO(customer));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody] CustomerDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var customer = CustomerMapper.ToEntity(dto);
            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
