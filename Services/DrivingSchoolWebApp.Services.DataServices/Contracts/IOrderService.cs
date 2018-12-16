namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Models.Enums;
    using Models.Order;

    public interface IOrderService
    {
        IEnumerable<Order> All();

        TViewModel GetOrderById<TViewModel>(int orderId);

        IEnumerable<TViewModel> GetOrdersByCustomerId<TViewModel>(int customerId);

        IEnumerable<TViewModel> GetOrdersByCourseId<TViewModel>(int courseId);

        IEnumerable<TViewModel> GetOrdersBySchoolId<TViewModel>(int schoolId);

        IEnumerable<TViewModel> GetOrdersByStatus<TViewModel>(OrderStatus status);

        Task<Order> Create(CreateOrderInputModel model);

        Task<Order> ChangeStatus(int id,OrderStatus newStatus);

        Task<Order> CancelOrder (int id);
        
        Task Delete(int id);
    }
}
