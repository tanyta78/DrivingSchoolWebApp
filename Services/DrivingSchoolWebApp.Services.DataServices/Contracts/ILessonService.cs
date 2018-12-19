namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Models.Lesson;

    public interface ILessonService
    {
        IEnumerable<Lesson> All();

        TViewModel GetLessonById<TViewModel>(int lessonId);

        Task<Lesson> Create(CreateLessonInputModel model);

        Task<Lesson> Edit(EditLessonInputModel model);
        
        Task Delete(int id);

        IEnumerable<TViewModel> GetLessonsByCourseIdAndCustomerId<TViewModel>(int customerId, int courseId);

        IEnumerable<TViewModel> GetLessonsByTrainerId<TViewModel>(int trainerId);

    }
}
