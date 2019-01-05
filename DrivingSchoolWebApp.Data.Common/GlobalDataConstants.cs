namespace DrivingSchoolWebApp.Data.Common
{
    using System.Collections.Generic;

    public static class GlobalDataConstants
    {
        //----Roles names---
        public const string AdministratorRoleName = "Admin";
        public const string SchoolRoleName = "School";
        public const string UserRoleName = "User";

        public static readonly List<string> RolesName = new List<string>
        {
            "Admin",
            "User",
            "School"
        };

        //--Areas names
        public const string AdministratorArea = "Admin";
        public const string SchoolArea = "SchoolManage";





        //--Exception messages
        public const string NoRights = "You do not have rights for this operation!";

        public class CloudinarySetup
        {
            public const string AccSecret = "3SuO7VAQJv_DqYW5AE2gE0XCReQ";
            public const string AccApiKey = "845742774873591";
            public const string CloudName = "ds4filwke";
        }
    }
}
