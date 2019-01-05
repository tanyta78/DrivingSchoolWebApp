namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using Mapping;
    using Models.School;

    public class SchoolService : ISchoolService
    {
        private readonly IRepository<School> schoolRepository;

        public SchoolService(IRepository<School> schoolRepository)
        {
            this.schoolRepository = schoolRepository;
        }

        public IEnumerable<TViewModel> AllActiveSchools<TViewModel>()
        {
            return this.schoolRepository.All().Where(s => s.IsActive).ProjectTo<TViewModel>();
        }

        public School Create(CreateSchoolInputModel model)
        {
            var school = Mapper.Map<School>(model);
            
            this.schoolRepository.AddAsync(school).GetAwaiter().GetResult();
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return school;
        }

        public School Delete(int id)
        {
            //todo 
            //change school isActive to false
            var school = this.GetSchoolById(id);
            school.IsActive = false;
            this.schoolRepository.Update(school);
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

            //change manager usertype to customer and remove user from role school and isApproved set to false=> ONLY with admin rights or approvement
            //do this in controller and call account service

            return school;
        }

        public School Edit(EditSchoolInputModel model)
        {
            var school = Mapper.Map<School>(model);

            this.schoolRepository.Update(school);
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return school;
        }

        public School ChangeManager(int schoolId, string newManagerId)
        {
            var school = this.GetSchoolById(schoolId);

            school.ManagerId = newManagerId;

            this.schoolRepository.Update(school);
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return school;
        }

        public TViewModel GetSchoolById<TViewModel>(int id)
        {
            var school = this.schoolRepository
                .All()
                .Where(s => s.Id == id)
                .To<TViewModel>().FirstOrDefault();
            return school;
        }

        public TViewModel GetSchoolByManagerName<TViewModel>(string username)
        {
            var school = this.schoolRepository.All()
                .Where(x => x.Manager.UserName == username)
                .To<TViewModel>()
                .FirstOrDefault();

            if (school == null)
            {
                throw new ArgumentException("No school with this manager name in db");
            }

            return school;
        }

        private School GetSchoolById(int schoolId)
        {
            var school = this.schoolRepository
                .All()
                .FirstOrDefault(x => x.Id == schoolId);

            if (school == null)
            {
                throw new ArgumentException("No school with id in db");
            }


            return school;
        }

       
    }
}
