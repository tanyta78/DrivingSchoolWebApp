namespace DrivingSchoolWebApp.Services.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using Constants;
    using Data.Models;
    using Mapping;

    public class LoginViewModel:IMapFrom<AppUser>
    {
        [Required(ErrorMessage = AppUserModelConstants.RequiredUsername)]
        [RegularExpression(AppUserModelConstants.RegexForValidationNicknameOrUsername, ErrorMessage =
            AppUserModelConstants.ErrMsgUsernameRGX)]
        [StringLength(AppUserModelConstants.UserModelUsernameMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgUsernameLength, MinimumLength = AppUserModelConstants.UserModelUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = AppUserModelConstants.RequiredPass)]
        [StringLength(AppUserModelConstants.UserModelPassMaxLength, ErrorMessage = AppUserModelConstants.ErrMsgPassLength, MinimumLength = AppUserModelConstants.UserModelPassMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
