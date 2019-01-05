namespace DrivingSchoolWebApp.Services.Models.Constants
{
    public static class FeedbackModelConstants
    {
        public const int RatingMin = 1;
        public const int RatingMax = 10;
        public const int ContentMaxLength = 250;
        
        public const string ErrMsgRating = "Rating must be between {1} and {2}";
        public const string ErrMsgContent= "Feedback can only contain alphanumeric characters, dashes and underscores, dots, comma.";
        public const string RegexForValidateContent= "[A-Za-z0-9.,-_]+";

        public const string RequiredRating= "You must enter a rating";
        public const string RequiredContent = "Please enter a short feedback";
        
    }
}
