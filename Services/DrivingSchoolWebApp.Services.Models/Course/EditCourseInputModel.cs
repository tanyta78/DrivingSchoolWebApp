namespace DrivingSchoolWebApp.Services.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Mapping;

    public class EditCourseInputModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = CourseModelConstants.RequiredPrice)]
        [Range(typeof(decimal), CourseModelConstants.MinPrice, CourseModelConstants.MaxPrice, ErrorMessage = CourseModelConstants.ErrMsgCoursePrice)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = CourseModelConstants.RequiredDescription)]
        [StringLength(CourseModelConstants.DescriptionMaxLength)]
        [RegularExpression(CourseModelConstants.RegexForValidationDescription, ErrorMessage = CourseModelConstants.ErrMsgDescription)]
        public string Description { get; set; }

        [Required(ErrorMessage = CourseModelConstants.RequiredMinimumLessonsCount)]
        [Range(CourseModelConstants.MinimumLessonsCountMin, CourseModelConstants.MinimumLessonsCountMax)]
        public int MinimumLessonsCount { get; set; }

        public int TrainerId { get; set; }

        public int CarId { get; set; }

        [IgnoreMap]
        public string Username { get; set; }
    }
}
