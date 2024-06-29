// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

ProductManager productManager = new ProductManager(new EfProductDal());


foreach (var product in productManager.GetProductDetails())
{
    Console.WriteLine(product.ProductName + " " + product.CategoryName);
}

// CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
//
// foreach (var c in customerManager.GetAll())
// {
//     Console.WriteLine(c.CustomerName);
// }

Console.WriteLine("Hello, World!");



