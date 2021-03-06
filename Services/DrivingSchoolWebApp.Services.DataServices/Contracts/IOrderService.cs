﻿namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Models.Enums;
    using Models.Order;

    public interface IOrderService
    {
        IEnumerable<Order> All();

        IEnumerable<TViewModel> All<TViewModel>();

        TViewModel GetOrderById<TViewModel>(int orderId);

        IEnumerable<TViewModel> GetOrdersByCustomerId<TViewModel>(int customerId);

        IEnumerable<TViewModel> GetOrdersByCourseId<TViewModel>(int courseId);

        IEnumerable<TViewModel> GetOrdersBySchoolId<TViewModel>(int schoolId);

        IEnumerable<TViewModel> GetOrdersByStatus<TViewModel>(OrderStatus status);

        IEnumerable<TViewModel> GetOrdersBySchoolIdAndPaymentMade<TViewModel>(int schoolId);

        IEnumerable<TViewModel> GetOrdersBySchoolIdPaymentMadeAndTrainerId<TViewModel>(int schoolId,int trainerId);
        
        Task<Order> Create(CreateOrderInputModel model);

        Task<Order> ChangeStatus(int id,OrderStatus newStatus);

        Task<Order> CancelOrder (int id);
        
        Task<Order> Delete(int id, string username);
    }
}
