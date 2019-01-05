namespace DrivingSchoolWebApp.Services.Models.School
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Mapping;

    public class CreateSchoolInputModel : IMapFrom<School>, IHaveCustomMappings
    {
        public string ManagerId { get; set; }

        public AppUser Manager { get; set; }

        [Required(ErrorMessage = SchoolModelConstants.RequiredTradeMark)]
        [RegularExpression(SchoolModelConstants.RegexForValidationSchoolModel, ErrorMessage =
            SchoolModelConstants.ErrMsgSchoolModel)]
        [StringLength(SchoolModelConstants.TradeMarkMaxLength, ErrorMessage = SchoolModelConstants.ErrMsgTrademarkLength, MinimumLength = SchoolModelConstants.TradeMarkMinLength)]
        public string TradeMark { get; set; }

        [Required(ErrorMessage = SchoolModelConstants.RequiredPhone)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = SchoolModelConstants.RequiredOfficeAddress)]
        [RegularExpression(SchoolModelConstants.RegexForValidationSchoolModel, ErrorMessage =
            SchoolModelConstants.ErrMsgSchoolModel)]
        [StringLength(SchoolModelConstants.OfficeAddressMaxLength, ErrorMessage = SchoolModelConstants.ErrMsgOfficeAddressLength, MinimumLength = SchoolModelConstants.OfficeAddressMinLength)]
        public string OfficeAddress { get; set; }

        public bool IsActive { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateSchoolInputModel, School>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.Trainers, x => x.Ignore())
                .ForMember(dest => dest.OwnedCars, x => x.Ignore())
                .ForMember(dest => dest.CoursesOffered, x => x.Ignore());

        }
    }
}