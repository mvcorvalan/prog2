using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RecetasSLN.datos.Interfaces
{
    internal interface IRecetaDao
    {
        DataTable GetIngredientes();
        int GetProximaReceta();
        //bool ConfirmarReceta();
        //DataTable GetTiposRecetas();
    }
}
