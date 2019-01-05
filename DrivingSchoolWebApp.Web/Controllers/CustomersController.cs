namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Customer;

    public class CustomersController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly IAccountService accountService;

        public CustomersController(ICustomerService customerService, IAccountService accountService)
        {
            this.customerService = customerService;
            this.accountService = accountService;
        }

        // GET: Customers
        [Authorize]
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Customers/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var customer = this.customerService.GetCustomerById<DetailsCustomerViewModel>(id);

            return this.View(customer);
        }

        // GET: Customers/Create/2
        [Authorize]
        public ActionResult Create(string userId)
        {
            this.ViewBag.UserId = userId;
            return this.View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(CreateCustomerInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var customer = this.customerService.Create(model);
                this.accountService.SetRole("User", model.UserId);

                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // TODO: Add update logic here for edit and delete customer . Update service.
        //// GET: Customers/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return this.View();
        //}

        //// POST: Customers/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {

        //        return this.RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return this.View();
        //    }
        //}

        //// GET: Customers/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return this.View();
        //}

        //// POST: Customers/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return this.RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return this.View();
        //    }

        private bool HasRights(int customerId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = this.customerService.GetCustomerById<DetailsCustomerViewModel>(customerId);

            var result = (userId == customer.UserId) || this.User.IsInRole("Admin");
            return result;
        }
    }
}