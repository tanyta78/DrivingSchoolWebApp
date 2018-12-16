namespace DrivingSchoolWebApp.Data.Common
{
    using System.Collections.Generic;

    public static class GlobalDataConstants
    {
        public const string AdministratorRoleName = "Admin";

        public static readonly List<string> RolesName = new List<string>
        {
            "Admin",
            "User",
            "School"
        };

        public class CloudinarySetup
        {
            public const string AccSecret = "3SuO7VAQJv_DqYW5AE2gE0XCReQ";
            public const string AccApiKey = "845742774873591";
            public const string CloudName = "ds4filwke";
        }
    }
}
