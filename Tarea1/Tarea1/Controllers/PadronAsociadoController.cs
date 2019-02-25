using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
        public ActionResult Create(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (!file.FileName.EndsWith(".xls") && !file.FileName.EndsWith(".xlsx"))
                    return View();

                var fileName = DateTime.Now.ToString("yyyyMMddHHmm.") + file.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();
                SaveFile(file, fileName);
                UploadRecordsToDataBase(fileName);
                return RedirectToAction("Index");
            }

            // Tu podras decidir que hacer aqui
            // si el archivo es nulo
            return View();

        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UploadFile(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        if (!file.FileName.EndsWith(".xls") && !file.FileName.EndsWith(".xlsx"))
        //            return View();

        //        var fileName = DateTime.Now.ToString("yyyyMMddHHmm.") + file.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();
        //        SaveFile(file, fileName);
        //        UploadRecordsToDataBase(fileName);
        //        return RedirectToAction("Index");
        //    }

        //    // Tu podras decidir que hacer aqui
        //    // si el archivo es nulo
        //    return View();

        //}

        private void SaveFile(HttpPostedFileBase file, string fileName)
        {
            var path = System.IO.Path.Combine(Server.MapPath("~/Content/Files/"), fileName);
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);

            using (var sw = new System.IO.FileStream(path, System.IO.FileMode.Create))
            {
                sw.Write(data, 0, data.Length);
            }
        }

        private void UploadRecordsToDataBase(string fileName)
        {
            var records = new List<tbl_PadronAsociado>();
            using (var stream = System.IO.File.Open(Path.Combine(Server.MapPath("~/Content/Files/"), fileName), FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        records.Add(new tbl_PadronAsociado
                        {

                            idExcel = reader.GetString(0),
                            Nombre = reader.GetString(1),
                            Cedula = reader.GetString(2),
                            Estatus1 = reader.GetString(3),
                            Estatus2 = reader.GetString(4),
                            Correo = reader.GetString(5),
                            Telefono = reader.GetString(6),
                            Estado3 = "",
                        });
                    }
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_PadronAsociado.AddRange(records);
                    db.SaveChanges();
                }
            }



            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }

        }
    }
}
