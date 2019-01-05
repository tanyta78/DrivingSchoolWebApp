namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Data.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Course;
    using Services.Models.Customer;
    using Services.Models.Order;
    using Services.Models.School;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly ICourseService courseService;
        private readonly ICustomerService customerService;
        private readonly ISchoolService schoolService;

        public OrdersController(IOrderService orderService, ICourseService courseService, ICustomerService customerService, ISchoolService schoolService)
        {
            this.orderService = orderService;
            this.courseService = courseService;
            this.customerService = customerService;
            this.schoolService = schoolService;
        }

        // GET: Orders/All
        [Authorize]
        public ActionResult All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = new List<AllOrdersViewModel>();
            if (this.User.IsInRole(GlobalDataConstants.SchoolRoleName))
            {
                var schoolId = this.schoolService
                    .GetSchoolByManagerName<DetailsSchoolViewModel>(this.User.Identity.Name).Id;
                orders = this.orderService.GetOrdersBySchoolId<AllOrdersViewModel>(schoolId).ToList();
            }
            else if (this.User.IsInRole(GlobalDataConstants.AdministratorRoleName))
            {
                orders = this.orderService.All<AllOrdersViewModel>().ToList();
            }
            else
            {
                var customerId = this.customerService.GetCustomerByUserId<DetailsCustomerViewModel>(userId).Id;
                orders = this.orderService.GetOrdersByCustomerId<AllOrdersViewModel>(customerId).ToList();
            }
            return this.View(orders);
        }

        // GET: Orders/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var order = this.orderService.GetOrderById<DetailsOrderViewModel>(id);

            return this.View(order);
        }


        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalDataConstants.UserRoleName)]
        public ActionResult Create([FromForm]int courseId)
        {
            try
            {
                var course = this.courseService.GetCourseById<DetailsCourseViewModel>(courseId);

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customer = this.customerService.GetCustomerByUserId(userId);

                var model = new CreateOrderInputModel()
                {
                    CourseId = courseId,
                    CustomerId = customer.Id,
                    ActualPriceWhenOrder = course.Price
                };
                var order = this.orderService.Create(model).GetAwaiter().GetResult();

                return this.RedirectToAction("Details", "Orders", new { Area = "", id = order.Id });
            }
            catch (Exception error)
            {
                //todo handle exceptions with filter and log it
                return this.View("_Error", error);
            }
        }

        // POST: Orders/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalDataConstants.UserRoleName)]
        public ActionResult Cancel(int id)
        {
            try
            {
                var result = this.HasRights(id);
                if (!result)
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                }
                this.orderService.CancelOrder(id).GetAwaiter().GetResult();

                return this.RedirectToAction(nameof(All));
            }
            catch (Exception error)
            {
                //todo handle exceptions with filter and log it
                return this.View("_Error", error.Message);
            }
        }

        private bool HasRights(int orderId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this.orderService.GetOrderById<DetailsOrderViewModel>(orderId);

            var result = (userId == order.CustomerUserId) || this.User.IsInRole(GlobalDataConstants.AdministratorRoleName);
            return result;
        }

        // TODO: Decide who can delete order. Does it needed?
        //// GET: Orders/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return this.View();
        //}

        //// POST: Orders/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        

        //        return this.RedirectToAction(nameof(All));
        //    }
        //    catch
        //    {
        //        return this.View();
        //    }
        //}
    }
}