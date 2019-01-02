namespace DrivingSchoolWebApp.Services.Models.Exam
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class AllExamsViewModel : IMapFrom<Exam>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public int CourseId { get; set; }

        public string CourseInfo { get; set; }

        public string CourseSchoolManagerUserId { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public LessonStatus Status { get; set; }

        public ExamType Type { get; set; }


        public void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Exam, AllExamsViewModel>()
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.User.FirstName + " " + src.Customer.User.LastName))
                .ForMember(dest => dest.CourseInfo, opt => opt.MapFrom(src => src.Course.Trainer.User.Nickname + " as trainer" + src.Course.School.TradeMark + " school" + src.Course.Car.CarModel + " " + src.Course.Car.CarModel + " " + src.Course.Car.Make + " " + src.Course.Car.Transmission));
        }
    }
}
