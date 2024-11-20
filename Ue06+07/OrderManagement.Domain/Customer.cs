namespace OrderManagement.Domain;

public class Customer(Guid id, string name, int zipCode, string city, Rating rating)
{
  public Guid Id { get; set; } = id;

  public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));

  public int ZipCode { get; set; } = zipCode;

  public string City { get; set; } = city ?? throw new ArgumentNullException(nameof(city));

  public Rating Rating { get; set; } = rating;

  public decimal TotalRevenue { get; set; }
}
