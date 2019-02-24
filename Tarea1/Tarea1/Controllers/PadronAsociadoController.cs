using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tarea1.Models;

namespace Tarea1.Controllers
{
    public class PadronAsociadoController : Controller
    {
        private Tarea1Entities db = new Tarea1Entities();

        // GET: PadronAsociado
        public ActionResult Index()
        {
            var tbl_PadronAsociado = db.tbl_PadronAsociado.Include(t => t.tbl_Evento);
            return View(tbl_PadronAsociado.ToList());
        }

        // GET: PadronAsociado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_PadronAsociado tbl_PadronAsociado = db.tbl_PadronAsociado.Find(id);
            if (tbl_PadronAsociado == null)
            {
                return HttpNotFound();
            }
            return View(tbl_PadronAsociado);
        }

        // GET: PadronAsociado/Create
        public ActionResult Create()
        {
            ViewBag.idEvento = new SelectList(db.tbl_Evento, "id", "Nombre");
            return View();
        }

        // POST: PadronAsociado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idExcel,Nombre,Cedula,Estatus1,Estatus2,Correo,Telefono,Estado3,HoraRegistro,idEvento")] tbl_PadronAsociado tbl_PadronAsociado)
        {
            if (ModelState.IsValid)
            {
                db.tbl_PadronAsociado.Add(tbl_PadronAsociado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEvento = new SelectList(db.tbl_Evento, "id", "Nombre", tbl_PadronAsociado.idEvento);
            return View(tbl_PadronAsociado);
        }

        // GET: PadronAsociado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_PadronAsociado tbl_PadronAsociado = db.tbl_PadronAsociado.Find(id);
            if (tbl_PadronAsociado == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEvento = new SelectList(db.tbl_Evento, "id", "Nombre", tbl_PadronAsociado.idEvento);
            return View(tbl_PadronAsociado);
        }

        // POST: PadronAsociado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idExcel,Nombre,Cedula,Estatus1,Estatus2,Correo,Telefono,Estado3,HoraRegistro,idEvento")] tbl_PadronAsociado tbl_PadronAsociado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_PadronAsociado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEvento = new SelectList(db.tbl_Evento, "id", "Nombre", tbl_PadronAsociado.idEvento);
            return View(tbl_PadronAsociado);
        }

        // GET: PadronAsociado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_PadronAsociado tbl_PadronAsociado = db.tbl_PadronAsociado.Find(id);
            if (tbl_PadronAsociado == null)
            {
                return HttpNotFound();
            }
            return View(tbl_PadronAsociado);
        }

        // POST: PadronAsociado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_PadronAsociado tbl_PadronAsociado = db.tbl_PadronAsociado.Find(id);
            db.tbl_PadronAsociado.Remove(tbl_PadronAsociado);
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
