using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGP.Models;
using SGP.DAL;

namespace SGP.Controllers
{
    public class ReportesController : Controller
    {
        IRepository<Persona> persistencepersona;
        SGPEntities entity;

        public ReportesController(IRepository<Persona> persistencepersona)
        {
            this.persistencepersona = persistencepersona;
            entity = new SGPEntities();
        }


        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }



        // GET: Reportes
        public ActionResult ReporteClientes()
        {
            //ViewBag.reporte = entity.SP_ReporteClientesByCiudad("tunja");
            //ViewBag.reporte = null;
            return View();
        }

        [HttpPost]
        public ActionResult ReporteClientes(string txtciudad)
        {
            ViewBag.reporte = entity.SP_ReporteClientesByCiudad(txtciudad);
            return View();
        }


    }
}