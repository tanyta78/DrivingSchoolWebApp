namespace DrivingSchoolWebApp.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Customer;

    public class CustomersController : BaseController
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: Customers
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
        }

        // GET: Customers/Create/2
        public ActionResult Create(string userId)
        {
            this.ViewBag.UserId = userId;
            return this.View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCustomerInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var customer = this.customerService.Create(model);

            return this.RedirectToAction("Index", "Home");
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}