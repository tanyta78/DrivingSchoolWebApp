namespace DrivingSchoolWebApp.Services.Models.Payment
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Constants;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CreatePaymentInputModel : IMapFrom<Payment>, IHaveCustomMappings
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = PaymentModelConstants.RequiredPrice)]
        [Range(typeof(decimal), PaymentModelConstants.MinPrice, PaymentModelConstants.MaxPrice, ErrorMessage = PaymentModelConstants.ErrMsgCoursePrice)]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [RequiredEnum]
        public PaymentMethod PaymentMethod { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreatePaymentInputModel, Payment>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.Order, x => x.Ignore())
               .ForMember(dest => dest.PaidOn, x => x.Ignore());
        }
    }
}
