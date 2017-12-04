namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using LearningSystem.Services.Admin;
    using LearningSystem.Web.Controllers;
    using LearningSystem.Web.Infrastructure.Extensions;
    using Models.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CoursesController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminCourseService courses;

        public CoursesController(UserManager<User> userManager, IAdminCourseService courses)
        {
            this.userManager = userManager;
            this.courses = courses;
        }

        public async Task<IActionResult> Create()
            => View(new AddCourseFormModel
            {
                Trainers = await this.GetTrainers(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30)
            });

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Trainers = await this.GetTrainers();
                return this.View(model);
            }

            await this.courses.Create(
                model.Name,
                model.Description,
                model.StartDate,
                model.EndDate,
                model.TrainerId);

            TempData.AddSuccessMessage($"Course {model.Name} created successful!");

            return RedirectToAction(nameof(HomeController.Index), "Home", new {area = string.Empty});
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainers()
        {
            var trainers = await this
                .userManager
                .GetUsersInRoleAsync(WebConstants.TrainerRole);
            var trainerListItem = trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();

            return trainerListItem;
        }       
    }
}
