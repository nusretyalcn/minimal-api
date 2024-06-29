using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfProductDal:IProductDal
{

    public List<Product> GetAll(Expression<Func<Product, bool>>? filter = null)
    {
        using (EfDbContext context = new EfDbContext())
        {
            return filter == null 
                ? context.Set<Product>().ToList() 
                : context.Set<Product>().Where(filter).ToList();
        }
    }

    public Product Get(Expression<Func<Product, bool>>? filter)
    {
        using (EfDbContext context = new EfDbContext())
        {
            return context.Set<Product>().SingleOrDefault(filter);
        }
    }

    public void Add(Product entity)
    {
        using (EfDbContext context =new EfDbContext())
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void Update(Product entity)
    {
        using (EfDbContext context =new EfDbContext())
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public void Delete(Product entity)
    {
        using (EfDbContext context =new EfDbContext())
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}