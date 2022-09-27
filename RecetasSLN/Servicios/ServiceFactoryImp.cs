using RecetasSLN.Servicios.Implementaciones;
using RecetasSLN.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Servicios
{
    internal class ServiceFactoryImp:AbstractServiceFactory
    {
        public override IService CrearService()
        {
            return new RecetaService();
        }

       
    }
}
