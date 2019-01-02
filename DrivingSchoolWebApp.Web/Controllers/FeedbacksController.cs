namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Customer;
    using Services.Models.Feedback;
    using Services.Models.Order;
    using Services.Models.School;

    public class FeedbacksController : BaseController
    {
        private readonly IFeedbackService feedbackService;
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;
        private readonly ISchoolService schoolService;

        public FeedbacksController(IFeedbackService feedbackService, IOrderService orderService, ICustomerService customerService, ISchoolService schoolService)
        {
            this.feedbackService = feedbackService;
            this.orderService = orderService;
            this.customerService = customerService;
            this.schoolService = schoolService;
        }

        // GET: Feedbacks/All
        public ActionResult All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var feedbacks = new List<AllFeedbackViewModel>();
            try
            {
                if (this.User.IsInRole("Admin"))
                {
                    feedbacks = this.feedbackService.All<AllFeedbackViewModel>().ToList();
                }
                else if (this.User.IsInRole("School"))
                {
                    var schoolId = this.schoolService
                        .GetSchoolByManagerName<DetailsSchoolViewModel>(this.User.Identity.Name).Id;
                    feedbacks = this.feedbackService.GetFeedbacksBySchoolId<AllFeedbackViewModel>(schoolId).ToList();
                }
                else
                {
                    var customerId = this.customerService.GetCustomerByUserId<DetailsCustomerViewModel>(userId).Id;
                    feedbacks = this.feedbackService.GetFeedbacksByCustomerId<AllFeedbackViewModel>(customerId).ToList();
                }
                return this.View(feedbacks);
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // GET: Feedbacks/Create/5
       public ActionResult Create(int orderId)
        {
            var order = this.orderService.GetOrderById<DetailsOrderViewModel>(orderId);

            var model = new CreateFeedbackInputModel()
            {
                CourseId = order.CourseId,
                CustomerId = order.CustomerId
            };

            return this.View(model);
        }

        // POST: Feedbacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFeedbackInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var feedback = this.feedbackService.Create(model).GetAwaiter().GetResult();

                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        private bool HasRights(int feedbackId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this.feedbackService.GetFeedbackById<AllFeedbackViewModel>(feedbackId);

            var result = (userId == order.CustomerUserId) || this.User.IsInRole("Admin");
            return result;
        }
    }
}