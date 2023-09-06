using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Game
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        public void AgregarVideojuego(E_Game objJuego)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();

                //Como utlizar STORED PROCEDURE

                //Se necesita un objeto de SqlCommand y le pasamos el nobre del SP y la conexion 
                SqlCommand comando = new SqlCommand("spAgregarJuegos", conexion);

                //Indicamos que vamos a usar SP
                comando.CommandType = CommandType.StoredProcedure;

                //Agregar parametros
                
                comando.Parameters.AddWithValue("@nombre", objJuego.nombre);
                comando.Parameters.AddWithValue("@fechaLanzamiento", objJuego.fechaLanzamiento);
                comando.Parameters.AddWithValue("@precio", objJuego.precio);
                comando.Parameters.AddWithValue("@imagen", objJuego.imagen);

                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

        public List<E_Game> ObtenerTodos()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            List<E_Game> lista = new List<E_Game>();
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("spObtenerJuegos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    E_Game objeto = new E_Game();
                    objeto.idVideojuego = Convert.ToInt32(reader["idVideojuego"]);
                    objeto.nombre = reader["nombre"].ToString();
                    objeto.fechaLanzamiento = Convert.ToDateTime(reader["fechaLanzamiento"]);
                    objeto.precio = Convert.ToDecimal(reader["precio"]);
                    objeto.imagen = reader["imagen"].ToString();

                    lista.Add(objeto);

                }
                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

        public E_Game ObtenerVideojuegoPorId(int idJuego)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("spObtenerJuegoPorId", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idVideojuego",idJuego);

                SqlDataReader reader = comando.ExecuteReader();

                reader.Read();

                E_Game objeto = new E_Game();
                objeto.idVideojuego = Convert.ToInt32(reader["idVideojuego"]);
                objeto.nombre = reader["nombre"].ToString();
                objeto.fechaLanzamiento = Convert.ToDateTime(reader["fechaLanzamiento"]);
                objeto.precio = Convert.ToDecimal(reader["precio"]);
                objeto.imagen = reader["imagen"].ToString();
                conexion.Close();

                return objeto;

            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

        public void GuardarEdicion(E_Game objJuego)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("spEditarJuegos", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@idVideojuego", objJuego.idVideojuego);
                comando.Parameters.AddWithValue("@nombre", objJuego.nombre);
                comando.Parameters.AddWithValue("@fechaLanzamiento", objJuego.fechaLanzamiento);
                comando.Parameters.AddWithValue("@precio", objJuego.precio);
                comando.Parameters.AddWithValue("@imagen", objJuego.imagen);

                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

        public void EliminarVideojuego (int idJuego)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("spBorrarJuegos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idVideojuego", idJuego);

                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }


        public E_Game BuscarJuegoPorNombre(string nombreJuego)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            E_Game juego = new E_Game();
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("spBuscarJuegos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@nombre", nombreJuego);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    juego.idVideojuego = Convert.ToInt32(reader["idVideojuego"]);
                    juego.nombre = reader["nombre"].ToString();
                }

                conexion.Close();
                return juego;
            }

            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

    }
}
