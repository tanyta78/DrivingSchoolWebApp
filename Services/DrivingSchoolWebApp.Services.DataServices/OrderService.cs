namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;
    using Models.Order;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public IEnumerable<Order> All()
        {
            return this.orderRepository.All().ToList();
        }

        public IEnumerable<TViewModel> All<TViewModel>()
        {
            var orders = this.orderRepository.All().ProjectTo<TViewModel>().ToList();

            return orders;
        }

        public TViewModel GetOrderById<TViewModel>(int orderId)
        {
            var order = this.orderRepository.All().Where(x => x.Id == orderId)
                             .To<TViewModel>().FirstOrDefault();

            if (order == null)
            {
                throw new ArgumentException("No order with id in db");
            }

            return order;
        }

        public IEnumerable<TViewModel> GetOrdersByCustomerId<TViewModel>(int customerId)
        {
            var orders = this.orderRepository.All().Where(x => x.CustomerId == customerId).ProjectTo<TViewModel>().ToList();

            return orders;
        }

        public IEnumerable<TViewModel> GetOrdersByCourseId<TViewModel>(int courseId)
        {
            var orders = this.orderRepository.All().Where(x => x.CourseId == courseId).ProjectTo<TViewModel>().ToList();

            return orders;
        }

        public IEnumerable<TViewModel> GetOrdersBySchoolId<TViewModel>(int schoolId)
        {
            var orders = this.orderRepository.All().Where(x => x.Course.SchoolId == schoolId).ProjectTo<TViewModel>().ToList();

            return orders;
        }

        public IEnumerable<TViewModel> GetOrdersByStatus<TViewModel>(OrderStatus status)
        {
            var orders = this.orderRepository.All().Where(x => x.OrderStatus == status).ProjectTo<TViewModel>().ToList();

            return orders;
        }

        public IEnumerable<TViewModel> GetOrdersBySchoolIdAndPaymentMade<TViewModel>(int schoolId)
        {
            var orders = this.orderRepository.All().Where(x => x.Course.SchoolId == schoolId && (x.OrderStatus == OrderStatus.PaymentReceived || x.OrderStatus == OrderStatus.PaymentUpdated || x.OrderStatus == OrderStatus.Completed)).ProjectTo<TViewModel>().ToList();

            return orders;
        }

        public IEnumerable<TViewModel> GetOrdersBySchoolIdPaymentMadeAndTrainerId<TViewModel>(int schoolId, int trainerId)
        {
            var orders = this.orderRepository.All().Where(x => x.Course.SchoolId == schoolId && x.Course.TrainerId == trainerId && (x.OrderStatus == OrderStatus.PaymentReceived || x.OrderStatus == OrderStatus.PaymentUpdated || x.OrderStatus == OrderStatus.Completed)).ProjectTo<TViewModel>().ToList();

            return orders;
        }

        public async Task<Order> Create(CreateOrderInputModel model)
        {
            var order = new Order
            {
                CustomerId = model.CustomerId,
                CourseId = model.CourseId,
                ActualPriceWhenOrder = model.ActualPriceWhenOrder
            };

            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order> ChangeStatus(int id, OrderStatus newStatus)
        {
            var order = this.GetOrderById(id);

            order.OrderStatus = newStatus;
            this.orderRepository.Update(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order> CancelOrder(int id)
        {
            var order = this.GetOrderById(id);

            order.OrderStatus = OrderStatus.Cancelled;
            this.orderRepository.Update(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order> Delete(int id, string username)
        {
            var order = this.GetOrderById(id);

            this.orderRepository.Delete(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }


        private Order GetOrderById(int orderId)
        {
            var order = this.orderRepository.All()
                .FirstOrDefault(x => x.Id == orderId);

            if (order == null)
            {
                throw new ArgumentException("No order with id in db");
            }

            return order;
        }
    }
}
