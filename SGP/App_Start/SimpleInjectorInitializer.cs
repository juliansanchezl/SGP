[assembly: WebActivator.PostApplicationStartMethod(typeof(SGP.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace SGP.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using SGP.DAL;
    using SGP.Models;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IRepository<Pais>, Repository<Pais>>(Lifestyle.Scoped);
            container.Register<IRepository<Departamento>, Repository<Departamento>>(Lifestyle.Scoped);
            container.Register<IRepository<Ciudad>, Repository<Ciudad>>(Lifestyle.Scoped);
            container.Register<IRepository<Persona>, Repository<Persona>>(Lifestyle.Scoped);
            container.Register<IRepository<TipoProducto>, Repository<TipoProducto>>(Lifestyle.Scoped);
            container.Register<IRepository<Producto>, Repository<Producto>>(Lifestyle.Scoped);
            container.Register<IRepository<EstadoPago>, Repository<EstadoPago>>(Lifestyle.Scoped);
            container.Register<IRepository<Venta>, Repository<Venta>>(Lifestyle.Scoped);
            container.Register<IRepository<DetalleVenta>, Repository<DetalleVenta>>(Lifestyle.Scoped);
            container.Register<IRepository<Pago>, Repository<Pago>>(Lifestyle.Scoped);
        }
    }
}