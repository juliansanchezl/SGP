using SGP.DAL;
using SGP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SGP.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public List<Producto> GetProducts()
        {
            List<Producto> listaproductos = new List<Producto>();
            IRepository<SGP.Models.Producto> repository = new Repository<SGP.Models.Producto>();
            var lista = repository.FindAll();
            foreach (var item in lista)
            {
                listaproductos.Add( TranslateTblProductoToProductoEntity(item));
            }

            return listaproductos;
        }

        public List<Producto> GetProductsByName(string nombre)
        {
            List<Producto> listaproductos = new List<Producto>();
            IRepository<SGP.Models.Producto> repository = new Repository<SGP.Models.Producto>();
            var query = repository.FindAll(x=> x.nombre.Contains(nombre));
            foreach (var item in query)
            {
                listaproductos.Add(TranslateTblProductoToProductoEntity(item));
            }
            return listaproductos;
        }


        private Producto TranslateTblProductoToProductoEntity(Models.Producto item)
        {
            return new Producto()
            {
                id = item.id,
                nombre = item.nombre,
                descripcion = item.descripcion,
                precio = item.precio,
                tipo = item.TipoProducto.nombre
            };
        }
    }
}
