using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panucci.Models.ViewModels;


namespace Panucci.Controllers
{
    [Authorize(Roles ="Manager")]
    public class AdminsController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminsController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser() { UserName = model.UserName};
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    result =   await userManager.AddToRoleAsync(user, model.Role);
                    if(result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
            return View(model);
        }

        [AcceptVerbs("Get","Post")]
        public async Task<JsonResult> UserNameIsExist(string UserName)
        {
            var user = await userManager.FindByNameAsync(UserName);
            if(user==null)
            {
                return Json(true);
            }
            return Json($"User Name '{UserName}' Is Already Used !");
        }


        [HttpPost]
        public async Task<JsonResult> Delete(string ID)
        {
            var user = await userManager.FindByIdAsync(ID);
            if(user.UserName.ToLower()!="admin")
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Json(ID);
                }
            }            
            return Json(0);
        }
    }
}