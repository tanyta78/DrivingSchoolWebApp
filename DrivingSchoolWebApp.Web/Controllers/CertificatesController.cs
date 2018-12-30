namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Certificate;
    using Services.Models.Customer;
    using Services.Models.School;

    public class CertificatesController : BaseController
    {
        private readonly ICertificateService certificateService;
        private readonly ICustomerService customerService;
        private readonly ISchoolService schoolService;

        public CertificatesController(ICertificateService certificateService, ICustomerService customerService, ISchoolService schoolService)
        {
            this.certificateService = certificateService;
            this.customerService = customerService;
            this.schoolService = schoolService;
        }

        // GET: Certificates/All
        public ActionResult All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var certificates = new List<AllCertificatesViewModel>();

            if (this.User.IsInRole("Customer"))
            {
                var customerId = this.customerService.GetCustomerByUserId<DetailsCustomerViewModel>(userId).Id;
                certificates = this.certificateService.GetCertificatesByCustomerId<AllCertificatesViewModel>(customerId).ToList();
            }
            else if (this.User.IsInRole("School"))
            {
                var schoolId = this.schoolService
                    .GetSchoolByManagerName<DetailsSchoolViewModel>(this.User.Identity.Name).Id;
                certificates = this.certificateService.GetCertificatesBySchoolId<AllCertificatesViewModel>(schoolId).ToList();
            }
            return this.View(certificates);
        }

        //Get: Certificates/Details/3
        public IActionResult Details(int id)
        {
            try
            {
                var certificate = this.certificateService.GetCertificateById<DetailsCertificateViewModel>(id);

                return this.View(certificate);
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }
    }
}