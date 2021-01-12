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

        // GET: Enemies
        public ActionResult Index()
        {
            var com = db.Comments.Where(x => x.Category == "Enemies").ToList();
            com.Reverse();
            ViewBag.Comments = com;
            ViewBag.Email = User.Identity.Name;

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

            ViewBag.WeaponId = new SelectList(db.Weapons, "Id", "Name");

            return View(enemy);
        }

        [HttpPost]
        public ActionResult AddDrop(int? id, int? weaponId)
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

            if (weaponId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Weapon weapon = db.Weapons.Find(weaponId);
            if (weapon == null)
            {
                return HttpNotFound();
            }

            enemy.Weapons.Add(weapon);
            db.SaveChanges();

            ViewBag.WeaponId = new SelectList(db.Weapons, "Id", "Name");

            return View("EditDrops", enemy);
        }

        [HttpPost]
        public ActionResult RemoveDrop(int? id, int? weaponId)
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

            if (weaponId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Weapon weapon = db.Weapons.Find(weaponId);
            if (weapon == null)
            {
                return HttpNotFound();
            }

            enemy.Weapons.Remove(weapon);
            db.SaveChanges();

            ViewBag.WeaponId = new SelectList(db.Weapons, "Id", "Name");

            return View("EditDrops", enemy);
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
