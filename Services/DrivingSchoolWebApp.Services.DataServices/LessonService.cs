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
    using Models.Lesson;

    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> lessonRepository;

        public LessonService(IRepository<Lesson> lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        public IEnumerable<Lesson> All()
        {
            var lessons = this.lessonRepository.All().ToList();

            return lessons;
        }

        public TViewModel GetLessonById<TViewModel>(int lessonId)
        {
            var lesson = this.lessonRepository.All()
                             .Where(x => x.Id == lessonId)
                             .To<TViewModel>().FirstOrDefault();

            if (lesson == null)
            {
                throw new ArgumentException("No course with id in db");
            }

            return lesson;
        }

        public IEnumerable<TViewModel> GetLessonsByCourseIdAndCustomerId<TViewModel>(int customerId, int courseId)
        {
            var lessons = this.lessonRepository.All().Where(x => x.Order.CustomerId == customerId && x.Order.CourseId == courseId).ProjectTo<TViewModel>().ToList();

            return lessons;
        }

        public IEnumerable<TViewModel> GetLessonsByOrderId<TViewModel>(int orderId)
        {
            var lessons = this.lessonRepository.All().Where(x => x.OrderId == orderId).ProjectTo<TViewModel>().ToList();

            return lessons;
        }

        public IEnumerable<TViewModel> GetLessonsByCustomerId<TViewModel>(int customerId)
        {
            var lessons = this.lessonRepository.All().Where(x => x.Order.CustomerId == customerId).ProjectTo<TViewModel>().ToList();

            return lessons;
        }

        public IEnumerable<TViewModel> GetLessonsByTrainerId<TViewModel>(int trainerId)
        {
            var lessons = this.lessonRepository.All().Where(x => x.Order.Course.TrainerId == trainerId).ProjectTo<TViewModel>().ToList();

            return lessons;
        }

        public async Task<Lesson> Create(CreateLessonInputModel model)
        {
            var lesson = new Lesson()
            {
                OrderId = model.OrderId,
                Description = model.Description,
                Subject = model.Subject,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                IsFullDay = model.IsFullDay,
                ThemeColor = model.ThemeColor
            };

            await this.lessonRepository.AddAsync(lesson);
            await this.lessonRepository.SaveChangesAsync();

            return lesson;
        }

        public async Task<Lesson> Edit(EditLessonInputModel model)
        {
            var lesson = this.GetLessonById(model.Id);

            lesson.Status = model.Status;
            lesson.StartTime = model.StartTime;
            lesson.EndTime = model.EndTime;
            lesson.ThemeColor = model.ThemeColor;
            lesson.IsFullDay = model.IsFullDay;
            lesson.Description = model.Description;

            this.lessonRepository.Update(lesson);
            await this.lessonRepository.SaveChangesAsync();

            return lesson;
        }

        public async Task<Lesson> Delete(int id)
        {
            var lesson = this.GetLessonById(id);

            this.lessonRepository.Delete(lesson);
            await this.lessonRepository.SaveChangesAsync();

            return lesson;
        }

        private Lesson GetLessonById(int lessonId)
        {
            var lesson = this.lessonRepository
                .All()
                .FirstOrDefault(x => x.Id == lessonId);

            if (lesson == null)
            {
                throw new ArgumentException("No course with id in db");
            }

            return lesson;
        }

        public int Save(FullCalendarInputModel model)
        {

            if (model.Id != 0)
            {
                var lessonEditModel = new EditLessonInputModel()
                {
                    Id = model.Id,
                    Description = model.Description,
                    EndTime = model.EndTime,
                    IsFullDay = model.IsFullDay,
                    StartTime = model.StartTime,
                    ThemeColor = model.ThemeColor
                };
                var editedLesson = this.Edit(lessonEditModel).GetAwaiter().GetResult();
                return editedLesson.Id;
            }
            else
            {
                var lessonCreateModel = new CreateLessonInputModel()
                {
                    Description = model.Description,
                    EndTime = model.EndTime,
                    IsFullDay = model.IsFullDay,
                    OrderId = model.OrderId,
                    StartTime = model.StartTime,
                    Subject = model.Subject,
                    ThemeColor = model.ThemeColor
                };
                var lesson = this.Create(lessonCreateModel).GetAwaiter().GetResult();

                return lesson.Id;
            }


        }
    }
}