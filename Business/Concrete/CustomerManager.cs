using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CustomerManager:ICustomerService
{
    private ICustomerDal _customerDal;

    public CustomerManager(ICustomerDal customerDal)
    {
        _customerDal = customerDal;
    }

    public List<Customer> GetAll()
    {
        return _customerDal.GetAll();
    }

    public Customer GetById(int id)
    {
        return _customerDal.Get(p=>p.Id==id);
    }

    public void Add(Customer customer)
    {
        _customerDal.Add(customer);
    }
}