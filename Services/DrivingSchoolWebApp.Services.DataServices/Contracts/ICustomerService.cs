namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.Customer;

    public interface ICustomerService
    {
        IEnumerable<TViewModel> All<TViewModel>();

        TViewModel GetCustomerById<TViewModel>(int id);

        Customer GetCustomerById(int id);

        TViewModel GetCustomerByUserId<TViewModel>(string userId);

        Customer GetCustomerByUserId(string userId);

        Customer Create(CreateCustomerInputModel model);

        //Customer Edit(CustomerViewModel model);

        Customer Delete(int id);
    }
}
