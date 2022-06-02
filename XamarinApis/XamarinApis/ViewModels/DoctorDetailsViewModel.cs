using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Helpers;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.ViewModels
{
    public class DoctorDetailsViewModel:ViewModelBase
    {
        //  Creamos la propiedad privada de service y helper:
        private ServiceApiDoctores _Service;
        private HelperUtilities _Helper;
        //  Creamos el constructor del viewmodel:
        public DoctorDetailsViewModel(ServiceApiDoctores service, HelperUtilities helper)
        {
            this._Service = service;
            this._Helper = helper;
        }
        //  Creamos la propiedad privada y publica de doctor:
        private Doctor _Doctor;
        public Doctor Doctor
        {
            get { return this._Doctor; }
            set
            {
                this._Doctor = value;
                this._Doctor.Favorito = this._Helper.IsFavoriteDoctor(value.IdDoctor);
                OnPropertyChanged("Doctor");
            }
        }
        //  Comando que nos permitira eliminar un doctor:
        public Command DeleteDoctor
        {
            get
            {
                return new Command(async () =>
                {
                    await this._Service.DeleteDoctorAsync(this.Doctor.IdDoctor);
                    //  Para llamar al subscriber y recargar los doctores:
                    //  (lo que estamos haciendo es llamar al viewmodel, recogerlo desde serviceioc y usar el metodo RELOADDOCTORES):
                    MessagingCenter.Send<DoctoresListViewModel>(App.ServiceLocator.DoctoresListViewModel, "RELOADDOCTORES");
                    //  Ponemos un mensajito para hacerlo mas ameno:
                    await Application.Current.MainPage.DisplayAlert("Alert", "Doctor " + Doctor.Apellido + " eliminado.", "OK");
                    //  Devolvemos la vista main:
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }
        //  Comando que nos permitira seleccionar un favorito:
        public Command SeleccionarFavorito
        {
            get
            {
                return new Command(async () =>
                {
                    //  Buscamos la clase session y añadimos el doctor:
                    ServiceSession session = App.ServiceLocator.ServiceSession;
                    session.Favoritos.Add(this.Doctor);
                    //  Mostramos un mensaje en el main:
                    await Application.Current.MainPage.DisplayAlert("Alert", "Doctor almacenado", "OK");
                });
            }
        }
    }
}
