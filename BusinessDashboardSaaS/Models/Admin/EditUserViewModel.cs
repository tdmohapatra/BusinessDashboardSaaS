﻿namespace BusinessDashboardSaaS.Models.Admin
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; } // ✅ Added

        public List<string> AllRoles { get; set; } = new();
        public List<string> SelectedRoles { get; set; } = new();
    }


    public class RoleViewModel
    {
        public string RoleName { get; set; }
        public bool IsAssigned { get; set; }
    }


}
