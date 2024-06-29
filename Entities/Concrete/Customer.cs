using Core.Entities;

namespace Entities.Concrete;

public class Customer:IEntity
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }
    public int RegionId { get; set; }
    public int CountryId { get; set; }
    public string? PhoneNumber { get; set; }
}