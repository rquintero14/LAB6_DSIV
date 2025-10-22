using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio6
{
    internal class DatosLibros 
    {
        // Cadena de conexión cambiar según configuración local
        private string cadenaConexion =
            "Data Source=LAPTOP-ALE;Initial Catalog=Libreria;Integrated Security=True";

        public int Insertar(Libros libros)
        {
            int idLibro = 0;

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("PA_RegistrarLibro", conexion))
                    {

                        comando.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros
                        comando.Parameters.AddWithValue("@titulo", libros.Titulo);
                        comando.Parameters.AddWithValue("@genero", libros.Genero);
                        comando.Parameters.AddWithValue("@autor", libros.Autor);
                        comando.Parameters.AddWithValue("@cantidadDisp", libros.CantidadDisp);

                        // Ejecutar y obtener el ID generado
                        object resultado = comando.ExecuteScalar();

                        if (resultado != null)
                        {
                            idLibro = Convert.ToInt32(resultado);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar libro: " + ex.Message);
            }

            return idLibro;
        }


        public List<Libros> ObtenerTodos()
        {
            List<Libros> lista = new List<Libros>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("PA_MostrarTodosLosLibros", conexion))
                    {

                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Libros lib = new Libros
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Titulo = reader["Titulo"].ToString(),
                                    Genero = reader["Genero"].ToString(),
                                    Autor = reader["Autor"].ToString(),
                                    CantidadDisp = Convert.ToInt32(reader["CantidadDisp"])
                                };

                                lista.Add(lib);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener los libros: " + ex.Message);
            }

            return lista;
        }


        public List<Libros> ObtenerPorGenero(string genero)
        {
            List<Libros> lista = new List<Libros>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("PA_LibrosYConteoPorGenero", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@genero", genero);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Libros lib = new Libros
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Titulo = reader["Titulo"].ToString(),
                                    Genero = reader["Genero"].ToString(),
                                    Autor = reader["Autor"].ToString(),
                                    CantidadDisp = Convert.ToInt32(reader["CantidadDisp"])
                                };

                                lista.Add(lib);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener los libros por genero: " + ex.Message);
            }

            return lista;
        }
    }
    
}
