using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tarea1.Models;

namespace Tarea1.Controllers
{
    public class EventoController : Controller
    {
        private Tarea1Entities db = new Tarea1Entities();

        // GET: Evento
        public ActionResult Index()
        {
            
            return View(db.tbl_Evento.ToList());
        }

        // GET: Evento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Evento tbl_Evento = db.tbl_Evento.Find(id);
            if (tbl_Evento == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Evento);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nombre,Fecha,Activo")] tbl_Evento tbl_Evento)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Evento.Add(tbl_Evento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Evento);
        }

        // GET: Evento/Edit/5
        public ActionResult AgregarPadron(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Evento tbl_Evento = db.tbl_Evento.Find(id);
            if (tbl_Evento == null)
            {
                return HttpNotFound();
            }
            //return RedirectToAction("Create", new RouteValueDictionary(
              //  new { controller = PatronAsociado, action = "Main", Id = Id }));
            return View();
        }

        // POST: Evento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarPadron(int id)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(id);
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Evento tbl_Evento = db.tbl_Evento.Find(id);
            if (tbl_Evento == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Evento);
        }

        // POST: Evento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nombre,Fecha,Activo")] tbl_Evento tbl_Evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Evento);
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Evento tbl_Evento = db.tbl_Evento.Find(id);
            if (tbl_Evento == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Evento tbl_Evento = db.tbl_Evento.Find(id);
            db.tbl_Evento.Remove(tbl_Evento);
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
