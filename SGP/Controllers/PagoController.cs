using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SGP.Models;
using SGP.DAL;

namespace SGP.Controllers
{
    public class PagoController : Controller
    {
        private IRepository<Pago> persistencepago;
        private IRepository<EstadoPago> persistenceestadopago;
        private IRepository<Venta> persistenceventa;

        public PagoController(IRepository<Pago> persistencepago, IRepository<EstadoPago> persistenceestadopago, IRepository<Venta> persistenceventa)
        {
            this.persistencepago = persistencepago;
            this.persistenceestadopago = persistenceestadopago;
            this.persistenceventa = persistenceventa;
        }

        // GET: Pago/Create
        public ActionResult RealizarPago(int ventaid)
        {
            ViewBag.venta = persistenceventa.FindById(ventaid);
            return View();
        }

        // POST: Pago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RealizarPago([Bind(Include = "id,fecha,ventaid,valor,estadopagoid")] Pago pago)
        {            
            if (ModelState.IsValid)
            {
                var venta= persistenceventa.FindById((int)pago.ventaid);
                if (pago.valor>=venta.Total)
                {
                    pago.fecha = DateTime.Now;
                    pago.estadopagoid = 2;
                    persistencepago.Create(pago);
                    persistencepago.SaveChanges();
                    return RedirectToAction("Details", "Ventas", new { id = pago.ventaid });
                }
                else
                {
                    ViewBag.Error = "El valor del pago debe ser igual al valor de la venta";
                }                
            }

            ViewBag.venta = persistenceventa.FindById((int)pago.ventaid);
            return View(pago);
        }

        // GET: Pago
        public ActionResult ConsultarPagos()
        {
            return View(persistencepago.FindAll());
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {
            Pago pago = persistencepago.FindById(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            ViewBag.ventaid = pago.ventaid;
            ViewBag.estadopagoid = new SelectList(persistenceestadopago.FindAll(), "id", "nombre", pago.estadopagoid);
            return View(pago);
        }

        // POST: Pago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fecha,ventaid,valor,estadopagoid")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                persistencepago.Update(pago);
                persistencepago.SaveChanges();
                return RedirectToAction("ConsultarPagos");
            }
            ViewBag.estadopagoid = new SelectList(persistenceestadopago.FindAll(), "id", "nombre", pago.estadopagoid);
            return View(pago);
        }

        // GET: Pago/Delete/5
        public ActionResult Delete(int id)
        {
            Pago pago = persistencepago.FindById(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pago pago = persistencepago.FindById(id);
            persistencepago.Delete(pago);
            persistencepago.SaveChanges();
            return RedirectToAction("ConsultarPagos");
        }

        public ActionResult irDetailsVenta(int id)
        {
            return RedirectToAction("Details","Ventas", new { id});
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
