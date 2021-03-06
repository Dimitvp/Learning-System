﻿namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using System.Collections.Generic;
    using Services.Admin.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AdminUserListingsViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
