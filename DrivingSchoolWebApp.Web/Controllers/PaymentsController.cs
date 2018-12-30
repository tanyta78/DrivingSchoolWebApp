namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Customer;
    using Services.Models.Order;
    using Services.Models.Payment;
    using Services.Models.School;

    public class PaymentsController : BaseController
    {
        private readonly IPaymentService paymentService;
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;
        private readonly ISchoolService schoolService;

        public PaymentsController(IPaymentService paymentService, IOrderService orderService, ICustomerService customerService, ISchoolService schoolService)
        {
            this.paymentService = paymentService;
            this.orderService = orderService;
            this.customerService = customerService;
            this.schoolService = schoolService;
        }

        // GET: Payments/All
        public ActionResult All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var payments = new List<AllPaymentsViewModel>();
            if (this.User.IsInRole("Admin"))
            {
                payments = this.paymentService.All<AllPaymentsViewModel>().ToList();
            }
            else if (this.User.IsInRole("School"))
            {
                var schoolId = this.schoolService
                    .GetSchoolByManagerName<DetailsSchoolViewModel>(this.User.Identity.Name).Id;
                payments = this.paymentService.GetPaymentsBySchoolId<AllPaymentsViewModel>(schoolId).ToList();
            }
            else
            {
                var customerId = this.customerService.GetCustomerByUserId<DetailsCustomerViewModel>(userId).Id;
                payments = this.paymentService.GetPaymentsByCustomerId<AllPaymentsViewModel>(customerId).ToList();
            }
            return this.View(payments);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int id)
        {
            var payment = this.paymentService.GetPaymentById<DetailsPaymentViewModel>(id);

            return this.View(payment);
        }

        // GET: Payments/Create/5
        public ActionResult Create(int id)
        {
            try
            {
                var order = this.orderService.GetOrderById<DetailsOrderViewModel>(id);

              var model = new CreatePaymentInputModel()
                {
                    OrderId = id
                };

                return this.View(model);
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }

        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePaymentInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var order = this.orderService.GetOrderById<DetailsOrderViewModel>(model.OrderId);
                var remainedPaymentAmount = order.CoursePrice - order.PaymentsAmountSum;

                if (remainedPaymentAmount <= 0)
                {
                    return this.View("_Error", $"Amount of payment is greater than remained amount {remainedPaymentAmount}. ");
                }

                var payment = this.paymentService.Create(model).GetAwaiter().GetResult();
                //todo change order status when pay if payment =! null!!

                return this.RedirectToAction(nameof(All));
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }


    }
}