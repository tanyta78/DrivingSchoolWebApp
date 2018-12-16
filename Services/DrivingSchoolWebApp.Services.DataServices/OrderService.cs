namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Models.Order;

    public class OrderService : BaseService, IOrderService
    {
        private readonly IRepository<Order> orderRepository;

        public OrderService(IRepository<Order> orderRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
        {
            this.orderRepository = orderRepository;
        }

        public IEnumerable<Order> All()
        {
            return this.orderRepository.All().ToList();
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

        public async Task<Order> Create(CreateOrderInputModel model)
        {
            var order = this.Mapper.Map<Order>(model);

            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order> ChangeStatus(int id, OrderStatus newStatus)
        {
            var order = this.GetOrderById<Order>(id);

            if (!this.HasRightsToEditOrDelete(id) || order.OrderStatus==OrderStatus.Cancelled)
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            order.OrderStatus = newStatus;
            this.orderRepository.Update(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order> CancelOrder(int id)
        {
            var order = this.GetOrderById<Order>(id);

            if (!this.HasRightsToEditOrDelete(id))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            order.OrderStatus = OrderStatus.Cancelled;
            this.orderRepository.Update(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task Delete(int id)
        {
            var order = this.GetOrderById<Order>(id);

            if (!this.HasRightsToEditOrDelete(id))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }
            this.orderRepository.Delete(order);
            await this.orderRepository.SaveChangesAsync();
        }

        private bool HasRightsToEditOrDelete(int orderId)
        {
            var order = this.GetOrderById<Order>(orderId);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and car for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var hasRights = roles.Any(x => x == "Admin");

            return hasRights;
        }
    }
}
