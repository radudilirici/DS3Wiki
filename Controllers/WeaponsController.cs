﻿using DbTest.Contexts;
using DS3Wiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DS3Wiki.Controllers
{
    public class WeaponsController : Controller
    {
        private readonly WikiContext wikiContext = new WikiContext();

        // GET: Weapons
        public ActionResult Index()
        {
            var weapons = wikiContext.Weapons.ToList();

            var com = wikiContext.Comments.Where(x => x.Category == "Weapons").ToList();
            com.Reverse();
            ViewBag.Comments = com;
            ViewBag.Email = User.Identity.Name;

            return View(weapons);
        }

        [HttpPost]
        public ActionResult Search(string weapon_name)
        {
            var com = wikiContext.Comments.Where(x => x.Category == "Weapons").ToList();
            com.Reverse();
            ViewBag.Comments = com;
            ViewBag.Email = User.Identity.Name;

            if (weapon_name.Equals(""))
                return View("Index", wikiContext.Weapons.ToList());

            var weapons = wikiContext.Weapons.Where(x => x.Name.ToLower().Contains(weapon_name.ToLower())).ToList();

            return View("Index", weapons);
        }

        public ActionResult Details(int id)
        {
            Weapon weapon = wikiContext.Weapons.Include(x => x.Enemies).Include("WeaponArt").FirstOrDefault(x => x.Id == id);
            
            if (weapon == null)
            {
                return HttpNotFound();
            }

            return View(weapon);
        }

        public ActionResult Create()
        {
            var weaponArts = wikiContext.WeaponArts.Select(x => new
            {
                WeaponArtId = x.Id,
                WeaponArtName = x.Name
            }).ToList();

            ViewBag.WeaponArts = new SelectList(weaponArts, "WeaponArtId", "WeaponArtName");

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

        public ActionResult Edit(int id)
        {
            var weapon = wikiContext.Weapons.Find(id);

            if (weapon == null)
            {
                return HttpNotFound();
            }

            var weaponArts = wikiContext.WeaponArts.Select(x => new
            {
                WeaponArtId = x.Id,
                WeaponArtName = x.Name
            }).ToList();

            ViewBag.WeaponArts = new SelectList(weaponArts, "WeaponArtId", "WeaponArtName");

            return View(weapon);
        }

        [HttpPost]
        public ActionResult Edit(Weapon weapon)
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
                    oldWeapon.WeaponArtId = weapon.WeaponArtId;

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