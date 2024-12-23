using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class ProductManager:IProductService
{
    private readonly IProductDal _productDal;
    private readonly ICategoryService _categoryService;

    public ProductManager(IProductDal productDal, ICategoryService categoryService)
    {
        _productDal = productDal;
        _categoryService = categoryService;
    }
    
    [PerformanceAspect(5)]
    [CacheAspect]
    public IDataResult<List<Product>> GetAll()
    {
        if (DateTime.Now.Hour==22)
        {
            return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        }
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
    }

    [PerformanceAspect(5)]
    [CacheAspect]
    public IDataResult<List<Product>> GetByCategoryId(int id)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId == id),Messages.ProductsListed);
    }
    
    [SecuredOperation("product.add,admin")]
    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    [TransactionScopeAspect]
    public IResult Add(Product product)
    {
        IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
            CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceded());
        if (result != null)
        {
            return result;
        }
            
        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
        
    }

    [SecuredOperation("product.add,admin")]
    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    [TransactionScopeAspect]
    public IResult Update(Product product)
    {
        _productDal.Update(product);
        return new SuccessResult(Messages.ProductUpdated);
    }

    [SecuredOperation("product.add,admin")]
    [CacheRemoveAspect("IProductService.Get")]
    [TransactionScopeAspect]
    public IResult Delete(Product product)
    {
        ValidationTool.Validate(new ProductValidator(),product);
        _productDal.Delete(product);
        return new SuccessResult(Messages.ProductDeleted);
    }

    [CacheAspect]
    public IDataResult<Product> GetById(int id)
    {
        return new SuccessDataResult<Product>(_productDal.Get(p=>p.Id == id));
    }

    [CacheAspect]
    public IDataResult<Product> GetByName(string name)
    {
        return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductName == name));
    }

    [CacheAspect]
    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetail());
    }

    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        var result = _productDal.GetAll(p=>p.CategoryId==categoryId).Count;
        if (result>=10)
        {
            return new ErrorResult(Messages.ProductCountOfCategoryError);
        }

        return new SuccessResult();
    }
    private IResult CheckIfProductNameExists(string productName)
    {
        var result = _productDal.GetAll(p=>p.ProductName==productName).Any();
        if (result)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        return new SuccessResult();
    }

    private IResult CheckIfCategoryLimitExceded()
    {
        var result = _categoryService.GetAll();
        if (result.Data.Count>15)
        {
            return new ErrorResult(Messages.CategoryLimitExceded);
        }

        return new SuccessResult();
    }
}