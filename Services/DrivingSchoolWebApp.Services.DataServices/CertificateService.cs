namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Models.Certificate;

    public class CertificateService : ICertificateService
    {
        private readonly IRepository<Certificate> certificateRepository;

        public CertificateService(IRepository<Certificate> certificateRepository)
        {
            this.certificateRepository = certificateRepository;
        }

        public IEnumerable<Certificate> All()
        {
            return this.certificateRepository.All().ToList();
        }

        public IEnumerable<TViewModel> All<TViewModel>()
        {
            var certificates = this.certificateRepository.All().ProjectTo<TViewModel>().ToList();

            return certificates;
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
            var certificate = new Certificate
            {
                CourseId = model.CourseId,
                CustomerId = model.CustomerId
            };

            await this.certificateRepository.AddAsync(certificate);
            await this.certificateRepository.SaveChangesAsync();

            return certificate;
        }

      
    }
}
