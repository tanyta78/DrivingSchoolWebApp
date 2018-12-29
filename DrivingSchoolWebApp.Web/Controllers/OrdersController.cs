namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
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
        public ActionResult All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = new List<AllOrdersViewModel>();
            if (this.User.IsInRole("Customer"))
            {
                var customerId = this.customerService.GetCustomerByUserId<DetailsCustomerViewModel>(userId).Id;
                orders = this.orderService.GetOrdersByCustomerId<AllOrdersViewModel>(customerId).ToList();
            }
            else if (this.User.IsInRole("School"))
            {
                var schoolId = this.schoolService
                    .GetSchoolByManagerName<DetailsSchoolViewModel>(this.User.Identity.Name).Id;
                orders = this.orderService.GetOrdersBySchoolId<AllOrdersViewModel>(schoolId).ToList();
            }
            return this.View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var order = this.orderService.GetOrderById<DetailsOrderViewModel>(id);

            return this.View(order);
        }

      
        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]int courseId)
        {
            try
            {
                var course = this.courseService.GetCourseById<DetailsCourseViewModel>(courseId);
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var customerId = this.customerService.GetCustomerByUserId(userId).Id;
                var model = new CreateOrderInputModel()
                {
                    CourseId = courseId,
                    CustomerId = customerId
                };
                var order = this.orderService.Create(model).GetAwaiter().GetResult();

                return this.RedirectToAction("Details","Orders",order.Id);
            }
            catch (Exception error)
            {
                //todo handle exceptions with filter and log it
                return this.View("_Error",error);
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return this.RedirectToAction(nameof(All));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return this.RedirectToAction(nameof(All));
            }
            catch
            {
                return this.View();
            }
        }
    }
}