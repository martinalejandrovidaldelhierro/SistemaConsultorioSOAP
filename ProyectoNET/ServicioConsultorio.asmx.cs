using SistemaConsultorioSOAP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace SistemaConsultorioSOAP
{
    /// <summary>
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ServicioConsultorio : System.Web.Services.WebService
    {
        [WebMethod]
        public List<Paciente> ObtenerPacientes()
        {
            
            List<Paciente> listaPacientes = new List<Paciente>();

            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = "SELECT IdPaciente, Cedula, Nombre, Apellido, Telefono, Estado FROM Paciente";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    
                    con.Open();

                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            Paciente p = new Paciente();

                            p.IdPaciente = Convert.ToInt32(reader["IdPaciente"]);
                            p.Cedula = reader["Cedula"].ToString();
                            p.Nombre = reader["Nombre"].ToString();
                            p.Apellido = reader["Apellido"].ToString();
                            p.Telefono = reader["Telefono"].ToString();
                            p.Estado = Convert.ToBoolean(reader["Estado"]);

                            
                            listaPacientes.Add(p);
                        }
                    }
                }
            }
            return listaPacientes;
        }
        // ---------------- METODOS DE CITAS ----------------

        [WebMethod]
        public List<Cita> ObtenerCitas()
        {
            List<Cita> listaCitas = new List<Cita>();
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = "SELECT IdCita, Fecha, Hora, Motivo, Tratamiento, Estado, IdPaciente FROM Cita";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cita c = new Cita();
                            c.IdCita = Convert.ToInt32(reader["IdCita"]);
                            c.Fecha = Convert.ToDateTime(reader["Fecha"]);
                            c.Hora = Convert.ToDateTime(reader["Hora"]);
                            c.Motivo = reader["Motivo"].ToString();
                            c.Tratamiento = reader["Tratamiento"] != DBNull.Value ? reader["Tratamiento"].ToString() : string.Empty;
                            c.Estado = Convert.ToBoolean(reader["Estado"]);
                            c.IdPaciente = Convert.ToInt32(reader["IdPaciente"]);

                            listaCitas.Add(c);
                        }
                    }
                }
            }
            return listaCitas;
        }

        [WebMethod]
        public bool AgregarCita(DateTime fecha, DateTime hora, string motivo, string tratamiento, bool estado, int idPaciente)
        {
            bool exito = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = @"INSERT INTO Cita (Fecha, Hora, Motivo, Tratamiento, Estado, IdPaciente) 
                             VALUES (@Fecha, @Hora, @Motivo, @Tratamiento, @Estado, @IdPaciente)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cmd.Parameters.AddWithValue("@Hora", hora);
                    cmd.Parameters.AddWithValue("@Motivo", motivo);
                    cmd.Parameters.AddWithValue("@Tratamiento", tratamiento);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);

                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }
            return exito;
        }

        [WebMethod]
        public bool EliminarCita(int idCita)
        {
            bool exito = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Cita WHERE IdCita = @IdCita";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdCita", idCita);

                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }
            return exito;
        }
        [WebMethod]
        public Cita ObtenerCita(int idCita)
        {
            Cita c = new Cita();
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IdCita, Fecha, Hora, Motivo, Tratamiento, Estado, IdPaciente FROM Cita WHERE IdCita = @IdCita";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdCita", idCita);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            c.IdCita = Convert.ToInt32(reader["IdCita"]);
                            c.Fecha = Convert.ToDateTime(reader["Fecha"]);
                            c.Hora = Convert.ToDateTime(reader["Hora"]);
                            c.Motivo = reader["Motivo"].ToString();
                            c.Tratamiento = reader["Tratamiento"] != DBNull.Value ? reader["Tratamiento"].ToString() : string.Empty;
                            c.Estado = Convert.ToBoolean(reader["Estado"]);
                            c.IdPaciente = Convert.ToInt32(reader["IdPaciente"]);
                        }
                    }
                }
            }
            return c;
        }

        [WebMethod]
        public bool ActualizarCita(int idCita, DateTime fecha, DateTime hora, string motivo, string tratamiento, bool estado, int idPaciente)
        {
            bool exito = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Cita SET Fecha = @Fecha, Hora = @Hora, Motivo = @Motivo, 
                             Tratamiento = @Tratamiento, Estado = @Estado, IdPaciente = @IdPaciente 
                             WHERE IdCita = @IdCita";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdCita", idCita);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cmd.Parameters.AddWithValue("@Hora", hora);
                    cmd.Parameters.AddWithValue("@Motivo", motivo);
                    cmd.Parameters.AddWithValue("@Tratamiento", tratamiento);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);

                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }
            return exito;
        }

        [WebMethod]
        public List<Cita> ObtenerCitaPorTratamiento(string tratamiento)
        {
            List<Cita> lista = new List<Cita>();
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = "SELECT IdCita, Fecha, Hora, Motivo, Tratamiento, Estado, IdPaciente FROM Cita WHERE Tratamiento LIKE '%' + @Tratamiento + '%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Tratamiento", tratamiento);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cita c = new Cita();
                            c.IdCita = Convert.ToInt32(reader["IdCita"]);
                            c.Fecha = Convert.ToDateTime(reader["Fecha"]);
                            c.Hora = Convert.ToDateTime(reader["Hora"]);
                            c.Motivo = reader["Motivo"].ToString();
                            c.Tratamiento = reader["Tratamiento"] != DBNull.Value ? reader["Tratamiento"].ToString() : string.Empty;
                            c.Estado = Convert.ToBoolean(reader["Estado"]);
                            c.IdPaciente = Convert.ToInt32(reader["IdPaciente"]);
                            lista.Add(c);
                        }
                    }
                }
            }
            return lista;
        }

        [WebMethod]
        public List<Cita> ObtenerCitaPorCedula(string cedula)
        {
            List<Cita> lista = new List<Cita>();
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = @"SELECT c.IdCita, c.Fecha, c.Hora, c.Motivo, c.Tratamiento, c.Estado, c.IdPaciente 
                             FROM Cita c 
                             INNER JOIN Paciente p ON c.IdPaciente = p.IdPaciente 
                             WHERE p.Cedula = @Cedula";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Cedula", cedula);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cita c = new Cita();
                            c.IdCita = Convert.ToInt32(reader["IdCita"]);
                            c.Fecha = Convert.ToDateTime(reader["Fecha"]);
                            c.Hora = Convert.ToDateTime(reader["Hora"]);
                            c.Motivo = reader["Motivo"].ToString();
                            c.Tratamiento = reader["Tratamiento"] != DBNull.Value ? reader["Tratamiento"].ToString() : string.Empty;
                            c.Estado = Convert.ToBoolean(reader["Estado"]);
                            c.IdPaciente = Convert.ToInt32(reader["IdPaciente"]);
                            lista.Add(c);
                        }
                    }
                }
            }
            return lista;
        }
    }
}