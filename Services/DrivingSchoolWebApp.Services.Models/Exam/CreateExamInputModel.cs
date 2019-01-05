namespace DrivingSchoolWebApp.Services.Models.Exam
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CreateExamInputModel:IMapTo<Exam>,IHaveCustomMappings
    {
        public int CustomerId { get; set; }
        
        public int CourseId { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        [RequiredEnum]
        public LessonStatus Status { get; set; }

        [RequiredEnum]
        public ExamType Type { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateExamInputModel, Exam>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.Customer, x => x.Ignore())
                .ForMember(dest => dest.Course, x => x.Ignore())
                ;
        }
    }
}
