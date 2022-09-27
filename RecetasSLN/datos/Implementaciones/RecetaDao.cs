using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RecetasSLN.datos.Interfaces;
using RecetasSLN.dominio;

namespace RecetasSLN.datos.Implementaciones
{
    internal class RecetaDao:IRecetaDao
    {
        public DataTable GetIngredientes()
        {
            return HelperDao.ObtenerInstancia().ConsultarDB("SP_CONSULTAR_INGREDIENTES");
        }
        public int GetProximaReceta()
        {
            return HelperDao.ObtenerInstancia().ConsultarProximo("SP_PROXIMA_RECETA", "@next");
        }
        //public bool ConfirmarReceta()
        //{
        //    //return HelperDao.ObtenerInstancia().ConfirmarReceta(Receta receta);
        //}
        //public DataTable GetTiposRecetas()
        //{
        //    return HelperDao.ObtenerInstancia().ConsultarDB("SP_CONSULTAR_INGREDIENTES");
        //}
    }
}
