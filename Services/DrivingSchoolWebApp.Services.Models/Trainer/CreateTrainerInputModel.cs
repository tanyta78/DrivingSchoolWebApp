namespace DrivingSchoolWebApp.Services.Models.Trainer
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Mapping;

    public class CreateTrainerInputModel : IMapFrom<Trainer>,IHaveCustomMappings
    {
        public string UserId { get; set; }

        public int SchoolId { get; set; }

        [Required(ErrorMessage = TrainerModelConstants.RequiredHireDate)]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }


        public void CreateMappings(IMapperConfigurationExpression configuration)
            {
                configuration.CreateMap<CreateTrainerInputModel, Trainer>()
                    .ForMember(dest => dest.Id, x => x.Ignore())
                    .ForMember(dest => dest.User, x => x.Ignore())
                    .ForMember(dest => dest.School, x => x.Ignore())
                    .ForMember(dest => dest.IsAvailable, x => x.Ignore())
                    .ForMember(dest => dest.AvailableStartTime, x => x.Ignore())
                    .ForMember(dest => dest.AvailableLessonDay, x => x.Ignore())
                    .ForMember(dest => dest.CoursesInvolved, x => x.Ignore());
            }
        
    }
}
