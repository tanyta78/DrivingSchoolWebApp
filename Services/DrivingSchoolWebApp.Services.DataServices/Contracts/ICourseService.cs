namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Models.Enums;
    using Models.Course;

    public interface ICourseService
    {
        IEnumerable<Course> All();

        TViewModel GetCourseById<TViewModel>(int courseId);

        IEnumerable<TViewModel> GetCoursesByTrainerId<TViewModel>(int trainerId);

        IEnumerable<TViewModel> GetCoursesByCarId<TViewModel>(int carId);
        
        IEnumerable<TViewModel> GetCoursesBySchoolId<TViewModel>(int schoolId);

        IEnumerable<TViewModel> GetCoursesByCategory<TViewModel>(Category category);
        
        Task<Course> Create(CreateCourseInputModel model);

        Task<Course> Edit(EditCourseInputModel model);

        Task Delete(int id);
    }
}
