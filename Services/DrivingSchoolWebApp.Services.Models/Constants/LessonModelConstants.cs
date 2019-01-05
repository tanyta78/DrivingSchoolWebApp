namespace DrivingSchoolWebApp.Services.Models.Constants
{
    public static class LessonModelConstants
    {
        public const int ContentMaxLength = 50;

        public const string ErrMsgRating = "Rating must be between {1} and {2}";
        public const string ErrMsgContent = "Field can only contain alphanumeric characters, dashes and underscores, dots, comma.";
        public const string RegexForValidateContent = "[A-Za-z0-9.,-_]+";

        public const string RequiredContent = "Please enter a short content";

    }
}
