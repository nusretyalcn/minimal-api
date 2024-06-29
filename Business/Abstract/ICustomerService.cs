using Entities.Concrete;

namespace Business.Abstract;

public interface ICustomerService
{
    List<Customer> GetAll();
    Customer GetById(int id);
    void Add(Customer customer);
}