﻿namespace DrivingSchoolWebApp.Services.Models.Account
{
    using System.Collections.Generic;

    public class AdminPanelUsersListViewModel
    {
        public IEnumerable<AdminPanelUsersViewModel> Users { get; set; }
    }
}
