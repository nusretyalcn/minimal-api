// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;

ProductManager productManager = new ProductManager(new EfProductDal());


var result = productManager.GetProductDetails();

if (result.Success)
{
    foreach (var product in result.Data)
    {
        Console.WriteLine(product.ProductName + " " + product.CategoryName);
    }
}
else
{
    Console.WriteLine(result.Message);
}



// CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
//
// foreach (var c in customerManager.GetAll())
// {
//     Console.WriteLine(c.CustomerName);
// }

Console.WriteLine("Hello, World!");



