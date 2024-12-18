using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CategoryManager:ICategoryService
{
    private readonly ICategoryDal _categoryDal;

    public CategoryManager(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }
    
    [CacheAspect]
    public IDataResult<List<Category>> GetAll()
    {
        return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(),Messages.CategoriesListed);
    }

    [CacheAspect]
    public IDataResult<Category> GetById(int id)
    {
        return new SuccessDataResult<Category>(_categoryDal.Get(p=>p.Id==id));
    }

    [SecuredOperation("admin")]
    [ValidationAspect(typeof(CategoryValidator))]
    [CacheRemoveAspect("ICategoryService.Get")]
    [TransactionScopeAspect]
    public IResult Add(Category category)
    {
        _categoryDal.Add(category);
        return new SuccessResult(Messages.CategoryAdded);
    }

    [SecuredOperation("admin")]
    [ValidationAspect(typeof(CategoryValidator))]
    [CacheRemoveAspect("ICategoryService.Get")]
    [TransactionScopeAspect]
    public IResult Update(Category category)
    {
        _categoryDal.Update(category);
        return new SuccessResult(Messages.CategoryUpdated);
    }

    [SecuredOperation("admin")]
    [CacheRemoveAspect("ICategoryService.Get")]
    [TransactionScopeAspect]
    public IResult Delete(Category category)
    {
        _categoryDal.Delete(category);
        return new SuccessResult(Messages.CategoryDeleted);
    }
}