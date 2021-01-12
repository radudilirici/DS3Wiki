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
    [Authorize]
    public class CommentsController : Controller
    {
        private WikiContext db = new WikiContext();

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }

            try
            {
                return RedirectToAction("Index", comment.Category);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
