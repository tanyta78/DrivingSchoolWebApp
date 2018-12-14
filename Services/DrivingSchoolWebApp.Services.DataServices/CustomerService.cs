namespace DrivingSchoolWebApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using Mapping;
    using Models.Customer;

    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IEnumerable<Customer> All()
        {
            var customers = this.customerRepository.All();
            return customers;
        }

        public Customer Create(AppUser user)
        {
            var customer = new Customer()
            {
                User = user
            };

            this.customerRepository.AddAsync(customer).GetAwaiter().GetResult();
            this.customerRepository.SaveChangesAsync().GetAwaiter().GetResult();

           return customer;
        }

        public void Delete(int id)
        {
            //todo find user and set isEnabled to false =>check this
            var customer = this.GetCustomerById<Customer>(id);
            var user = customer.User;
            user.IsEnabled = false;
            this.customerRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        public void Edit(CustomerViewModel model)
        {
            //todo decide model and what to change?!? do not change userId
            //maybe you do not need this
        }

        public TViewModel GetCustomerById<TViewModel>(int id)
        {
            var customer = this.customerRepository.All().Where(c => c.Id == id).To<TViewModel>().FirstOrDefault();
            return customer;
        }

        public TViewModel GetCustomerByUserId<TViewModel>(string userId)
        {
            var customer = this.customerRepository.All().Where(c => c.User.Id == userId).To<TViewModel>().FirstOrDefault();
            return customer;
        }
    }
}
