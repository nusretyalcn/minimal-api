using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract;

public interface IProductImageService
{
    IResult Add(IFormFile file, ProductImage productImage);
    IResult Delete(ProductImage productImage);
    IDataResult<List<ProductImage>> GetAll();
    IResult UpdateImage(IFormFile file, ProductImage productImage);
    IDataResult<List<ProductImage>> GetByProductId(int id);
    IDataResult<ProductImage> GetByImageId(int id);
}