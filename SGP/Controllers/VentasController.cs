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
    public class VentasController : Controller
    {
        private IRepository<Venta> persistenceventa;
        private IRepository<Persona> persistencepersona;
        private IRepository<Producto> persistenceproducto;
        private IRepository<DetalleVenta> persistencedetalleventa;

        public VentasController(IRepository<Venta> persistenceventa, IRepository<Persona> persistencepersona, IRepository<Producto> persistenceproducto, IRepository<DetalleVenta> persistencedetalleventa)
        {
            this.persistenceventa = persistenceventa;
            this.persistencepersona = persistencepersona;
            this.persistenceproducto = persistenceproducto;
            this.persistencedetalleventa = persistencedetalleventa;
        }




        // GET: Ventas
        public ActionResult Index()
        {           
            return View(persistenceventa.FindAll());
        }



        // GET: Ventas/Create
        public ActionResult Create()
        {
            var lista = persistencepersona.FindAll().Select(i=> new { id = i.id, nombres=i.nombres + " " + i.apellidos });
            ViewBag.clienteid = new SelectList(lista, "id", "nombres");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fecha,clienteid,Total")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                venta.Total = 0;
                venta.fecha = DateTime.Now;
                persistenceventa.Create(venta);
                persistenceventa.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clienteid = new SelectList(persistencepersona.FindAll(), "id", "nombres", venta.clienteid);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int id)
        {
            Venta venta = persistenceventa.FindById(id);
            if (venta == null)
            {
                return HttpNotFound();
            }

            var lista = persistencepersona.FindAll().Select(i => new { id = i.id, nombres = i.nombres + " " + i.apellidos });
            ViewBag.clienteid = new SelectList(lista, "id", "nombres", venta.clienteid);

            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fecha,clienteid,Total")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                persistenceventa.Update(venta);
                persistenceventa.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clienteid = new SelectList(persistencepersona.FindAll(), "id", "nombres", venta.clienteid);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int id)
        {
            Venta venta = persistenceventa.FindById(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           persistencedetalleventa.Delete(x=> x.ventaid==id);
           persistencedetalleventa.SaveChanges();

            //Se elimina la venta
            Venta venta = persistenceventa.FindById(id);
            persistenceventa.Delete(venta);
            persistenceventa.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: Ventas/Details/5
        public ActionResult Details(int id)
        {
            Venta venta = persistenceventa.FindById(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }



        public ActionResult irSeleccionarProducto(int id)
        {
            return RedirectToAction("SeleccionarProducto", new { idventa = id});
        }

        public ActionResult SeleccionarProducto(int idventa)
        {
            ViewBag.idventa = idventa;
            return View(persistenceproducto.FindAll());
        }


        public ActionResult irAgregarProducto(int id, int idventa)
        {
            return RedirectToAction("AgregarProducto", new { productoid=id, ventaid = idventa });
        }

        public ActionResult AgregarProducto(int productoid, int ventaid)
        {
            ViewBag.producto = persistenceproducto.FindById(productoid);
            ViewBag.ventaid = ventaid;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProducto([Bind(Include = "id,ventaid,productoid,cantidad,Total")] DetalleVenta detalleventa)
        {
            if (ModelState.IsValid)
            {
                int ventaid = (int)detalleventa.ventaid;
                double? total= persistenceproducto.FindById((int)detalleventa.productoid).precio * detalleventa.cantidad;

                detalleventa.Total = total;
                persistencedetalleventa.Create(detalleventa);
                persistencedetalleventa.SaveChanges();

                //Se actualiza el valor a pagar                
                var venta = persistenceventa.FindById(ventaid);
                venta.Total += total;
                persistenceventa.Update(venta);
                persistenceventa.SaveChanges();


                return RedirectToAction("Details", new { id = ventaid });
            }

            return View(detalleventa);
        }



        public ActionResult irRemoverProductoVenta(int id)
        {
            return RedirectToAction("RemoverProductoVenta", new { detalleventaid = id });
        }

        public ActionResult RemoverProductoVenta(int detalleventaid)
        {
            ViewBag.producto = persistencedetalleventa.FindById(detalleventaid).Producto;
            return View(persistencedetalleventa.FindById(detalleventaid));
        }

       
        public ActionResult EliminarDetalleVenta(int id)
        {
            DetalleVenta detalleventa = persistencedetalleventa.FindById(id);
            int ventaid = (int)detalleventa.ventaid;
            Double total = (double)detalleventa.Total; 

            //Se elimina el registro
            persistencedetalleventa.Delete(detalleventa);
            persistencedetalleventa.SaveChanges();

            //Se actualiza el valor a pagar de la venta
            var venta = persistenceventa.FindById(ventaid);
            venta.Total -= total;
            persistenceventa.Update(venta);
            persistenceventa.SaveChanges();

            return RedirectToAction("Details", new { id = ventaid });            
        }


        public ActionResult irRealizarPago(int ventaid)
        {
            return RedirectToAction("RealizarPago","Pago",new { ventaid });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
