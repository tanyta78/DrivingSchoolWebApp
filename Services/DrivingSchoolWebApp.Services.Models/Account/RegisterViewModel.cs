namespace DrivingSchoolWebApp.Services.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class RegisterViewModel:IMapFrom<AppUser>
    {
        [Required(ErrorMessage = "You must enter an username")]
        [RegularExpression(@"[\w-.^~]+", ErrorMessage =
            "May only contain alphanumeric characters ,dashes and underscores,dots, asterisks and tildes")]
        [StringLength(50, ErrorMessage = "Username length must between 3 and 50 characters", MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter an email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter a first name")]
        [StringLength(20, ErrorMessage = "First name length must between 2 and 20 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a last name")]
        [StringLength(20, ErrorMessage = "Last name length must between 2 and 20 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter a password")]
        [StringLength(50, ErrorMessage = "Password length must between 5 and 50 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You must enter a nickname")]
        [StringLength(8, ErrorMessage = "Nickname length must between 2 and 8 characters", MinimumLength = 2)]
        public string Nickname { get; set; }

        //todo add validation for date
        public DateTime BirthDate { get; set; }

        //todo add validation for phone
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "You must enter an address")]
        [StringLength(50, ErrorMessage = "Address length must between 10 and 50 characters", MinimumLength = 10)]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must enter a user type")]
        public UserType UserType { get; set; }
    }
}
