using StoreApplication.Library.Models;
using System.Collections.Generic;

namespace StoreApplication.Library
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer findCustomer(string FirstName, string LastName, string UserName);
        void AddCustomer(Customer customer);
        void Save();

        bool DoesUsernameExist(Customer customer);
    }
}
