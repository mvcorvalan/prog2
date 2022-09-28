using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using RecetasSLN.dominio;

namespace RecetasSLN.datos
{
    internal class HelperDao
    {
        private static HelperDao instancia;
        private string cadenaConexion;

        private HelperDao()
        {
            cadenaConexion = @"Data Source=LAPTOP-M9NS5N6F\SQLEXPRESS01;Initial Catalog=recetas_db;Integrated Security=True";
        }
        public static HelperDao ObtenerInstancia()
        {
            if(instancia==null)
            {
                instancia=new HelperDao();
            }
            return instancia;
        }
        public DataTable ConsultarDB(string sp)
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = sp;
            dt.Load(comando.ExecuteReader());
            conexion.Close();

            return dt;
        }
        public int ConsultarProximo(string sp, string parametro)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = sp;

            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = parametro;
            pOut.Direction = ParameterDirection.Output;
            pOut.DbType = DbType.Int32;
            comando.Parameters.Add(pOut);

            comando.ExecuteNonQuery();

            conexion.Close();
            return (int)pOut.Value;

        }
        public bool ConfirmarReceta(Receta oReceta)
        {
            bool resultado = true;
            SqlConnection conexion = new SqlConnection();
            SqlTransaction t = null;

            //try
            //{
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();
                t=conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand("SP_INSERTAR_RECETA",conexion,t);
                comando.CommandType= CommandType.StoredProcedure;

            //comando.Parameters.AddWithValue("@id_receta", oReceta.IdReceta);
                comando.Parameters.AddWithValue("@nombre", oReceta.Nombre);
                comando.Parameters.AddWithValue("@cheff", oReceta.Cheff);
                comando.Parameters.AddWithValue("@tipo_receta", oReceta.TipoReceta);

                SqlParameter parametro = new SqlParameter("id_receta", DbType.Int32);
                parametro.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parametro);

                comando.ExecuteNonQuery();

                int id_receta = Convert.ToInt32(parametro.Value);
                
                foreach(DetalleReceta detalle in oReceta.lDetalles)
                {
                    SqlCommand cmddetalles = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);
                    comando.CommandType=CommandType.StoredProcedure;

                    cmddetalles.Parameters.AddWithValue("id_receta", id_receta);
                    cmddetalles.Parameters.AddWithValue("id_ingrediente", detalle.Ingrediente.IdIngrediente);
                    cmddetalles.Parameters.AddWithValue("cantidad", detalle.Cantidad);

                    cmddetalles.ExecuteNonQuery();
                }
                t.Commit();
            //}
            //catch(Exception)
            //{
            //    t.Rollback();
            //    resultado = false;
            //}
            //finally
            //{
                if(conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            //}
            return resultado;
        }
    }
}
