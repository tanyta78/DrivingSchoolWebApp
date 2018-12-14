namespace DrivingSchoolWebApp.Services.Models.School
{
    using System.Collections.Generic;

    public class SchoolListViewModel
    {
        public IEnumerable<SchoolViewModel> ActiveSchools { get; set; } = new HashSet<SchoolViewModel>();
    }
}
