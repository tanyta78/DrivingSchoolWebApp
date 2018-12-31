namespace DrivingSchoolWebApp.Services.Models.Certificate
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Mapping;

    public class DetailsCertificateViewModel : IMapFrom<Certificate>, IHaveCustomMappings
    {

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public int CourseId { get; set; }

        public string CourseInfo { get; set; }

        public DateTime IssueDate { get; set; }


        public void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Certificate, DetailsCertificateViewModel>()
                .ForMember(dest => dest.CustomerFullName,
                    opt => opt.MapFrom(src => src.Customer.User.FirstName + " " + src.Customer.User.LastName))
                .ForMember(dest => dest.CourseInfo,
                    opt => opt.MapFrom(src =>
                        src.Course.Trainer.User.Nickname + " as trainer" + src.Course.School.TradeMark + " school" +
                        src.Course.Car.CarModel + " " + src.Course.Car.CarModel + " " + src.Course.Car.Make + " " +
                        src.Course.Car.Transmission));
        }
    }
}
