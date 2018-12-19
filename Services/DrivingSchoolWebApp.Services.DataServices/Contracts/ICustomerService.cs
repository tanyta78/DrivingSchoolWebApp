namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.Customer;

    public interface ICustomerService
    {
        IEnumerable<Customer> All();

        Customer GetCustomerById(int id);

        Customer GetCustomerByUserId(string userId);

        Customer Create(AppUser user);

        void Edit(CustomerViewModel model);

        void Delete(int id);
    }
}
