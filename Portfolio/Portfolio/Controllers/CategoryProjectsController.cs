using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class CategoryProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategoryProjects
        public ActionResult Index()
        {
            var categoryProjects = db.CategoryProjects.Include(c => c.Category).Include(c => c.Project);
            return View(categoryProjects.ToList());
        }

        // GET: CategoryProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProject categoryProject = db.CategoryProjects.Find(id);
            if (categoryProject == null)
            {
                return HttpNotFound();
            }
            return View(categoryProject);
        }

        // GET: CategoryProjects/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            return View();
        }

        // POST: CategoryProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,ProjectId")] CategoryProject categoryProject)
        {
            if (ModelState.IsValid)
            {
                db.CategoryProjects.Add(categoryProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", categoryProject.CategoryId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", categoryProject.ProjectId);
            return View(categoryProject);
        }

        // GET: CategoryProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProject categoryProject = db.CategoryProjects.Find(id);
            if (categoryProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", categoryProject.CategoryId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", categoryProject.ProjectId);
            return View(categoryProject);
        }

        // POST: CategoryProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,ProjectId")] CategoryProject categoryProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", categoryProject.CategoryId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", categoryProject.ProjectId);
            return View(categoryProject);
        }

        // GET: CategoryProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProject categoryProject = db.CategoryProjects.Find(id);
            if (categoryProject == null)
            {
                return HttpNotFound();
            }
            return View(categoryProject);
        }

        // POST: CategoryProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryProject categoryProject = db.CategoryProjects.Find(id);
            db.CategoryProjects.Remove(categoryProject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
