namespace DrivingSchoolWebApp.Web.Areas.SchoolManage.Controllers
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Car;
    using Services.Models.School;
    using Web.Controllers;
    using X.PagedList;

    [Area("SchoolManage")]
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
            try
            {
                var car = this.carService.GetCarById<CarDetailsViewModel>(id);

                return this.View(car);
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
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
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var car = this.carService.Create(model).GetAwaiter().GetResult();

                return this.RedirectToAction("Details", "Cars", new { id = car.Id });
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }

        }

        // GET: SchoolsManage/Cars/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var carModel = this.carService.GetCarById<EditCarInputModel>(id);

                return this.View(carModel);
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // POST: SchoolsManage/Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCarInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                if (!this.HasRights(model.Id))
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                }

                var car = this.carService.Edit(model).GetAwaiter().GetResult();

                return this.RedirectToAction("Details", "Cars", new { id = car.Id });
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // GET: SchoolsManage/Cars/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (!this.HasRights(id))
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                }

                var carModel = this.carService.GetCarById<CarDetailsViewModel>(id);

                return this.View(carModel);

            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // POST: SchoolsManage/Cars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CarDetailsViewModel model)
        {
            try
            {
                if (!this.HasRights(model.Id))
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                } 

                this.carService.Delete(model).GetAwaiter().GetResult();

                return this.RedirectToAction(nameof(this.All));
            }
            catch
            {
                return this.View();
            }
        }

        //todo Implement change owner for car . Maybe with isInUse prop=?

        private bool HasRights(int carId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var car = this.carService.GetCarById<CarDetailsViewModel>(carId);

            var result = (userId == car.OwnerManagerId) || this.User.IsInRole("Admin");
            return result;
        }
    }
}