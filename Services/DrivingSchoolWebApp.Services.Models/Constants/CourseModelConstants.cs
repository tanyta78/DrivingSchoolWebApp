namespace DrivingSchoolWebApp.Services.Models.Constants
{
    public static class CourseModelConstants
    {

        public const string MinPrice = "0";
        public const string MaxPrice = "1500";
        public const int MinimumLessonsCountMin = 10;
        public const int MinimumLessonsCountMax = 100;
        public const int DescriptionMaxLength = 50;
        
        public const string ErrMsgCoursePrice = "Price must be between {1} and {2}";
        public const string ErrMsgMinLessons = "Minimum lesson count must be between {1} and {2}";
        public const string ErrMsgDescription = "Description can only contain alphanumeric characters, dashes and underscores, dots, comma.";
        public const string RegexForValidationDescription = "[A-Za-z0-9.,-_]+";

        public const string RequiredPrice = "You must enter a price";
        public const string RequiredDescription = "You must enter a short description";
        public const string RequiredMinimumLessonsCount = "You must enter a minimum lesson more than 10.";
       
    }
}
