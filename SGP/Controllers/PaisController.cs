using System.Web.Mvc;
using SGP.DAL;
using SGP.Models;

namespace SGP.Controllers
{
    public class PaisController : Controller
    {
        private IRepository<Pais> paispersistence;

        public PaisController(IRepository<Pais> paispersistence)
        {
            this.paispersistence = paispersistence;
        }


        // GET: Pais
        public ActionResult Index()
        {
            return View(paispersistence.FindAll());
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            Pais pais = paispersistence.FindById(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                paispersistence.Create(pais);
                paispersistence.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pais);
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            Pais pais = paispersistence.FindById(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                paispersistence.Update(pais);
                paispersistence.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            Pais pais = paispersistence.FindById(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pais pais = paispersistence.FindById(id);
            paispersistence.Delete(pais);
            paispersistence.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
