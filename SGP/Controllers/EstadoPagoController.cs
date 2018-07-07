using System.Web.Mvc;
using SGP.Models;
using SGP.DAL;

namespace SGP.Controllers
{
    public class EstadoPagoController : Controller
    {
        private IRepository<EstadoPago> persistenceestadopago;

        public EstadoPagoController(IRepository<EstadoPago> persistenceestadopago)
        {
            this.persistenceestadopago = persistenceestadopago;
        }

        // GET: EstadoPago
        public ActionResult Index()
        {
            return View(persistenceestadopago.FindAll());
        }

        // GET: EstadoPago/Details/5
        public ActionResult Details(int id)
        {
            EstadoPago estadoPago = persistenceestadopago.FindById(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // GET: EstadoPago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] EstadoPago estadoPago)
        {
            if (ModelState.IsValid)
            {
                persistenceestadopago.Create(estadoPago);
                persistenceestadopago.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoPago);
        }

        // GET: EstadoPago/Edit/5
        public ActionResult Edit(int id)
        {
            EstadoPago estadoPago = persistenceestadopago.FindById(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // POST: EstadoPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] EstadoPago estadoPago)
        {
            if (ModelState.IsValid)
            {
                persistenceestadopago.Update(estadoPago);
                persistenceestadopago.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoPago);
        }

        // GET: EstadoPago/Delete/5
        public ActionResult Delete(int id)
        {
            EstadoPago estadoPago = persistenceestadopago.FindById(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // POST: EstadoPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoPago estadoPago = persistenceestadopago.FindById(id);
            persistenceestadopago.Delete(estadoPago);
            persistenceestadopago.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
