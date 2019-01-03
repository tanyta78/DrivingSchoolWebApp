namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Models.Enums;
    using Models.Exam;

    public interface IExamService
    {
        IEnumerable<Exam> All();

        IEnumerable<TViewModel> All<TViewModel>();

        TViewModel GetExamById<TViewModel>(int examId);

        IEnumerable<TViewModel> GetExamsByCustomerId<TViewModel>(int customerId);

        IEnumerable<TViewModel> GetExamsByCourseId<TViewModel>(int courseId);

        IEnumerable<TViewModel> GetExamsBySchoolId<TViewModel>(int schoolId);

        IEnumerable<TViewModel> GetExamsByStatus<TViewModel>(LessonStatus status);

        IEnumerable<TViewModel> GetExamsByType<TViewModel>(ExamType type);
        
        Task<Exam> Create(CreateExamInputModel model);

        Task<Exam> ChangeStatus(int id,LessonStatus newStatus);

        Task<Exam> CancelExam (int id);
        
        Task<Exam> Delete(int id);
    }
}
