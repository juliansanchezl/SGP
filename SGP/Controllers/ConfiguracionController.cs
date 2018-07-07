using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGP.Controllers
{
    public class ConfiguracionController : Controller
    {
        // GET: Configuracion
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GestionarTiposProducto()
        {
            return RedirectToAction("Index", "TipoProducto");
        }


        public ActionResult GestionarEstadoPago()
        {
            return RedirectToAction("Index", "EstadoPago");
        }

        public ActionResult GestionarPais()
        {
            return RedirectToAction("Index", "Pais");
        }

        public ActionResult GestionarDepartamento()
        {
            return RedirectToAction("Index", "Departamento");
        }

        public ActionResult GestionarCiudad()
        {
            return RedirectToAction("Index", "Ciudad");
        }


    }
}