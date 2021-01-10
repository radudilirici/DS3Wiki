using DS3Wiki.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS3Wiki.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var users = context.Users.Include(x => x.Roles).OrderBy(x => x.Email).ToList();

            return View(users);
        }

        public ActionResult Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            var user = context.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id.Equals(id));

            if (user == null)
            {
                return HttpNotFound();
            }

            var roleName = context.Roles.Find(user.Roles.First().RoleId).Name;

            ViewBag.roleName = roleName;

            return View(user);
        }

        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            UserViewModel model = new UserViewModel();

            var user = context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = context.Roles.Select(x => new
            {
                RoleName = x.Name
            }).ToList();

            ViewBag.UserRoles = new SelectList(userRoles, "RoleName", "RoleName");

            model.User = user;
            if (user.Roles.Any())
            {
                model.RoleName = context.Roles.Find(user.Roles.First().RoleId).Name;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string id, UserViewModel model)
        {
            try
            {
                ApplicationUser user = context.Users.Find(id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                if (TryUpdateModel(user))
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                    var roles = context.Roles.ToList();

                    foreach (var role in roles)
                    {
                        userManager.RemoveFromRole(user.Id, role.Name);
                    }

                    userManager.AddToRole(user.Id, model.RoleName);

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
    }
}