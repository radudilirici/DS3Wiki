using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DS3Wiki.Models;
using DbTest.Contexts;

namespace DS3Wiki.Controllers
{
    public class EnemiesController : Controller
    {
        private readonly WikiContext db = new WikiContext();
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Enemies
        public ActionResult Index()
        {
            return View(db.Enemies.ToList());
        }

        // GET: Enemies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enemy enemy = db.Enemies.Include(x => x.Weapons).FirstOrDefault(x => x.Id == id);
            if (enemy == null)
            {
                return HttpNotFound();
            }
            return View(enemy);
        }

        // GET: Enemies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enemies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Enemy enemy)
        {
            if (ModelState.IsValid)
            {
                db.Enemies.Add(enemy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enemy);
        }

        // GET: Enemies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enemy enemy = db.Enemies.Find(id);
            if (enemy == null)
            {
                return HttpNotFound();
            }
            return View(enemy);
        }

        // POST: Enemies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Enemy enemy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enemy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enemy);
        }

        public ActionResult EditDrops(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enemy enemy = db.Enemies.Include(x => x.Weapons).FirstOrDefault(x => x.Id == id);
            if (enemy == null)
            {
                return HttpNotFound();
            }

            var weapons = db.Weapons.Select(x => new
            {
                WeaponId = x.Id,
                WeaponName = x.Name
            }).ToList();

            ViewBag.Weapons = new SelectList(weapons, "WeaponId", "WeaponName");

            return View(enemy);
        }

        [HttpPost]
        public ActionResult AddDrop(int? id, int? weapon_id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enemy enemy = db.Enemies.Include(x => x.Weapons).FirstOrDefault(x => x.Id == id);
            if (enemy == null)
            {
                return HttpNotFound();
            }

            if (weapon_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Weapon weapon = db.Weapons.Find(weapon_id);
            if (weapon == null)
            {
                return HttpNotFound();
            }

            enemy.Weapons.Add(weapon);
            db.SaveChanges();

            var weapons = db.Weapons.Select(x => new
            {
                WeaponId = x.Id,
                WeaponName = x.Name
            }).ToList();

            ViewBag.Weapons = new SelectList(weapons, "WeaponId", "WeaponName");

            return RedirectToAction("EditDrops", new { id = id });
        }

        [HttpPost]
        public ActionResult RemoveDrop(int? id, int? weapon_id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enemy enemy = db.Enemies.Include(x => x.Weapons).FirstOrDefault(x => x.Id == id);
            if (enemy == null)
            {
                return HttpNotFound();
            }

            if (weapon_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Weapon weapon = db.Weapons.Find(weapon_id);
            if (weapon == null)
            {
                return HttpNotFound();
            }

            enemy.Weapons.Remove(weapon);
            db.SaveChanges();

            var weapons = db.Weapons.Select(x => new
            {
                WeaponId = x.Id,
                WeaponName = x.Name
            }).ToList();

            ViewBag.Weapons = new SelectList(weapons, "WeaponId", "WeaponName");

            return RedirectToAction("EditDrops", new { id = id});
        }

        // POST: Enemies/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Enemy enemy = db.Enemies.Find(id);
            db.Enemies.Remove(enemy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
