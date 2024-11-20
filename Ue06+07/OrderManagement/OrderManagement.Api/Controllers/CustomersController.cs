using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Dtos;
using OrderManagement.Api.Mappers;
using OrderManagement.Domain;
using OrderManagement.Logic;

namespace OrderManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(IOrderManagementLogic logic) : ControllerBase
{
    private readonly IOrderManagementLogic _logic = logic ?? throw new ArgumentNullException(nameof(logic));

    [HttpGet]
    public async Task<IEnumerable<CustomerDto>> GetCustomers()
    {
        IEnumerable<Domain.Customer> customer = await logic.GetCustomersAsync();
        return customer.Select(c => c.ToCustomerDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCustomerById(Guid id)
    {
        Domain.Customer customer = await logic.GetCustomerAsync(id);
        if(customer == null)
        {
            return NotFound();
        }
        return Ok(customer.ToCustomerDto());
    }
}
