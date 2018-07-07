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
    public class PersonaController : Controller
    {
        private IRepository<Persona> persistencepersona;
        private IRepository<Ciudad> persistenceciudad;

        public PersonaController(IRepository<Persona> persistencepersona, IRepository<Ciudad> persistenceciudad)
        {
            this.persistencepersona = persistencepersona;
            this.persistenceciudad = persistenceciudad;
        }


        public ActionResult Index()
        {
            ViewBag.ciudadid = new SelectList(persistenceciudad.FindAll(), "id", "nombre");
            return View(new List<Persona>());
        }

        [HttpPost]
        public ActionResult Index(string txtnombre, string txtapellido, string ciudadid)
        {
            int idciudad = 0;
            Int32.TryParse(ciudadid, out idciudad);

            ViewBag.ciudadid = new SelectList(persistenceciudad.FindAll(), "id", "nombre");

            //Filtro personalizado
            return View(persistencepersona.FindAll(x =>
                ( txtapellido != "" ? x.apellidos == txtapellido : x.apellidos != null)
                && (txtnombre != "" ? x.nombres == txtnombre : x.nombres != null)
                && ( idciudad!=0 ? x.ciudadid == idciudad : x.ciudadid!=null)
            ));
        }
        


        // GET: Persona/Details/5
        public ActionResult Details(int id)
        {
            Persona persona = persistencepersona.FindById(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Persona/Create
        public ActionResult Create()
        {
            ViewBag.ciudadid = new SelectList(persistenceciudad.FindAll(), "id", "nombre");
            return View();
        }

        // POST: Persona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombres,apellidos,fechanacimiento,ciudadid")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persistencepersona.Create(persona);
                persistencepersona.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ciudadid = new SelectList(persistenceciudad.FindAll(), "id", "nombre", persona.ciudadid);
            return View(persona);
        }

        // GET: Persona/Edit/5
        public ActionResult Edit(int id)
        {
            Persona persona = persistencepersona.FindById(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.ciudadid = new SelectList(persistenceciudad.FindAll(), "id", "nombre", persona.ciudadid);
            return View(persona);
        }

        // POST: Persona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombres,apellidos,fechanacimiento,ciudadid")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persistencepersona.Update(persona);
                persistencepersona.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ciudadid = new SelectList(persistenceciudad.FindAll(), "id", "nombre", persona.ciudadid);
            return View(persona);
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(int id)
        {
            Persona persona = persistencepersona.FindById(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = persistencepersona.FindById(id);
            persistencepersona.Delete(persona);
            persistencepersona.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
