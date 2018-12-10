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
    }
}
