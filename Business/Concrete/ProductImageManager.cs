using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete;

public class ProductImageManager: IProductImageService
{
    public IProductImageDal _productImageDal;
    public IFileHelper _fileHelper;

    public ProductImageManager(IProductImageDal productImageDal, IFileHelper fileHelper)
    {
        _productImageDal = productImageDal;
        _fileHelper = fileHelper;
    }

    public IResult Add(IFormFile file, ProductImage productImage)
    {
        productImage.ProductId = productImage.ProductId;
        productImage.ImagePath = _fileHelper.Upload(file, PathConstants.ImagesPath);
        productImage.Date = DateTime.Now;
        _productImageDal.Add(productImage);
        return new SuccessResult("Resim başarıyla yüklendi");
    }

    public IDataResult<List<ProductImage>> GetAll()
    {
        return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll());
    }

    public IResult UpdateProductImage(IFormFile file, ProductImage productImage)
    {
        var currentCarImage = _productImageDal.Get(x => x.ProductId == productImage.ProductId);//&& x.Id == productImage.Id);
        string newPath = _fileHelper.Update(file, currentCarImage.ImagePath, PathConstants.ImagesPath);
        productImage.ImagePath = newPath;
        productImage.Date = DateTime.Now;

        _productImageDal.Update(productImage);
        return new SuccessResult();
    }

    public IDataResult<List<ProductImage>> GetByProductId(int id)
    {
        IResult IsDefaultImage = BusinessRules.Run((IResult)CheckProductImage(id));

        if (IsDefaultImage == null) //null değilse Car image tablosunda bu araca ait bir kayıt yok demektir. 
        {
            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll(c => c.ProductId == id));
        }
        else
            
            return new ErrorDataResult<List<ProductImage>>(CheckProductImage(id).Data);
    }
    
    private IDataResult<List<ProductImage>> CheckProductImage(int productId)
    {
        int count = _productImageDal.GetAll(x => x.ProductId == productId).Count();
        if (count > 0)
        {
            return new SuccessDataResult<List<ProductImage>>();
        }
           
        return new ErrorDataResult<List<ProductImage>>(new List<ProductImage>() 
        { 
            new ProductImage 
            { 
                ProductId = productId , 
                ImagePath=PathConstants.ImagesPath+"defaultImageForCar.png"
            } 
        });
            
    }
    
}