namespace DrivingSchoolWebApp.Services.Models.Lesson
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class EditLessonInputModel:IMapFrom<Lesson>,IMapFrom<FullCalendarInputModel>,IHaveCustomMappings
    {
        public int Id { get; set; }
        
        [RequiredEnum]
        public LessonStatus Status { get; set; }
      
        [DataType(DataType.DateTime)]
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

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<EditLessonInputModel, Lesson>()
                .ForMember(dest => dest.OrderId, x => x.Ignore())
                .ForMember(dest => dest.Order, x => x.Ignore())
                .ForMember(dest => dest.Subject, x => x.Ignore());
        }
    }
}