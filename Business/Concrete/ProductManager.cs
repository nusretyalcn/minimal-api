using System.Text.RegularExpressions;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class ProductManager:IProductService
{
    private IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }
    
    public List<Product> GetAll()
    {
        return _productDal.GetAll();
    }

    public List<Product> GetByCategoryId(int id)
    {
        return _productDal.GetAll(p=>p.CategoryId == id);
    }

    public void Add(Product product)
    {
        _productDal.Add(product);
    }
}