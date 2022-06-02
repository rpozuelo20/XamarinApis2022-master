using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XamarinApis.Base;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.ViewModels
{
    public class DoctoresFavoritosViewModel:ViewModelBase
    {
        //  En el constructor decimos que 'this.DoctoresFavoritos' va a ser una nueva coleccion de 'Doctor'/'serviceSession.Favoritos':
        public DoctoresFavoritosViewModel(ServiceSession serviceSession)
        {
            this.DoctoresFavoritos = new ObservableCollection<Doctor>(serviceSession.Favoritos);
        }
        //  Creamos la propiedad de privada y publica de 'DoctoresFavoritos':
        private ObservableCollection<Doctor> _DoctoresFavoritos;
        public ObservableCollection<Doctor> DoctoresFavoritos
        {
            get { return this._DoctoresFavoritos; }
            set
            {
                this._DoctoresFavoritos = value;
                OnPropertyChanged("DoctoresFavoritos");
            }
        }
    }
}
