using DbTest.Contexts;
using DS3Wiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS3Wiki.Controllers
{
    public class WeaponsController : Controller
    {
        private readonly WikiContext wikiContext = new WikiContext();

        // GET: Weapons
        public ActionResult Index()
        {
            var weapons = wikiContext.Weapons.ToList();

            return View(weapons);
        }

        public ActionResult Details(int id)
        {
            var weapon = wikiContext.Weapons.Find(id);
            
            if (weapon == null)
            {
                return HttpNotFound();
            }

            return View(weapon);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Weapon weapon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    wikiContext.Weapons.Add(weapon);
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

        public ActionResult Update(int id)
        {
            var weapon = wikiContext.Weapons.Find(id);

            if (weapon == null)
            {
                return HttpNotFound();
            }

            return View(weapon);
        }

        [HttpPost]
        public ActionResult update(Weapon weapon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldWeapon = wikiContext.Weapons.Find(weapon.Id);

                    if (oldWeapon == null)
                    {
                        return HttpNotFound();
                    }

                    oldWeapon.Name = weapon.Name;
                    oldWeapon.Description = weapon.Description;

                    TryUpdateModel(oldWeapon);
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
            var weapon = wikiContext.Weapons.Find(id);

            if (weapon == null)
            {
                return HttpNotFound();
            }

            wikiContext.Weapons.Remove(weapon);
            wikiContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}