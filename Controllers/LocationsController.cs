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
    public class LocationsController : Controller
    {
        private readonly WikiContext db = new WikiContext();

        // GET: Locations
        public ActionResult Index()
        {
            var com = db.Comments.Where(x => x.Category == "Locations").ToList();
            com.Reverse();
            ViewBag.Comments = com;
            ViewBag.Email = User.Identity.Name;

            return View(db.Locations.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Include(x => x.Enemies).FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        public ActionResult EditEnemies(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = db.Locations.Include(x => x.Enemies).FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }

            ViewBag.EnemyId = new SelectList(db.Enemies, "Id", "Name");

            return View(location);
        }

        [HttpPost]
        public ActionResult AddEnemy(int? id, int? enemyId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location= db.Locations.Include(x => x.Enemies).FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }

            if (enemyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enemy enemy = db.Enemies.Find(enemyId);
            if (enemy == null)
            {
                return HttpNotFound();
            }

            location.Enemies.Add(enemy);
            db.SaveChanges();

            ViewBag.EnemyId = new SelectList(db.Enemies, "Id", "Name");

            return View("EditEnemies", location);
        }

        [HttpPost]
        public ActionResult RemoveEnemy(int? id, int? enemyId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = db.Locations.Include(x => x.Enemies).FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }

            if (enemyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enemy enemy = db.Enemies.Find(enemyId);
            if (enemy == null)
            {
                return HttpNotFound();
            }

            location.Enemies.Remove(enemy);
            db.SaveChanges();

            return RedirectToAction("EditEnemies", new { id = id });
        }

        // POST: Locations/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
