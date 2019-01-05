namespace DrivingSchoolWebApp.Services.Models.Constants
{

    public static class CarModelConstants
    {
        public const int CarModelMaxLength = 20;
        public const int CarModelMinLength = 2;
       
        public const string ErrMsgCarModel = "May only contain alphanumeric characters";
        public const string RegexForValidationCarModel = "[A-Za-z0-9]+";

        public const string RequiredCarModel = "You must enter a car model";
        public const string RequiredMake = "You must enter a car make";
        public const string RequiredImageUrl = "You must enter a phone number";
        public const string RequiredVIN = "You must enter a car VIN";
    }
}
