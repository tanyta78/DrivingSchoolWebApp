namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Models.Feedback;

    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> feedbackRepository;

        public FeedbackService(IRepository<Feedback> feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        public IEnumerable<Feedback> All()
        {
            return this.feedbackRepository.All().ToList();
        }

        public IEnumerable<TViewModel> All<TViewModel>()
        {
            var feedbacks = this.feedbackRepository.All().ProjectTo<TViewModel>().ToList();

            return feedbacks;
        }

        public TViewModel GetFeedbackById<TViewModel>(int feedbackId)
        {
            var feedback = this.feedbackRepository.All().Where(x => x.Id == feedbackId)
                .To<TViewModel>().FirstOrDefault();

            if (feedback == null)
            {
                throw new ArgumentException("No feedback with id in db");
            }

            return feedback;
        }

        public IEnumerable<TViewModel> GetFeedbacksByCustomerId<TViewModel>(int customerId)
        {
            var feedbacks = this.feedbackRepository.All().Where(x => x.CustomerId == customerId).ProjectTo<TViewModel>().ToList();

            return feedbacks;
        }

        public IEnumerable<TViewModel> GetFeedbacksByCourseId<TViewModel>(int courseId)
        {
            var feedbacks = this.feedbackRepository.All().Where(x => x.CourseId == courseId).ProjectTo<TViewModel>().ToList();

            return feedbacks;
        }

        public IEnumerable<TViewModel> GetFeedbacksBySchoolId<TViewModel>(int schoolId)
        {
            var feedbacks = this.feedbackRepository.All().Where(x => x.Course.SchoolId == schoolId).ProjectTo<TViewModel>().ToList();

            return feedbacks;
        }

        public async Task<Feedback> Create(CreateFeedbackInputModel model)
        {
            var feedback=Mapper.Map<Feedback>(model);

            await this.feedbackRepository.AddAsync(feedback);
            await this.feedbackRepository.SaveChangesAsync();

            return feedback;
        }

        public async Task<Feedback> Delete(int id)
        {
            var feedback = this.GetFeedbackById(id);

            this.feedbackRepository.Delete(feedback);
            await this.feedbackRepository.SaveChangesAsync();

            return feedback;
        }



        private Feedback GetFeedbackById(int feedbackId)
        {
            var feedback = this.feedbackRepository.All()
                .FirstOrDefault(x => x.Id == feedbackId);

            if (feedback == null)
            {
                throw new ArgumentException("No feedback with id in db");
            }

            return feedback;
        }
    }
}
