using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using XamarinApis.Helpers;
using XamarinApis.ViewModels;

namespace XamarinApis.Services
{
    public class ServiceIoC
    {
        private IContainer _Container;
        public ServiceIoC()
        {
            this.RegisterDependencies();
        }
        private void RegisterDependencies()
        {
            //  Esta parte es para poder configurar services y viewmodels:
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ServiceApiDoctores>();
            builder.RegisterType<ServiceSession>().SingleInstance();
            builder.RegisterType<HelperUtilities>();
            builder.RegisterType<DoctoresListViewModel>();
            builder.RegisterType<DoctorDetailsViewModel>();
            builder.RegisterType<DoctoresFavoritosViewModel>();
            //  Esta parte es para poder configurar appsettings:
            string resourceName = "XamarinApis.appsettings.json";
            Stream stream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream(resourceName);
            IConfiguration configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Register<IConfiguration>(x => configuration);
            //  Final del metodo:
            this._Container = builder.Build();
        }
        //  Necesitamos una propiedad para recuperar la session desde cualquier lugar:
        public ServiceSession ServiceSession
        {
            get
            {
                return this._Container.Resolve<ServiceSession>();
            }
        }
        //  Cuando indicamos un viewmodel en el registro de dependencias, hay que 'construirlas':
        public DoctoresListViewModel DoctoresListViewModel
        {
            get
            {
                return this._Container.Resolve<DoctoresListViewModel>();
            }
        }
        public DoctorDetailsViewModel DoctorDetailsViewModel
        {
            get
            {
                return this._Container.Resolve<DoctorDetailsViewModel>();
            }
        }
        public DoctoresFavoritosViewModel DoctoresFavoritosViewModel
        {
            get
            {
                return this._Container.Resolve<DoctoresFavoritosViewModel>();
            }
        }
    }
}
