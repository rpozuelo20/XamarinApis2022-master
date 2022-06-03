using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApis.Code;
using XamarinApis.Views;

namespace XamarinApis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDoctores : MasterDetailPage
    {
        public MainDoctores()
        {
            InitializeComponent();
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
            this.lsvMenu.ItemsSource = menu;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)));
            this.lsvMenu.ItemSelected += LsvMenu_ItemSelected;
        }

        private void LsvMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type type = item.Tipo;
            Detail = new NavigationPage((Page)Activator.CreateInstance(type));
            IsPresented = false;
        }
    }
}