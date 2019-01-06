namespace DrivingSchoolWebApp.Services.Models.Car
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateCarInputModel : IMapFrom<Car>,IHaveCustomMappings
    {
        [Required(ErrorMessage = CarModelConstants.RequiredCarModel)]
        [RegularExpression(CarModelConstants.RegexForValidationCarModel, ErrorMessage =
            CarModelConstants.ErrMsgCarModel)]
        [MinLength(CarModelConstants.CarModelMinLength,ErrorMessage =ErrorMessageConstants.MinLength)]
        [MaxLength(CarModelConstants.CarModelMaxLength,ErrorMessage =ErrorMessageConstants.MaxLength)]
        public string CarModel { get; set; }

        [Required(ErrorMessage = CarModelConstants.RequiredMake)]
        public string Make { get; set; }


        public Transmission Transmission { get; set; }

        public int OwnerId { get; set; }

        public bool InUse { get; set; } = true;

        [Required(ErrorMessage = CarModelConstants.RequiredVin)]
        [StringLength(CarModelConstants.VINLength, ErrorMessage = CarModelConstants.ErrMsgVin, MinimumLength = CarModelConstants.VINLength)]
        public string VIN { get; set; }

        [IgnoreMap]
        [Required(ErrorMessage = CarModelConstants.RequiredImageUrl)]
        public IFormFile CarImage { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateCarInputModel, Car>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.CoursesInvolved, x => x.Ignore())
                .ForMember(dest => dest.ImageUrl, x => x.Ignore())
                .ForMember(dest => dest.Owner, x => x.Ignore());

        }
    }
}
