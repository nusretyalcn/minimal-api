using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfCategoryDal:ICategoryDal
{
    public List<Category> GetAll(Expression<Func<Category, bool>>? filter = null)
    {
        using (EfDbContext context = new EfDbContext())
        {
            return filter == null
                ? context.Set<Category>().ToList()
                : context.Set<Category>().Where(filter).ToList();
        }
    }

    public Category Get(Expression<Func<Category, bool>>? filter)
    {
        using (EfDbContext context = new EfDbContext())
        {
            return context.Set<Category>().SingleOrDefault(filter);
        }
    }

    public void Add(Category entity)
    {
        using (EfDbContext context = new EfDbContext())
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void Update(Category entity)
    {
        using (EfDbContext context = new EfDbContext())
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void Delete(Category entity)
    {
        using (EfDbContext context = new EfDbContext())
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }
}