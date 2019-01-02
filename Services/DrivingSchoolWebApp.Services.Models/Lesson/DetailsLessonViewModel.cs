namespace DrivingSchoolWebApp.Services.Models.Lesson
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Mapping;

    public class DetailsLessonViewModel : IMapFrom<Lesson>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string OrderCustomerFullName { get; set; }

        public string OrderCourseTrainerNickName { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime StartTime { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        public string Description { get; set; }

        public string Subject { get; set; }

        public void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Lesson, DetailsLessonViewModel>()
               .ForMember(dest => dest.OrderCustomerFullName, opt => opt.MapFrom(src => src.Order.Customer.User.FirstName + " " + src.Order.Customer.User.LastName));
        }
    }
}