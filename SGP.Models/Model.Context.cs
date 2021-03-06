﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGP.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SGPEntities : DbContext
    {
        public SGPEntities()
            : base("name=SGPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }
        public virtual DbSet<EstadoPago> EstadoPago { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<TipoProducto> TipoProducto { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
    
        public virtual ObjectResult<SP_ReporteClientesByCiudad_Result> SP_ReporteClientesByCiudad(string nombreciudad)
        {
            var nombreciudadParameter = nombreciudad != null ?
                new ObjectParameter("nombreciudad", nombreciudad) :
                new ObjectParameter("nombreciudad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ReporteClientesByCiudad_Result>("SP_ReporteClientesByCiudad", nombreciudadParameter);
        }
    
        public virtual ObjectResult<SP_ReporteProductos_Result> SP_ReporteProductos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ReporteProductos_Result>("SP_ReporteProductos");
        }
    
        public virtual ObjectResult<SP_ReporteProductosByID_Result> SP_ReporteProductosByID(Nullable<int> tipoproductoid)
        {
            var tipoproductoidParameter = tipoproductoid.HasValue ?
                new ObjectParameter("tipoproductoid", tipoproductoid) :
                new ObjectParameter("tipoproductoid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ReporteProductosByID_Result>("SP_ReporteProductosByID", tipoproductoidParameter);
        }
    }
}
