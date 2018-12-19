﻿namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Models.Lesson;

    public class LessonService :BaseService, ILessonService
    {
        private readonly IRepository<Lesson> lessonRepository;

        public LessonService(IRepository<Lesson> lessonRepository,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
        {
            this.lessonRepository = lessonRepository;
        }

        public IEnumerable<Lesson> All()
        {
            var lessons = this.lessonRepository.All();

            return lessons;
        }

        public TViewModel GetLessonById<TViewModel>(int lessonId)
        {
            var lesson = this.lessonRepository.All().Where(x => x.Id == lessonId)
                             .To<TViewModel>().FirstOrDefault();

            if (lesson == null)
            {
                throw new ArgumentException("No course with id in db");
            }

            return lesson;
        }

        public IEnumerable<TViewModel> GetLessonsByCourseIdAndCustomerId<TViewModel>(int customerId, int courseId)
        {
            var lessons = this.lessonRepository.All().Where(x => x.CustomerId== customerId && x.CourseId==courseId).ProjectTo<TViewModel>().ToList();
            
            return lessons;
        }

        public IEnumerable<TViewModel> GetLessonsByTrainerId<TViewModel>(int trainerId)
        {
            var lessons = this.lessonRepository.All().Where(x => x.Course.TrainerId==trainerId).ProjectTo<TViewModel>().ToList();
            
            return lessons;
        }

        public async Task<Lesson> Create(CreateLessonInputModel model)
        {
            var lesson = this.Mapper.Map<Lesson>(model);
            
            await this.lessonRepository.AddAsync(lesson);
            await this.lessonRepository.SaveChangesAsync();

            return lesson;
        }

        public async Task<Lesson> Edit(EditLessonInputModel model)
        {
            var lesson = this.GetLessonById<Lesson>(model.Id);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);

            if (!this.HasRightsToEditOrDelete(model.Id, username))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            lesson.Status = model.Status;
            lesson.EndTime = model.EndTime;
            lesson.ThemeColor = model.ThemeColor;
            lesson.IsFullDay = model.IsFullDay;
            lesson.Description = model.Description;

            this.lessonRepository.Update(lesson);
            await this.lessonRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var lesson = this.GetLessonById<Lesson>(id);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);

            if (!this.HasRightsToEditOrDelete(id, username))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            this.lessonRepository.Delete(lesson);
            await this.lessonRepository.SaveChangesAsync();
        }

        private bool HasRightsToEditOrDelete(int lessonId, string username)
        {
            var lesson = this.GetLessonById<Lesson>(lessonId);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and lesson for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var hasRights = roles.Any(x => x == "Admin");
            var isCreator = username == lesson.Course.School.Manager.UserName;

            return isCreator || hasRights;
        }


    }
}