using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class ImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Image
        public ActionResult Index()
        {
            //var fileToRetrieve = db.Images.Find(id);
            //return Image(fileToRetrieve.Content, fileToRetrieve.ContentType);

            var images = db.Images.Include(i => i.Project);
            return View(images.ToList());
        }

        // GET: Image/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageId,ImageName,ProjectId")] Image image, HttpPostedFileBase upload)
        {
                try
                {
                    if (upload.ContentLength > 0)
                    {
                        //it's not empty... so let's see how big it is

                        int intSizeLimit = 1048576; //bytes .... about 1MB
                                                    //int intSizeLimit = 3145728;  //bytes.... about 3 MB

                        if (upload.ContentLength <= intSizeLimit)
                        {
                            //if file size is ok....

                            string fileName = Path.GetFileName(upload.FileName);
                        string filePath = Server.MapPath("~/Images/" + fileName);
                        string relativePath = Path.Combine("~/Images", fileName);
                        //string filePath = Server.MapPath("~/Content/images/" + fileName);

                        //start image tye validation - only these images allowed
                        System.Drawing.Image img =
                                        System.Drawing.Image.FromStream(upload.InputStream);

                            if (ImageFormat.Jpeg.Equals(img.RawFormat))
                            {
                                //then it is a jpg
                                ViewBag.type = "That was a jpeg file.";
                            }
                            else if (ImageFormat.Gif.Equals(img.RawFormat))
                            {
                                //then it is a gif
                                ViewBag.type = "That was a gif file.";
                            }
                            else if (ImageFormat.Bmp.Equals(img.RawFormat))
                            {
                                //then it is a bmp
                                ViewBag.type = "That was a bmp file.";
                            }
                            else if (ImageFormat.Png.Equals(img.RawFormat))
                            {
                                //then it is a png
                                ViewBag.type = "That was a png file.";
                            }
                            else if (ImageFormat.Tiff.Equals(img.RawFormat))
                            {
                                //then it is a Tiff
                                ViewBag.type = "That was a Tiff file.";
                            }
                            else
                            {
                                ViewBag.Msg = "Nope... I don't like this... NOT A VALID IMAGE Type that we allow";
                            }

                            //end

                            image.ImageName = fileName;
                            image.FilePath = relativePath;
                            image.UploadDateTime = DateTime.Now;
                            db.Images.Add(image);
                            db.SaveChanges();
                            upload.SaveAs(filePath);
                            return RedirectToAction("Index");
                        }
                        else
                        {//file size not ok
                            ViewBag.Msg = "Uploaded file exceeds size max.";
                        }
                    }
                    ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", image.ProjectId);
                    return View(image);
                }
                catch (Exception ex)
                {
                    ViewBag.Msg = "Uploaded File NOT Saved Successfully " + ex.Message;

                    return View();
                }
        }

        // GET: Image/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", image.ProjectId);
            return View(image);
        }

        // POST: Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageId,ImageName,ContentType,Content,ProjectId")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", image.ProjectId);
            return View(image);
        }

        // GET: Image/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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
