namespace DrivingSchoolWebApp.Services.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CreateCourseInputModel : IMapFrom<Course>, IHaveCustomMappings
    {
        [RequiredEnum]
        public Category Category { get; set; }

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

        public int SchoolId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateCourseInputModel, Course>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.AllFeedbacks, x => x.Ignore())
                .ForMember(dest => dest.Car, x => x.Ignore())
                .ForMember(dest => dest.Trainer, x => x.Ignore())
                .ForMember(dest => dest.School, x => x.Ignore())
                .ForMember(dest => dest.ExamsTaken, x => x.Ignore())
                .ForMember(dest => dest.Students, x => x.Ignore())
                .ForMember(dest => dest.Info, x => x.Ignore())
                .ForMember(dest => dest.IsFinished, x => x.Ignore());
        }
    }
}
