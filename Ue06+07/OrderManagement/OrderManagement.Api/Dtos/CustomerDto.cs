using OrderManagement.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderManagement.Api.Dtos;

public record CustomerDto
{
    //will be taken in count by the validation
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required int ZipCode { get; set; }
    public required string City { get; set; }
    public required Rating Rating { get; set; }
    public decimal TotalRevenue { get; set; }
}