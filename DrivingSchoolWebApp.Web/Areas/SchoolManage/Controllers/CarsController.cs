namespace DrivingSchoolWebApp.Web.Areas.SchoolManage.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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

        // GET: SchoolsManage/Cars/All
        public ActionResult All(int? page)
        {
            var schoolId = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(this.User.Identity.Name).Id;
            var cars = this.carService.GetCarsBySchoolId<CarViewModel>(schoolId);

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            IPagedList<CarViewModel> model = cars.ToPagedList(pageNumber, 5); // will only contain 5 products max because of the pageSize

            return this.View(model);
        }

        // GET: SchoolsManage/Cars/Details/5
        public ActionResult Details(int id)
        {
            var car = this.carService.GetCarById<CarDetailsViewModel>(id);

            return this.View(car);
        }

        // GET: SchoolsManage/Cars/Create
        public ActionResult Create()
        {
            var schoolId = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(this.User.Identity.Name).Id;
            this.ViewBag.SchoolId = schoolId;
            return this.View();
        }

        // POST: SchoolsManage/Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCarInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var carId = this.carService.Create(model).GetAwaiter().GetResult().Id;

            return this.RedirectToAction("Details", "Cars", carId);

        }

        // GET: SchoolsManage/Cars/Edit/5
        public ActionResult Edit(int id)
        {
            var carModel = this.carService.GetCarById<EditCarInputModel>(id);

            return this.View(carModel);
        }

        // POST: SchoolsManage/Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCarInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var carId = this.carService.Edit(model);

            return this.RedirectToAction("Details", "Cars", carId);
        }

        // GET: SchoolsManage/Cars/Delete/5
        public ActionResult Delete(int id)
        {
            var carModel = this.carService.GetCarById<CarDetailsViewModel>(id);

            return this.View(carModel);
        }

        // POST: SchoolsManage/Cars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                this.carService.Delete(id).GetAwaiter().GetResult();

                return this.RedirectToAction(nameof(this.All));
            }
            catch
            {
                return this.View();
            }
        }

        //todo Implement change owner for car . Maybe with isInUse prop=?
    }
}