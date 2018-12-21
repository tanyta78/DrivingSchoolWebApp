namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Models.Certificate;

    public class CertificateService:BaseService,ICertificateService
    {
        private readonly IRepository<Certificate> certificateRepository;

        public CertificateService(IRepository<Certificate> certificateRepository,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
        {
            this.certificateRepository = certificateRepository;
        }

        public IEnumerable<Certificate> All()
        {
            return this.certificateRepository.All().ToList();
        }

        public TViewModel GetCertificateById<TViewModel>(int certificateId)
        {
            var certificate = this.certificateRepository.All()
                .Where(x => x.Id == certificateId)
                .To<TViewModel>()
                .FirstOrDefault();

            if (certificate == null)
            {
                throw new ArgumentException("No Certificate with id in db");
            }

            return certificate;
        }

        public IEnumerable<TViewModel> GetCertificatesByCustomerId<TViewModel>(int customerId)
        {
            var certificates = this.certificateRepository.All().Where(x => x.CustomerId == customerId).ProjectTo<TViewModel>().ToList();

            return certificates;
        }

        public IEnumerable<TViewModel> GetCertificatesByCourseId<TViewModel>(int courseId)
        {
            var certificates = this.certificateRepository.All().Where(x => x.CourseId == courseId).ProjectTo<TViewModel>().ToList();

            return certificates;
        }

        public IEnumerable<TViewModel> GetCertificatesBySchoolId<TViewModel>(int schoolId)
        {
            var certificates = this.certificateRepository.All().Where(x => x.Course.SchoolId == schoolId).ProjectTo<TViewModel>().ToList();

            return certificates;
        }

        public async Task<Certificate> Create(CreateCertificateInputModel model)
        {
            var certificate = this.Mapper.Map<Certificate>(model);

            await this.certificateRepository.AddAsync(certificate);
            await this.certificateRepository.SaveChangesAsync();

            return certificate;
        }

        public async Task Delete(int id)
        {
            var certificate = this.GetCertificateById(id);

            if (!this.HasRightsToEditOrDelete(id))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }
            this.certificateRepository.Delete(certificate);
            await this.certificateRepository.SaveChangesAsync();
        }

        private bool HasRightsToEditOrDelete(int certificateId)
        {
            var certificate = this.GetCertificateById(certificateId);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and Certificate for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var isAdmin = roles.Any(x => x == "Admin");
            var isCreator = username == certificate.Customer.User.UserName;

            return isCreator || isAdmin;
        }

        private Certificate GetCertificateById(int certificateId)
        {
            var certificate = this.certificateRepository.All()
                .FirstOrDefault(x => x.Id == certificateId);

            if (certificate == null)
            {
                throw new ArgumentException("No Certificate with id in db");
            }

            return certificate;
        }
    }
}
