using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Library;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private Project0StoreContext context;
        private DbSet<Customer> table = null;

        public CustomerRepository()
        {
            context = new Project0StoreContext();
            table = context.Set<Customer>();
        }
        public CustomerRepository(Project0StoreContext context)
        {
            this.context = context;
            table = context.Set<Customer>();
        }
        public IEnumerable<StoreApplication.Library.Models.Customer> GetCustomers()
        {
            var customers = context.Customer.ToList();
            return customers.Select(Mapper.MapCustomerEntity);
        }

        public StoreApplication.Library.Models.Customer GetById(object id)
        {
            return Mapper.MapCustomerEntity(context.Customer.Find(id));
        }

        /// <summary>
        /// Looks in the DB for any customer with the given first and last names
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <returns></returns>
        private List<Customer> findCustomerByName(string FirstName, string LastName)
        {

            List<Customer> customer = context.Customer.Where(c => (c.FirstName == FirstName) && (c.LastName == LastName)).ToList();
            return customer;
        }

        // Looks in the DB for any customer with the given first,last, and username
        public StoreApplication.Library.Models.Customer findCustomer(string FirstName, string LastName, string UserName)
        {
            List<Customer> customers = findCustomerByName(FirstName, LastName);
            if(customers.Count > 1)
            {
                return Mapper.MapCustomerEntity(customers.Find(c => c.UserName == UserName));
            }
            else if (customers.Count == 1)
            {
                if(customers.Find(c => c.UserName == UserName) == null)
                {
                    return null;
                }

                return Mapper.MapCustomerEntity(customers[0]);
            }
            else
            {
                return null;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void AddCustomer(StoreApplication.Library.Models.Customer customer)
        {
            Customer entity = Mapper.MapCustomerDTO(customer);
            context.Customer.Add(entity);
        }

        public bool DoesUsernameExist(StoreApplication.Library.Models.Customer customer)
        {
            Customer entity = Mapper.MapCustomerDTO(customer);
            bool result = (context.Customer.FirstOrDefault(c => c.UserName == entity.UserName) != null);
            return result;
        }
    }
}
