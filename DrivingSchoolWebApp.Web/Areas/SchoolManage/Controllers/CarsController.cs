namespace DrivingSchoolWebApp.Web.Areas.SchoolManage.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Services.DataServices.Contracts;
    using Services.Models.Car;
    using Services.Models.School;
    using Web.Controllers;
    using X.PagedList;

    public class CarsController : BaseController
    {
        private readonly ICarService carService;
        private readonly ISchoolService schoolService;

        public CarsController(ICarService carService, ISchoolService schoolService)
        {
            this.carService = carService;
            this.schoolService = schoolService;
        }

        // GET: Cars/All
        public ActionResult All(int? page)
        {
            var schoolId = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(this.User.Identity.Name).Id;
            var cars = this.carService.GetCarsBySchoolId<CarViewModel>(schoolId);

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            IPagedList<CarViewModel> model = cars.ToPagedList(pageNumber, 5); // will only contain 5 products max because of the pageSize

            return this.View(model);
        }

        // GET: Cars/Details/5
        public ActionResult Details(int id)
        {
            var car = this.carService.GetCarById<CarDetailsViewModel>(id);

            return this.View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Cars/Edit/5
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

        // GET: Cars/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Cars/Delete/5
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