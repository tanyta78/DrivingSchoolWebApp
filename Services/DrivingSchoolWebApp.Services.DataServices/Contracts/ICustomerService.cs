namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.Customer;

    public interface ICustomerService
    {
        IEnumerable<Customer> All();

        TViewModel GetCustomerById<TViewModel>(int id);

        TViewModel GetCustomerByUserId<TViewModel>(string userId);

        Customer Create(AppUser user);

        void Edit(CustomerViewModel model);

        void Delete(int id);
    }
}
