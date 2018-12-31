namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Models.School;

    public class SchoolService : BaseService, ISchoolService
    {
        private readonly IRepository<School> schoolRepository;

        public SchoolService(IRepository<School> schoolRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
        {
            this.schoolRepository = schoolRepository;
        }

        public IEnumerable<TViewModel> AllActiveSchools<TViewModel>()
        {
            return this.schoolRepository.All().Where(s => s.IsActive).ProjectTo<TViewModel>();
        }

        //only admin
        public void ApproveSchool(AppUser manager)
        {
            var roles = this.UserManager.GetRolesAsync(manager).GetAwaiter().GetResult();
            if (!roles.Contains("School"))
            {
                return;
            }

            var school = new School()
            {
                Manager = manager
            };

            this.schoolRepository.AddAsync(school).GetAwaiter().GetResult();
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        //only admin
        public int Create(CreateSchoolInputModel model)
        {
            //todo check manager is in role school 
            var school = new School()
            {
                ManagerId = model.ManagerId,
                OfficeAddress = model.OfficeAddress,
                TradeMark = model.TradeMark,
                Phone = model.Phone
            };

            this.schoolRepository.AddAsync(school).GetAwaiter().GetResult();
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return school.Id;
        }

        public void Delete(int id)
        {
            //todo 
            //change school isActive to false
            var school = this.GetSchoolById(id);
            school.IsActive = false;
            this.schoolRepository.SaveChangesAsync().GetAwaiter().GetResult();

            //change manager usertype to customer and remove user from role school and isApproved set to false=> ONLY with admin rights or approvement
            //do this in controller and call account service
        }

        public int Edit(EditSchoolInputModel model)
        {
            var school = this.GetSchoolById(model.Id);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);

            if (!this.HasRightsToEditOrDelete(model.Id, username))
            {
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            school.OfficeAddress = model.OfficeAddress;
            school.TradeMark = model.TradeMark;
            school.Phone = model.Phone;

            this.schoolRepository.Update(school);
            return school.Id;
        }

        public int ChangeManager(int schoolId, AppUser newManager)
        {
            var school = this.GetSchoolById(schoolId);

            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var hasRights = roles.Any(x => x == "Admin");

            if (!hasRights)
            {
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            school.ManagerId = newManager.Id;

            this.schoolRepository.Update(school);
            return school.Id;
        }

        public TViewModel GetSchoolById<TViewModel>(int id)
        {
            var school = this.schoolRepository
                .All()
                .Where(s=>s.Id==id)
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

        private School GetSchoolByManagerName(string username)
        {
            var school = this.schoolRepository.All()
                             .FirstOrDefault(x => x.Manager.UserName == username);

            if (school == null)
            {
                throw new ArgumentException("No school with this manager name in db");
            }

            return school;
        }

        private bool HasRightsToEditOrDelete(int schoolId, string username)
        {
            var school = this.GetSchoolById(schoolId);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and car for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var hasRights = roles.Any(x => x == "Admin");
            var isManager = username == school.Manager.UserName;

            return isManager || hasRights;
        }
    }
}
