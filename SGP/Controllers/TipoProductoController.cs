using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SGP.DAL;
using SGP.Models;

namespace SGP.Controllers
{
    public class TipoProductoController : Controller
    {
        private IRepository<TipoProducto> persistencetipoproducto;

        public TipoProductoController(IRepository<TipoProducto> persistencetipoproducto)
        {
            this.persistencetipoproducto = persistencetipoproducto;
        }

        // GET: TipoProducto
        public ActionResult Index()
        {
            return View(persistencetipoproducto.FindAll());
        }

        // GET: TipoProducto/Details/5
        public ActionResult Details(int id)
        {
            TipoProducto tipoProducto = persistencetipoproducto.FindById(id);
            if (tipoProducto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProducto);
        }

        // GET: TipoProducto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoProducto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] TipoProducto tipoProducto)
        {
            if (ModelState.IsValid)
            {
                persistencetipoproducto.Create(tipoProducto);
                persistencetipoproducto.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoProducto);
        }

        // GET: TipoProducto/Edit/5
        public ActionResult Edit(int id)
        {
            TipoProducto tipoProducto = persistencetipoproducto.FindById(id);
            if (tipoProducto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProducto);
        }

        // POST: TipoProducto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] TipoProducto tipoProducto)
        {
            if (ModelState.IsValid)
            {
                persistencetipoproducto.Update(tipoProducto);
                persistencetipoproducto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoProducto);
        }

        // GET: TipoProducto/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProducto tipoProducto = persistencetipoproducto.FindById(id);
            if (tipoProducto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProducto);
        }

        // POST: TipoProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoProducto tipoProducto = persistencetipoproducto.FindById(id);
            persistencetipoproducto.Delete(tipoProducto);
            persistencetipoproducto.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
