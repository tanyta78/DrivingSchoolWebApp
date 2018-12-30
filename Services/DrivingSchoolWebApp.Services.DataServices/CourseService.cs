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
    using DrivingSchoolWebApp.Data.Models;
    using DrivingSchoolWebApp.Data.Models.Enums;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models.Course;

    public class CourseService : BaseService, ICourseService
    {
        private readonly IRepository<Course> courseRepository;

        public CourseService(IRepository<Course> courseRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
        {
            this.courseRepository = courseRepository;
        }

        public IEnumerable<Course> All()
        {
            return this.courseRepository.All().Where(c => c.IsFinished == false).ToList();
        }

        public async Task<Course> Create(CreateCourseInputModel model)
        {
            var course = new Course
            {
                Category = model.Category,
                Description = model.Description,
                MinimumLessonsCount = model.MinimumLessonsCount,
                Price = model.Price,
                TrainerId = model.TrainerId,
                CarId = model.CarId,
                SchoolId = model.SchoolId
            };

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();

            return course;
        }

        public async Task Delete(int id)
        {
            var course = this.GetCourseById(id);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);

            if (!this.HasRightsToEditOrDelete(id, username))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            this.courseRepository.Delete(course);
            await this.courseRepository.SaveChangesAsync();
        }

        public async Task<Course> Edit(EditCourseInputModel model)
        {
            //todo check model validation in controller?!?
            //todo change price, description, min lessons,trainerId, carId
            var course = this.GetCourseById(model.Id);
          
            if (!this.HasRightsToEditOrDelete(model.Id, model.Username))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            course.CarId = model.CarId;
            course.TrainerId = model.TrainerId;
            course.Price = model.Price;
            course.Description = model.Description;
            course.MinimumLessonsCount = model.MinimumLessonsCount;

            this.courseRepository.Update(course);
            await this.courseRepository.SaveChangesAsync();

            return course;
        }

        public TViewModel GetCourseById<TViewModel>(int courseId)
        {
            var course = this.courseRepository.All()
                .Include(c => c.AllFeedbacks)
                .Include(c => c.ExamsTaken)
                .Include(c => c.Students)
                .Where(x => x.Id == courseId)
                .To<TViewModel>().FirstOrDefault();

            if (course == null)
            {
                throw new ArgumentException("No course with id in db");
            }

            return course;
        }

        public IEnumerable<TViewModel> GetAllCourses<TViewModel>()
        {
            var courses = this.courseRepository.All().ProjectTo<TViewModel>().ToList();
            return courses;
        }

        public IEnumerable<TViewModel> GetCoursesByCarId<TViewModel>(int carId)
        {
            var courses = this.courseRepository.All().Where(x => x.CarId == carId).ProjectTo<TViewModel>().ToList();

            return courses;
        }

        public IEnumerable<TViewModel> GetCoursesByCategory<TViewModel>(Category category)
        {
            var courses = this.courseRepository.All().Where(x => x.Category == category).ProjectTo<TViewModel>().ToList();

            return courses;
        }

        public IEnumerable<TViewModel> GetCoursesBySchoolId<TViewModel>(int schoolId)
        {
            var courses = this.courseRepository.All().Where(x => x.SchoolId == schoolId).ProjectTo<TViewModel>().ToList();

            return courses;
        }

        public IEnumerable<TViewModel> GetCoursesBySchoolIdAndCategory<TViewModel>(int schoolId, Category category)
        {
            var courses = this.courseRepository.All().Where(x => x.SchoolId == schoolId && x.Category == category).ProjectTo<TViewModel>().ToList();

            return courses;
        }

        public IEnumerable<TViewModel> GetCoursesByTrainerId<TViewModel>(int trainerId)
        {
            var courses = this.courseRepository.All().Where(x => x.TrainerId == trainerId).ProjectTo<TViewModel>().ToList();

            return courses;
        }

        private bool HasRightsToEditOrDelete(int courseId, string username)
        {
            var course = this.GetCourseById(courseId);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and car for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var hasRights = roles.Any(x => x == "Admin");
            var isManager = username == course.School.Manager.UserName;

            return isManager || hasRights;
        }

        private Course GetCourseById(int courseId)
        {
            var course = this.courseRepository.All()
                .Include(c => c.AllFeedbacks)
                .Include(c => c.ExamsTaken)
                .Include(c => c.Students)
                .FirstOrDefault(x => x.Id == courseId);

            if (course == null)
            {
                throw new ArgumentException("No course with id in db");
            }

            return course;
        }
    }
}
