using OrderManagement.Api.Dtos;
using OrderManagement.Domain;

namespace OrderManagement.Api.Mappers;

public static class CustomerMapper
{
    public static CustomerDto ToCustomerDto(this Customer customer) =>
        new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            ZipCode = customer.ZipCode,
            City = customer.City,
            Rating = customer.Rating,
            TotalRevenue = customer.TotalRevenue
        };
}
