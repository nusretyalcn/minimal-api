using Entities.Concrete;

namespace Business.Abstract;

public interface IProductService
{
    List<Product> GetAll();
    List<Product> GetByCategoryId(int id);
    void Add(Product product);
}