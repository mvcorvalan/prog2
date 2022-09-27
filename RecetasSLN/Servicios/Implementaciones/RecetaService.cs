using RecetasSLN.datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecetasSLN.datos.Implementaciones;
using RecetasSLN.Servicios.Interfaces;
using System.Data;

namespace RecetasSLN.Servicios.Implementaciones
{
    internal class RecetaService:IService
    {
        private IRecetaDao oDao;
        public RecetaService()
        {
            oDao = new RecetaDao();
        }
        //public DataTable GetTiposRecetas()
        //{
        //    return oDao.GetTiposRecetas();
        //}
        public DataTable GetIngredientes()
        {
            return oDao.GetIngredientes();
        }

        public int GetProximaReceta()
        {
            return oDao.GetProximaReceta();
        }
    }
}
