using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    internal class Receta
    {
        public int IdReceta { get; set; }
        public string Nombre { get; set; }
        public int TipoReceta { get; set; }
        public string Cheff { get; set; }
        public List<DetalleReceta> lDetalles { get; set; }
        public Receta()
        {
            lDetalles= new List<DetalleReceta>();
        }
        public void AgregarDetalle(DetalleReceta detalle)
        {
            lDetalles.Add(detalle);
        }
        public void QuitarDetalle(int indice)
        {
            lDetalles.RemoveAt(indice);
        }
        public double CalcularTotal()
        {
            double total = 0;
            foreach(DetalleReceta detalle in lDetalles)
            {
                total += detalle.Cantidad;
            }
            return total;
        }
    }
}
