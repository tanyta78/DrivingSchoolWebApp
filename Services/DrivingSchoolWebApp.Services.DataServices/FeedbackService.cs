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
    using Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Models.Feedback;

    public class FeedbackService : BaseService, IFeedbackService
    {
        private readonly IRepository<Feedback> feedbackRepository;

        public FeedbackService(IRepository<Feedback> feedbackRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
        {
            this.feedbackRepository = feedbackRepository;
        }

        public IEnumerable<Feedback> All()
        {
            return this.feedbackRepository.All().ToList();
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
            var feedback = this.Mapper.Map<Feedback>(model);

            await this.feedbackRepository.AddAsync(feedback);
            await this.feedbackRepository.SaveChangesAsync();

            return feedback;
        }

        public async Task Delete(int id)
        {
            var feedback = this.GetFeedbackById<Feedback>(id);

            if (!this.HasRightsToEditOrDelete(id))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }
            this.feedbackRepository.Delete(feedback);
            await this.feedbackRepository.SaveChangesAsync();
        }

        private bool HasRightsToEditOrDelete(int feedbackId)
        {
            var feedback = this.GetFeedbackById<Feedback>(feedbackId);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and feedback for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var isAdmin = roles.Any(x => x == "Admin");
            var isCreator = username == feedback.Customer.User.UserName;

            return isCreator || isAdmin;
        }
    }
}
