using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.ViewModels
{
    public class NuevoDoctorViewModel:ViewModelBase
    {
        private ServiceApiDoctores _Service;
        public NuevoDoctorViewModel(ServiceApiDoctores service)
        {
            this._Service = service;
            this.Doctor = new Doctor();
        }

        private Doctor _Doctor;
        public Doctor Doctor
        {
            get { return this._Doctor; }
            set
            {
                this._Doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        public Command InsertarDoctor
        {
            get
            {
                return new Command(async () =>
                {
                    await this._Service.CreateDoctor(this.Doctor.Apellido, this.Doctor.Especialidad, this.Doctor.Salario, this.Doctor.IdHospital);
                    await Application.Current.MainPage.DisplayAlert("Alert", "Doctor creado.", "OK");
                });
            }
        }
    }
}
