namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Models.Feedback;

    public interface IFeedbackService
    {
        IEnumerable<Feedback> All();

        IEnumerable<TViewModel> All<TViewModel>();

        TViewModel GetFeedbackById<TViewModel>(int feedbackId);

        IEnumerable<TViewModel> GetFeedbacksByCustomerId<TViewModel>(int customerId);

        IEnumerable<TViewModel> GetFeedbacksByCourseId<TViewModel>(int courseId);

        IEnumerable<TViewModel> GetFeedbacksBySchoolId<TViewModel>(int schoolId);
        
        Task<Feedback> Create(CreateFeedbackInputModel model);

       Task Delete(int id);
    }
}
