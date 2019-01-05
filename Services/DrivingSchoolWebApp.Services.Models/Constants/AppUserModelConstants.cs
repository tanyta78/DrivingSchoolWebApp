namespace DrivingSchoolWebApp.Services.Models.Constants
{

    public static class AppUserModelConstants
    {
        public const int UserModelUsernameMaxLength = 50;
        public const int UserModelUsernameMinLength = 3;
        public const string ErrMsgUsernameLength = "Username length must between 3 and 50 characters";

        public const int UserModelNameMaxLength = 20;
        public const int UserModelNameMinLength = 2;
        public const string ErrMsgNameLength = "Name length must between 2 and 20 characters";

        public const int UserModelPassMaxLength = 50;
        public const int UserModelPassMinLength = 5;
        public const string ErrMsgPassLength = "Password length must between 5 and 50 characters";

        public const int UserModelNickNameMaxLength = 8;
        public const int UserModelNickNameMinLength = 2;
        public const string ErrMsgNickNameLength = "NickName length must between 2 and 8 characters";

        public const int UserModelAddressMaxLength = 50;
        public const int UserModelAddressMinLength = 10;
        public const string ErrMsgAddrLength = "Address length must between 10 and 50 characters";

        public const string ErrMsgUsernameRGX = "May only contain alphanumeric characters, dashes and underscores, dots, asterisks and tildes";
        public const string RegexForValidationNicknameOrUsername = @"[\w-.^~]+";

        public const string RequiredEmail = "You must enter an email";
        public const string RequiredFirstName = "You must enter a first name";
        public const string RequiredLastName = "You must enter a last name";
        public const string RequiredUsername = "You must enter an username";
        public const string RequiredPass = "You must enter a password";
        public const string RequiredConfirmPass = "You must confirm your password";
        public const string RequiredNickname = "You must enter a nickname";
        public const string RequiredAddress = "You must enter an address";
        public const string RequiredUserType = "You must enter a user type";
        public const string RequiredBirthDate = "You must enter your birth date";
        public const string RequiredPhone = "You must enter a phone number";

    }
}
