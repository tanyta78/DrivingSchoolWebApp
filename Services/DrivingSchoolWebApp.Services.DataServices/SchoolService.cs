namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Models.School;

    public class SchoolService : BaseService,ISchoolService
    {
        private readonly IRepository<School> schoolRepository;

        public SchoolService(IRepository<School> schoolRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
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
            //todo check manager is in role school 
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

        public School Edit(EditSchoolInputModel model)
        {
            //todo can change office address, trade mark
            var school = this.GetSchoolById<School>(model.Id);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);
            
            if (!this.HasRightsToEditOrDelete(model.Id, username))
            {
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            school.OfficeAddress = model.OfficeAddress;
            school.TradeMark = model.TradeMark;

            this.schoolRepository.Update(school);
            return school;
        }

        public School ChangeManager(int schoolId,AppUser newManager)
        {
            //todo change manager must change manager usertype to customer and remove user from role school => ONLY with admin rights or approvement=> do this in controller with user service or account service
            var school = this.GetSchoolById<School>(schoolId);

            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and car for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var hasRights = roles.Any(x => x == "Admin");

            if (!hasRights)
            {
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            school.ManagerId = user.Id;

            this.schoolRepository.Update(school);
            return school;
        }

        public TViewModel GetSchoolById<TViewModel>(int id)
        {
            var school = this.schoolRepository.All()
                             .Where(x => x.Id == id)
                             .To<TViewModel>().FirstOrDefault();

           if (school == null)
            {
                throw new ArgumentException("No school with id in db");
            }

           
            return school;
        }

        public TViewModel GetSchoolByManagerName<TViewModel>(string username)
        {
            var school = this.schoolRepository.All()
                             .Where(x => x.Manager.UserName == username)
                             .To<TViewModel>().FirstOrDefault();

            if (school == null)
            {
                throw new ArgumentException("No school with this manager name in db");
            }

            return school;
        }

        private bool HasRightsToEditOrDelete(int schoolId, string username)
        {
            var school = this.GetSchoolById<School>(schoolId);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and car for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var hasRights = roles.Any(x => x == "Admin");
            var isManager = username == school.Manager.UserName;

            return isManager || hasRights;
        }




    }



}
