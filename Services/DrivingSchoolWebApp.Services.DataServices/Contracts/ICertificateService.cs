namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Models.Certificate;

    public interface ICertificateService
    {
        IEnumerable<Certificate> All();

        IEnumerable<TViewModel> All<TViewModel>();

        TViewModel GetCertificateById<TViewModel>(int certificateId);

        IEnumerable<TViewModel> GetCertificatesByCustomerId<TViewModel>(int customerId);

        IEnumerable<TViewModel> GetCertificatesByCourseId<TViewModel>(int courseId);

        IEnumerable<TViewModel> GetCertificatesBySchoolId<TViewModel>(int schoolId);

        Task<Certificate> Create(CreateCertificateInputModel model);

        Task Delete(int id, string username);
    }
}
