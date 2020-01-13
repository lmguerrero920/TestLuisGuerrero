using PagedList;
using Registros.Data;
using Registros.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Registros.Controllers
{
    public class EstudiantesController : Controller
    {

        private RegistrosContext db = new RegistrosContext();

        //Validacion de campo unico
     /* public ActionResult IsTagAvailble(string Tag)
        {
            using (RegistrosContext db = new RegistrosContext())
            {
                try
                {
                    var tag = db.Estudiantes.Single(m => m.Correo == Tag);
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
        }
    */
    
        


        //Filtrado y presentación
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var estudiantes = from s in db.Estudiantes
                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                estudiantes = estudiantes.Where(s => s.Correo.Contains(searchString)
                                       || s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    estudiantes = estudiantes.OrderByDescending(s => s.Nombre);
                    break;
                case "Date":
                    estudiantes = estudiantes.OrderBy(s => s.Nacimiento);
                    break;
                case "date_desc":
                    estudiantes = estudiantes.OrderByDescending(s => s.Apellido);
                    break;
                default:  // Name ascending 
                    estudiantes = estudiantes.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(estudiantes.ToPagedList(pageNumber, pageSize));
        }
        // GET: Estudiantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public ActionResult Create()
        {
            return View();
        }

        
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstudiante,Correo,Clave,Nombre,Apellido,Nacimiento")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Estudiantes.Add(estudiante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstudiante,Correo,Clave,Nombre,Apellido,Nacimiento")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);
            db.Estudiantes.Remove(estudiante);
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
