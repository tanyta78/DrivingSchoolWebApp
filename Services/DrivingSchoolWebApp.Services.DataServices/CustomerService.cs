namespace DrivingSchoolWebApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models.Customer;

    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomerService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IRepository<Customer> customerRepository) : base(userManager, signInManager, mapper)
        {
            this.customerRepository = customerRepository;
        }

        public IEnumerable<Customer> All()
        {
            var customers = this.customerRepository.All();
            return customers;
        }

        public Customer Create(CreateCustomerInputModel model)
        {
            var customer = this.Mapper.Map<Customer>(model);

            this.customerRepository.AddAsync(customer).GetAwaiter().GetResult();
            this.customerRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return customer;
        }

        public void Delete(int id)
        {
            //todo find user and set isEnabled to false =>check this
            var customer = this.GetCustomerById(id);
            var user = customer.User;
            user.IsEnabled = false;
            this.customerRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        public void Edit(CustomerViewModel model)
        {
            //todo decide model and what to change?!? do not change userId
            //maybe you do not need this
        }

        public IEnumerable<TViewModel> All<TViewModel>()
        {
            var customers = this.customerRepository.All().ProjectTo<TViewModel>().ToList();
            return customers;
        }

        public TViewModel GetCustomerById<TViewModel>(int id)
        {
            var customer = this.customerRepository.All().Where(c => c.Id == id).To<TViewModel>().FirstOrDefault();
            return customer;
        }

        public Customer GetCustomerById(int id)
        {
            var customer = this.customerRepository.All()
                               .Include(c=>c.CoursesOrdered)
                               .Include(c=>c.ExamsTaken)
                               .Include(c=>c.Feedbacks)
                               .FirstOrDefault(c => c.Id == id);
            return customer;
        }

        public TViewModel GetCustomerByUserId<TViewModel>(string userId)
        {
            var customer = this.customerRepository.All().Where(c => c.UserId == userId).To<TViewModel>().FirstOrDefault();
            return customer;
        }

        public Customer GetCustomerByUserId(string userId)
        {
            var customer = this.customerRepository.All()
                               .Include(c=>c.CoursesOrdered)
                               .Include(c=>c.ExamsTaken)
                               .Include(c=>c.Feedbacks)
                              .FirstOrDefault(c => c.UserId == userId);
            return customer;
        }


    }
}
