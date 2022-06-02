using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.Helpers
{
    public class HelperUtilities
    {
        //  Desde el helper necesitamos buscar el doctor en session de favoritos y devolver si es favorito o no:
        private ServiceSession _ServiceSession;
        public HelperUtilities(ServiceSession serviceSession)
        {
            this._ServiceSession = serviceSession;
        }

        public bool IsFavoriteDoctor(int id)
        {
            Doctor doctor = this._ServiceSession.Favoritos.FirstOrDefault(x => x.IdDoctor == id);
            if (doctor == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
