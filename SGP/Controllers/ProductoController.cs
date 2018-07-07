using System.Web.Mvc;
using SGP.DAL;
using SGP.Models;

namespace SGP.Controllers
{
    public class ProductoController : Controller
    {
        private IRepository<Producto> persistenceproducto;
        private IRepository<TipoProducto> persistencetipoproducto;

        public ProductoController(IRepository<Producto> persistenceproducto, IRepository<TipoProducto> persistencetipoproducto)
        {
            this.persistenceproducto = persistenceproducto;
            this.persistencetipoproducto = persistencetipoproducto;
        }

        // GET: Producto
        public ActionResult Index()
        {
            return View(persistenceproducto.FindAll());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            Producto producto = persistenceproducto.FindById(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.tipoproductoid = new SelectList(persistencetipoproducto.FindAll(), "id", "nombre");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,precio,tipoproductoid")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                persistenceproducto.Create(producto);
                persistenceproducto.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tipoproductoid = new SelectList(persistencetipoproducto.FindAll(), "id", "nombre", producto.tipoproductoid);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            Producto producto = persistenceproducto.FindById(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.tipoproductoid = new SelectList(persistencetipoproducto.FindAll(), "id", "nombre", producto.tipoproductoid);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,precio,tipoproductoid")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                persistenceproducto.Update(producto);
                persistenceproducto.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tipoproductoid = new SelectList(persistencetipoproducto.FindAll(), "id", "nombre", producto.tipoproductoid);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            Producto producto = persistenceproducto.FindById(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = persistenceproducto.FindById(id);
            persistenceproducto.Delete(producto);
            persistenceproducto.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
