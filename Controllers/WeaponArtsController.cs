using DbTest.Contexts;
using DS3Wiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS3Wiki.Controllers
{
    [Authorize(Roles = "Admin,Contributor")]
    public class WeaponArtsController : Controller
    {
        private readonly WikiContext wikiContext = new WikiContext();

        public ActionResult Index()
        {
            var weaponArts = wikiContext.WeaponArts.ToList();

            return View(weaponArts);
        }

        public ActionResult Details(int id)
        {
            var weaponArt = wikiContext.WeaponArts.Find(id);

            if (weaponArt == null)
            {
                return HttpNotFound();
            }

            return View(weaponArt);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WeaponArt weaponArt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    wikiContext.WeaponArts.Add(weaponArt);
                    wikiContext.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var weaponArt = wikiContext.WeaponArts.Find(id);

            if (weaponArt == null)
            {
                return HttpNotFound();
            }

            return View(weaponArt);
        }

        [HttpPost]
        public ActionResult Edit(WeaponArt weaponArt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldWeaponArt = wikiContext.WeaponArts.Find(weaponArt.Id);

                    if (oldWeaponArt == null)
                    {
                        return HttpNotFound();
                    }

                    oldWeaponArt.Name = weaponArt.Name;
                    oldWeaponArt.Description = weaponArt.Description;

                    TryUpdateModel(oldWeaponArt);
                    wikiContext.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var weaponArt = wikiContext.WeaponArts.Find(id);

            if (weaponArt == null)
            {
                return HttpNotFound();
            }

            wikiContext.WeaponArts.Remove(weaponArt);
            wikiContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}