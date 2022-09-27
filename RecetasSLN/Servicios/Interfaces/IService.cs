using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RecetasSLN.Servicios.Interfaces
{
    internal interface IService
    {
        DataTable GetIngredientes();
        int GetProximaReceta();
        //DataTable GetTiposRecetas();
    }
}
