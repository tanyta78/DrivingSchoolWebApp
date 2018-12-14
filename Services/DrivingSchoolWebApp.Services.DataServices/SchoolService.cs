namespace DrivingSchoolWebApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
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
            return this.schoolRepository.All().Where(s => s.IsActive).To<TViewModel>();
        }

        //only admin
        public School Create(AppUser manager)
        {
            //to check manager is in role school 
            var school = new School()
            {
                Manager = manager
            };

            this.schoolRepository.AddAsync(school).GetAwaiter().GetResult();
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return school;
        }

        public void Delete(int id)
        {
            //todo 
            //change school isActive to false
            var school = this.GetSchoolById<School>(id);
            school.IsActive = false;
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

            //change manager usertype to customer and remove user from role school and isApproved set to false=> ONLY with admin rights or approvement
            //do this in controller and call account service
        }

        public void Edit(CreateSchoolInputModel model)
        {
            //todo can change manager , office address, trade mark
            //when change manager must change manager usertype to customer and remove user from role school => ONLY with admin rights or approvement
        }

        public TViewModel GetSchoolById<TViewModel>(int id)
        {
            var school = this.schoolRepository.All()
                             .Where(x => x.Id == id)
                             .To<TViewModel>().FirstOrDefault();
            return school;
        }

        public TViewModel GetSchoolByManagerName<TViewModel>(string username)
        {
            var school = this.schoolRepository.All()
                             .Where(x => x.Manager.UserName == username)
                             .To<TViewModel>().FirstOrDefault();
            return school;
        }
    }
}
