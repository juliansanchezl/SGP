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
    public class CiudadController : Controller
    {
        private IRepository<Ciudad> persistenceciudad;
        private IRepository<Departamento> persistencedepartamento;

        public CiudadController(IRepository<Ciudad> persistenceciudad, IRepository<Departamento> persistencedepartamento)
        {
            this.persistenceciudad = persistenceciudad;
            this.persistencedepartamento = persistencedepartamento;
        }

        // GET: Ciudad
        public ActionResult Index()
        {
            return View(persistenceciudad.FindAll());
        }

        // GET: Ciudad/Details/5
        public ActionResult Details(int id)
        {
            Ciudad ciudad = persistenceciudad.FindById(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return View(ciudad);
        }

        // GET: Ciudad/Create
        public ActionResult Create()
        {
            ViewBag.departamentoid = new SelectList(persistencedepartamento.FindAll(), "id", "nombre");
            return View();
        }

        // POST: Ciudad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,departamentoid")] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                persistenceciudad.Create(ciudad);
                persistenceciudad.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.departamentoid = new SelectList(persistencedepartamento.FindAll(), "id", "nombre", ciudad.departamentoid);
            return View(ciudad);
        }

        // GET: Ciudad/Edit/5
        public ActionResult Edit(int id)
        {
            Ciudad ciudad = persistenceciudad.FindById(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            ViewBag.departamentoid = new SelectList(persistencedepartamento.FindAll(), "id", "nombre", ciudad.departamentoid);
            return View(ciudad);
        }

        // POST: Ciudad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,departamentoid")] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                persistenceciudad.Update(ciudad);
                persistenceciudad.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.departamentoid = new SelectList(persistencedepartamento.FindAll(), "id", "nombre", ciudad.departamentoid);
            return View(ciudad);
        }

        // GET: Ciudad/Delete/5
        public ActionResult Delete(int id)
        {
            Ciudad ciudad = persistenceciudad.FindById(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return View(ciudad);
        }

        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ciudad ciudad = persistenceciudad.FindById(id);
            persistenceciudad.Delete(ciudad);
            persistenceciudad.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
