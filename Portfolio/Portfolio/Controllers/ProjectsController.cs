using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string CurrentSort, int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "First Name" : sortOrder;
            IPagedList<Project> projects = null;
            switch (sortOrder)
            {
                case "First Name":
                    if (sortOrder.Equals(CurrentSort))
                        projects = db.Projects.OrderByDescending
                                (m => m.Author.FirstName).ToPagedList(pageIndex, pageSize);
                    else
                        projects = db.Projects.OrderBy
                                (m => m.Author.FirstName).ToPagedList(pageIndex, pageSize);
                    break;
                case "Title":
                    if (sortOrder.Equals(CurrentSort))
                        projects = db.Projects.OrderByDescending
                                (m => m.ProjectTitle).ToPagedList(pageIndex, pageSize);
                    else
                        projects = db.Projects.OrderBy
                                (m => m.ProjectTitle).ToPagedList(pageIndex, pageSize);
                    break;
                case "Description":
                    if (sortOrder.Equals(CurrentSort))
                        projects = db.Projects.OrderByDescending
                                (m => m.ProjectDescription).ToPagedList(pageIndex, pageSize);
                    else
                        projects = db.Projects.OrderBy
                                (m => m.ProjectDescription).ToPagedList(pageIndex, pageSize);
                    break;
                case "Project Date":
                    if (sortOrder.Equals(CurrentSort))
                        projects = db.Projects.OrderByDescending
                                (m => m.ProjectDate).ToPagedList(pageIndex, pageSize);
                    else
                        projects = db.Projects.OrderBy
                                (m => m.ProjectDate).ToPagedList(pageIndex, pageSize);
                    break;
                case "Last Updated":
                    if (sortOrder.Equals(CurrentSort))
                        projects = db.Projects.OrderByDescending
                                (m => m.Updated).ToPagedList(pageIndex, pageSize);
                    else
                        projects = db.Projects.OrderBy
                                (m => m.Updated).ToPagedList(pageIndex, pageSize);
                    break;
                case "Default":
                    projects = db.Projects.OrderBy
                        (m => m.Author.FirstName).ToPagedList(pageIndex, pageSize);
                    break;
            }
            return View(projects);

            //var projects = db.Projects.Include(p => p.Author);
            //return View(projects.ToList());
        }

        [AllowAnonymous]
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectTitle,ProjectDescription,ProjectDate,Updated,AuthorId")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.ProjectId = Convert.ToInt32(Guid.NewGuid());
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", project.AuthorId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", project.AuthorId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectTitle,ProjectDescription,ProjectDate,Updated,AuthorId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", project.AuthorId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
