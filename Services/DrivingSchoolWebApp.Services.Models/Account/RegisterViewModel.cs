namespace DrivingSchoolWebApp.Services.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class RegisterViewModel : IMapFrom<AppUser>
    {
        [Required(ErrorMessage = AppUserModelConstants.RequiredUsername)]
        [RegularExpression(AppUserModelConstants.RegexForValidationNicknameOrUsername, ErrorMessage =
           AppUserModelConstants.ErrMsgUsernameRGX)]
        [StringLength(AppUserModelConstants.UserModelUsernameMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgUsernameLength, MinimumLength = AppUserModelConstants.UserModelUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredEmail)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredFirstName)]
        [StringLength(AppUserModelConstants.UserModelNameMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgNameLength, MinimumLength = AppUserModelConstants.UserModelNameMinLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredLastName)]
        [StringLength(AppUserModelConstants.UserModelNameMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgNameLength, MinimumLength = AppUserModelConstants.UserModelNameMinLength)]
        public string LastName { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredPass)]
        [StringLength(AppUserModelConstants.UserModelPassMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgPassLength, MinimumLength = AppUserModelConstants.UserModelPassMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredConfirmPass)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredNickname)]
        [StringLength(AppUserModelConstants.UserModelNickNameMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgNickNameLength, MinimumLength = AppUserModelConstants.UserModelNickNameMinLength)]
        public string Nickname { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredBirthDate)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredPhone)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredAddress)]
        [StringLength(AppUserModelConstants.UserModelAddressMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgAddrLength, MinimumLength = AppUserModelConstants.UserModelAddressMinLength)]
        public string Address { get; set; }

        [RequiredEnum(ErrorMessage = AppUserModelConstants.RequiredUserType)]
        public UserType UserType { get; set; }
    }
}
