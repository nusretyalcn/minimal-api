using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface ICustomerService
{
    IDataResult<List<Customer>> GetAll();
    IDataResult<Customer> GetById(int id);
    IResult Add(Customer customer);
    IResult Update(Customer customer);
    IResult Delete(Customer customer);
}