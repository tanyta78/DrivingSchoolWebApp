namespace DrivingSchoolWebApp.Services.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;
    using Data.Models;
    using Mapping;

    public class EditUserProfileInputModel : IMapFrom<AppUser>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredFirstName)]
        [StringLength(AppUserModelConstants.UserModelNameMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgNameLength, MinimumLength = AppUserModelConstants.UserModelNameMinLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredLastName)]
        [StringLength(AppUserModelConstants.UserModelNameMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgNameLength, MinimumLength = AppUserModelConstants.UserModelNameMinLength)]
        public string LastName { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredNickname)]
        [StringLength(AppUserModelConstants.UserModelNickNameMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgNickNameLength, MinimumLength = AppUserModelConstants.UserModelNickNameMinLength)]
        public string Nickname { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredBirthDate)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredPhone)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredAddress)]
        [StringLength(AppUserModelConstants.UserModelAddressMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgAddrLength, MinimumLength = AppUserModelConstants.UserModelAddressMinLength)]
        public string Address { get; set; }

    }
}
