﻿namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;
    using Models.Exam;

    public class ExamService : IExamService
    {
        private readonly IRepository<Exam> examRepository;

        public ExamService(IRepository<Exam> examRepository)
        {
            this.examRepository = examRepository;
        }

        public IEnumerable<Exam> All()
        {
            return this.examRepository.All().ToList();
        }

        public IEnumerable<TViewModel> All<TViewModel>()
        {
            var exams = this.examRepository.All().ProjectTo<TViewModel>().ToList();

            return exams;
        }

        public TViewModel GetExamById<TViewModel>(int examId)
        {
            var exam = this.examRepository.All().Where(x => x.Id == examId)
                .To<TViewModel>().FirstOrDefault();

            if (exam == null)
            {
                throw new ArgumentException("No exam with id in db");
            }

            return exam;
        }

        public Exam GetExamById(int examId)
        {
            var exam = this.examRepository.All()
                .FirstOrDefault(x => x.Id == examId);

            if (exam == null)
            {
                throw new ArgumentException("No exam with id in db");
            }

            return exam;
        }

        public IEnumerable<TViewModel> GetExamsByCustomerId<TViewModel>(int customerId)
        {
            var exams = this.examRepository.All().Where(x => x.CustomerId == customerId).ProjectTo<TViewModel>().ToList();

            return exams;
        }

        public IEnumerable<TViewModel> GetExamsByCourseId<TViewModel>(int courseId)
        {
            var exams = this.examRepository.All().Where(x => x.CourseId == courseId).ProjectTo<TViewModel>().ToList();

            return exams;
        }

        public IEnumerable<TViewModel> GetExamsBySchoolId<TViewModel>(int schoolId)
        {
            var exams = this.examRepository.All().Where(x => x.Course.SchoolId == schoolId).ProjectTo<TViewModel>().ToList();

            return exams;
        }

        public IEnumerable<TViewModel> GetExamsByStatus<TViewModel>(LessonStatus status)
        {
            var exams = this.examRepository.All().Where(x => x.Status == status).ProjectTo<TViewModel>().ToList();

            return exams;
        }

        public IEnumerable<TViewModel> GetExamsByType<TViewModel>(ExamType type)
        {
            var exams = this.examRepository.All().Where(x => x.Type == type).ProjectTo<TViewModel>().ToList();

            return exams;
        }

        public async Task<Exam> Create(CreateExamInputModel model)
        {
            var exam = new Exam()
            {
                CourseId = model.CourseId,
                CustomerId = model.CustomerId,
                Date = model.Date,
                Time = model.Time,
                Status = model.Status,
                Type = model.Type
            };

            await this.examRepository.AddAsync(exam);
            await this.examRepository.SaveChangesAsync();

            return exam;
        }

        public async Task<Exam> ChangeStatus(int id, LessonStatus newStatus)
        {
            var exam = this.GetExamById(id);

            //if (!this.HasRightsToEditOrDelete(id, username))
            //{
            //    //todo throw custom error message
            //    throw new OperationCanceledException("You do not have rights for this operation!");
            //}

            exam.Status = newStatus;
            this.examRepository.Update(exam);
            await this.examRepository.SaveChangesAsync();

            return exam;
        }

        public async Task<Exam> CancelExam(int id)
        {
            var exam = this.GetExamById(id);

            //if (!this.HasRightsToEditOrDelete(id, username))
            //{
            //    //todo throw custom error message
            //    throw new OperationCanceledException("You do not have rights for this operation!");
            //}

            exam.Status = LessonStatus.Canceled;
            this.examRepository.Update(exam);
            await this.examRepository.SaveChangesAsync();

            return exam; ;
        }

        public async Task Delete(int id)
        {
            var exam = this.GetExamById(id);

            //if (!this.HasRightsToEditOrDelete(id, username))
            //{
            //    //todo throw custom error message
            //    throw new OperationCanceledException("You do not have rights for this operation!");
            //}
            this.examRepository.Delete(exam);
            await this.examRepository.SaveChangesAsync();
        }

        //private bool HasRightsToEditOrDelete(int examId, string username)
        //{
        //    var exam = this.GetExamById(examId);

        //    var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

        //    //todo check user and car for null; to add include if needed

        //    var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

        //    var isAdmin = roles.Any(x => x == "Admin");
        //    var isCreator = username == exam.Course.School.Manager.UserName;

        //    return isCreator || isAdmin;
        //}
    }
}
