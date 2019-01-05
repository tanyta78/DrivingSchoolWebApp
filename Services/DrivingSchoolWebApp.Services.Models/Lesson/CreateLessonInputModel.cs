namespace DrivingSchoolWebApp.Services.Models.Lesson
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Mapping;

    public class CreateLessonInputModel : IMapFrom<Lesson>, IMapFrom<FullCalendarInputModel>, IMapTo<Lesson>,IHaveCustomMappings
    {
        public int OrderId { get; set; }

        [DataType(DataType.DateTime)]
        [GreaterThan("StartTime",ErrorMessage = "End time must be greater than start time")]
        public DateTime? EndTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        [Required(ErrorMessage = LessonModelConstants.RequiredContent)]
        [StringLength(LessonModelConstants.ContentMaxLength)]
        [RegularExpression(LessonModelConstants.RegexForValidateContent, ErrorMessage = LessonModelConstants.ErrMsgContent)]
        public string Description { get; set; }

        [Required(ErrorMessage = LessonModelConstants.RequiredContent)]
        [StringLength(LessonModelConstants.ContentMaxLength)]
        [RegularExpression(LessonModelConstants.RegexForValidateContent, ErrorMessage = LessonModelConstants.ErrMsgContent)]
        public string Subject { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateLessonInputModel, Lesson>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.Order, x => x.Ignore())
                .ForMember(dest => dest.Status, x => x.Ignore());
           
        }
    }
}