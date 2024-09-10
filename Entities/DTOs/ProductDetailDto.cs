using Core.Entities;

namespace Entities.DTOs;

public class ProductDetailDto:IDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public int UnitsInStock { get; set; }
}