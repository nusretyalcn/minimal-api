using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IProductService
{
    List<Product> GetAll();
    List<Product> GetByCategoryId(int id);
    void Add(Product product);
    List<ProductDetailDto> GetProductDetails();
}