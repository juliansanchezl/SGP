using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SGP.DAL;
using SGP.Models;

namespace SGP.Controllers
{
    public class DepartamentoController : Controller
    {
        private IRepository<Departamento> persistencedepartamento;
        private IRepository<Pais> persistencepais;

        public DepartamentoController(IRepository<Departamento> persistencedepartamento, IRepository<Pais> persistencepais)
        {
            this.persistencedepartamento = persistencedepartamento;
            this.persistencepais = persistencepais;
        }

        // GET: Departamento
        public ActionResult Index()
        {            
            return View(persistencedepartamento.FindAll());
        }

        // GET: Departamento/Details/5
        public ActionResult Details(int id)
        {
            Departamento departamento = persistencedepartamento.FindById(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: Departamento/Create
        public ActionResult Create()
        {
            ViewBag.paisid = new SelectList(persistencepais.FindAll(), "id", "nombre");
            return View();
        }

        // POST: Departamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,paisid")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                persistencedepartamento.Create(departamento);
                persistencedepartamento.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.paisid = new SelectList(persistencepais.FindAll(), "id", "nombre", departamento.paisid);
            return View(departamento);
        }

        // GET: Departamento/Edit/5
        public ActionResult Edit(int id)
        {         
            Departamento departamento = persistencedepartamento.FindById(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.paisid = new SelectList(persistencepais.FindAll(), "id", "nombre", departamento.paisid);
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,paisid")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                persistencedepartamento.Update(departamento);
                persistencedepartamento.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.paisid = new SelectList(persistencepais.FindAll(), "id", "nombre", departamento.paisid);
            return View(departamento);
        }

        // GET: Departamento/Delete/5
        public ActionResult Delete(int id)
        {
            Departamento departamento = persistencedepartamento.FindById(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = persistencedepartamento.FindById(id);
            persistencedepartamento.Delete(departamento);
            persistencedepartamento.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
