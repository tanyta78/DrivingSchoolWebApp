﻿namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using DrivingSchoolWebApp.Data.Models.Enums;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.Course;

    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public IEnumerable<Course> All()
        {
            return this.courseRepository.All().Where(c => (c.IsFinished == false)&&(c.School.IsActive)&&(c.School.Manager.IsApproved)).ToList();
        }

        public async Task<Course> Create(CreateCourseInputModel model)
        {
           var course=Mapper.Map<Course>(model);

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();

            return course;
        }

        public async Task<Course> Delete(int id)
        {
            var course = this.GetCourseById(id);
           
            this.courseRepository.Delete(course);
            await this.courseRepository.SaveChangesAsync();

            return course;
        }

        public async Task<Course> Edit(EditCourseInputModel model)
        {
            var course = this.GetCourseById(model.Id);

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
            var courses = this.courseRepository.All()
                .Where(c => (c.IsFinished == false)&&(c.School.IsActive)&&(c.School.Manager.IsApproved))
                .ProjectTo<TViewModel>().ToList();
            return courses;
        }

        public IEnumerable<TViewModel> GetCoursesByCarId<TViewModel>(int carId)
        {
            var courses = this.courseRepository.All().Where(c =>(c.IsFinished == false)&&(c.School.IsActive)&&(c.School.Manager.IsApproved)&&(c.CarId == carId)).ProjectTo<TViewModel>().ToList();

            return courses;
        }

        public IEnumerable<TViewModel> GetCoursesByCategory<TViewModel>(Category category)
        {
            var courses = this.courseRepository.All().Where(c => (c.Category == category)&&(c.IsFinished == false)&&(c.School.IsActive)&&(c.School.Manager.IsApproved)).ProjectTo<TViewModel>().ToList();

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
