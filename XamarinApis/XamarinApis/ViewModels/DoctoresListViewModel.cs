using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Helpers;
using XamarinApis.Models;
using XamarinApis.Services;
using XamarinApis.Views;

namespace XamarinApis.ViewModels
{
    public class DoctoresListViewModel:ViewModelBase
    {
        //  Creamos la propiedad privada de service:
        private ServiceApiDoctores _Service;
        private HelperUtilities _Helper;
        //  Creamos el constructor del viewmodel:
        public DoctoresListViewModel(ServiceApiDoctores service, HelperUtilities helper)
        {
            this._Service = service;
            this._Helper = helper;
            //  ¿Como llamamos a un metodo async en un constructor?
            Task.Run(async () =>
            {
                await this.LoadDoctoresAsync();
            });
            //  ¿Como subscribimos en distintos view/viewmodel, por ejemplo, para recargar una vista realizando una accion en otra vista?
            //  this, significa que afecta a este viewmodel, RELOADDOCTORES es el nombre del subscribe:
            MessagingCenter.Subscribe<DoctoresListViewModel>(this, "RELOADDOCTORES", async (sender) =>
            {
                await this.LoadDoctoresAsync();
            });
        }
        //  Creamos la propiedad privada y publica de doctores:
        private ObservableCollection<Doctor> _Doctores;
        public ObservableCollection<Doctor> Doctores
        {
            get { return this._Doctores; }
            set
            {
                this._Doctores = value;
                OnPropertyChanged("Doctores");
            }
        }
        //  Comando que nos permitira mostrar los doctores:
        public Command MostrarDoctores
        {
            get
            {
                return new Command(async () =>
                {
                    await this.LoadDoctoresAsync();
                });
            }
        }
        //  Comando que nos permitira mostrar los detalles del doctor:
        public Command ShowDoctor
        {
            get
            {
                return new Command(async (idDoctor) =>
                {
                    Doctor doctor = await this._Service.FindDoctor((int)idDoctor);
                    //  Creamos la vista detalles del doctor:
                    DoctorDetailsView view = new DoctorDetailsView();
                    //  Creamos el viewmodel del doctor:
                    DoctorDetailsViewModel viewmodel = App.ServiceLocator.DoctorDetailsViewModel;
                    //  Pasamos los datos al viewmodel del doctor:
                    viewmodel.Doctor = doctor;
                    //  Enlazamos la vista detalles con el viewmodel del doctor:
                    view.BindingContext = viewmodel;
                    //  Mostramos la vista detalles del doctor:
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
        //  Comando que nos permitira eliminar el doctor:
        public Command DeleteDoctor
        {
            get
            {
                return new Command(async (idDoctor) =>
                {
                    int id = (int)idDoctor;
                    await this._Service.DeleteDoctorAsync(id);
                    await this.LoadDoctoresAsync();
                });
            }
        }
        //  Metodo para cargar doctores:
        private async Task LoadDoctoresAsync()
        {
            List<Doctor> data = await this._Service.GetDoctoresAsync();
            foreach(Doctor doc in data)
            {
                doc.Favorito = this._Helper.IsFavoriteDoctor(doc.IdDoctor);
            }
            this.Doctores = new ObservableCollection<Doctor>(data);
        }
        //  Comando que nos permitira seleccionar un favorito:
        public Command SeleccionarFavorito
        {
            get
            {
                return new Command(async(doctor) =>
                {
                    ServiceSession session = App.ServiceLocator.ServiceSession;
                    session.Favoritos.Add(doctor as Doctor);
                    await this.LoadDoctoresAsync();
                    await Application.Current.MainPage.DisplayAlert("Alert", "Doctor almacenado", "OK");
                });
            }
        }
        //  Comando que nos permitira redirigirnos a la vista de favoritos:
        public Command MostrarFavoritos
        {
            get
            {
                return new Command(async () =>
                {
                    DoctoresFavoritosView view = new DoctoresFavoritosView();
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
