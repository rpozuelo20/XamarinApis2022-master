using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Code;
using XamarinApis.Views;

namespace XamarinApis.ViewModels
{
    public class MainDoctoresViewModel:ViewModelBase
    {
        public MainDoctoresViewModel()
        {
            ObservableCollection<MasterPageItem> menu = new ObservableCollection<MasterPageItem>
            {
                new MasterPageItem
                {
                    Titulo="Doctores",
                    Tipo=typeof(DoctoresView),
                    Icono="img1"
                },
                new MasterPageItem
                {
                    Titulo="Favoritos",
                    Tipo=typeof(DoctoresFavoritosView),
                    Icono="img2"
                },
                new MasterPageItem
                {
                    Titulo="Nuevo doctor",
                    Tipo=typeof(NuevoDoctorView),
                    Icono="img3"
                }
            };
            this.MenuPrincipal = menu;
        }

        public Command PaginaSeleccionada
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Estoy aqui", "OK");
                    MainDoctores masterPage = App.ServiceLocator.MainDoctores;
                    ListView lsv = (ListView)masterPage.FindByName("lsvMenu");
                    var item = (MasterPageItem)lsv.SelectedItem;
                    var type = item.Tipo;
                    masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(type));
                    masterPage.IsPresented = false;
                });
            }
        }

        private ObservableCollection<MasterPageItem> _MenuPrincipal;
        public ObservableCollection<MasterPageItem> MenuPrincipal
        {
            get { return this._MenuPrincipal; }
            set
            {
                this._MenuPrincipal = value;
                OnPropertyChanged("MenuPrincipal");
            }
        }
    }
}
