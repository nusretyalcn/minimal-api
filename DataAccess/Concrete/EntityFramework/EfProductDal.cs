using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfProductDal:EfEntityRepositoryBase<Product,EfDbContext>,IProductDal
{
    public List<ProductDetailDto> GetProductDetail()
    {
        using (EfDbContext context = new EfDbContext())
        {
            var result = from p in context.Products
                join c in context.Categories on p.CategoryId equals c.Id
                select new ProductDetailDto
                {
                    Id = p.Id,ProductName = p.ProductName,
                    CategoryName = c.CategoryName,UnitsInStock = p.UnitsInStock
                };
            return result.ToList();
        }
    }
}