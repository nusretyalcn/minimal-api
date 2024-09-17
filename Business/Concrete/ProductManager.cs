using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class ProductManager:IProductService
{
    private readonly IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }
    
    public IDataResult<List<Product>> GetAll()
    {
        if (DateTime.Now.Hour==22)
        {
            return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        }
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
    }

    public IDataResult<List<Product>> GetByCategoryId(int id)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId == id),Messages.ProductsListed);
    }

    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(Product product)
    {
        //ValidationTool.Validate(new ProductValidator(),product);
        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    public IResult Update(Product product)
    {
        ValidationTool.Validate(new ProductValidator(),product);
        _productDal.Update(product);
        return new SuccessResult(Messages.ProductUpdated);
    }

    public IResult Delete(Product product)
    {
        ValidationTool.Validate(new ProductValidator(),product);
        _productDal.Delete(product);
        return new SuccessResult(Messages.ProductDeleted);
    }

    public IDataResult<Product> GetById(int id)
    {
        return new SuccessDataResult<Product>(_productDal.Get(p=>p.Id == id));
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetail());
    }
}