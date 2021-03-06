﻿namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Data.Common;
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public ActionResult All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var certificates = new List<AllCertificatesViewModel>();

            if (this.User.IsInRole(GlobalDataConstants.AdministratorRoleName))
            {
                certificates = this.certificateService.All<AllCertificatesViewModel>().ToList();
            }
            else if (this.User.IsInRole(GlobalDataConstants.SchoolRoleName))
            {
                var schoolId = this.schoolService
                    .GetSchoolByManagerName<DetailsSchoolViewModel>(this.User.Identity.Name).Id;
                certificates = this.certificateService.GetCertificatesBySchoolId<AllCertificatesViewModel>(schoolId).ToList();
            }
            else
            {
                var customerId = this.customerService.GetCustomerByUserId<DetailsCustomerViewModel>(userId).Id;
                certificates = this.certificateService.GetCertificatesByCustomerId<AllCertificatesViewModel>(customerId).ToList();
            }
            return this.View(certificates);
        }

        //Get: Certificates/Details/3
        [Authorize]
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