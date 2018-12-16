namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Models.Enums;
    using Models.Payment;

    public interface IPaymentService
    {
        IEnumerable<Payment> All();

        TViewModel GetPaymentById<TViewModel>(int paymentId);

        IEnumerable<TViewModel> GetPaymentsByCustomerId<TViewModel>(int customerId);

        IEnumerable<TViewModel> GetPaymentsByOrderId<TViewModel>(int orderId);

        IEnumerable<TViewModel> GetPaymentsBySchoolId<TViewModel>(int schoolId);

        IEnumerable<TViewModel> GetPaymentsByMethod<TViewModel>(PaymentMethod method);

        Task<Payment> Create(CreatePaymentInputModel model);

    }
}
