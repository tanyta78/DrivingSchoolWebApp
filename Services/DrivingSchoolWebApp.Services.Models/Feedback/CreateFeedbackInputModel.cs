namespace DrivingSchoolWebApp.Services.Models.Feedback
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Mapping;

    public class CreateFeedbackInputModel : IMapFrom<Feedback>, IHaveCustomMappings
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        [Required(ErrorMessage = FeedbackModelConstants.RequiredContent)]
        [StringLength(FeedbackModelConstants.ContentMaxLength)]
        [RegularExpression(FeedbackModelConstants.RegexForValidateContent, ErrorMessage = FeedbackModelConstants.ErrMsgContent)]
        public string Content { get; set; }

        [Required(ErrorMessage = FeedbackModelConstants.RequiredRating)]
        [Range(FeedbackModelConstants.RatingMin, FeedbackModelConstants.RatingMax,ErrorMessage = FeedbackModelConstants.ErrMsgRating)]
        public int Rating { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateFeedbackInputModel, Feedback>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.Customer, x => x.Ignore())
                .ForMember(dest => dest.Course, x => x.Ignore());
        }
    }
}
