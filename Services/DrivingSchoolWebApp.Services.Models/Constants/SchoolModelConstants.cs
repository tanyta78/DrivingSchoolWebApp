namespace DrivingSchoolWebApp.Services.Models.Constants
{

    public static class SchoolModelConstants
    {
        public const int TradeMarkMaxLength = 20;
        public const int TradeMarkMinLength = 2;
        public const string ErrMsgTrademarkLength = "Trademark length must be between 2 and 20 characters";

        public const int OfficeAddressMaxLength = 30;
        public const int OfficeAddressMinLength = 10;
        public const string ErrMsgOfficeAddressLength = "Office address length must be between 10 and 30 characters";
       
        public const string ErrMsgSchoolModel = "May only contain alphanumeric characters";
        public const string RegexForValidationSchoolModel = "[A-Za-z0-9]+";

        public const string RequiredTradeMark = "You must enter a Trade mark";
        public const string RequiredOfficeAddress = "You must enter a School address";
        public const string RequiredPhone = "You must enter a phone number";
       
    }
}
