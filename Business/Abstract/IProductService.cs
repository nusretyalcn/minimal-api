using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IProductService
{
    IDataResult<List<Product>> GetAll();
    IDataResult<List<Product>> GetByCategoryId(int id);
    IDataResult<List<ProductDetailDto>> GetProductDetails();
    IResult Add(Product product);
    IResult Update(Product product);
    IResult Delete(Product product);
    IDataResult<Product> GetById(int id);
    IDataResult<Product> GetByName(string name);
    
}