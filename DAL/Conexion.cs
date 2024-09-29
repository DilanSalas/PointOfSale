using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//driver para SQL Server  ADO.net
using System.Data.SqlClient;

//Permite utilizar los objetos de la capa logica negocio
using BLL;
using System.Data;

namespace DAL
{
    public class Conexion
    {
        //Objetos para interactuar con el servidor de base datos
        private string StringConexion;

        //Variable para manejar la referencia para la conexión
        private SqlConnection _connection; 

        //Variable para ejecutar transac-sql del lado del servidor db
        private SqlCommand _command;

        //Esta variable permite ejecutar transac-sql de consulta
        private SqlDataReader _reader;


        /// <summary>
        /// Constructor con parámetros recibe el string de conexión
        /// </summary>
        /// <param name="pStringCnx"></param>
        public Conexion(string pStringCnx)
        {
            //ojo se asignan los datos de la conexion
                StringConexion = pStringCnx;
        }


        public Usuario ValidarUsuario(string pEmail,string pPassword)
        {
            try
            {
                //se intancia la conexion
                _connection = new SqlConnection(StringConexion);
                
                //se intanta abrir la conexion
                _connection.Open();

                //se instancia el comando
                _command = new SqlCommand();

                //se asignan la conexion al comando
                _command.Connection = _connection;  

                //Se indica el tipo de comando
                _command.CommandType = System.Data.CommandType.StoredProcedure;

                //se indica el nombre del procedimiento almacenado a ejecutar
                _command.CommandText = "Sp_Cns_Usuario";

                //Se asigna a cada parámetro valor
                _command.Parameters.AddWithValue("pEmail",pEmail); //se indica el parámetro y se asigna su valor
                _command.Parameters.AddWithValue("pPw",pPassword);//se indica el parámetro y se asigna su valor

                //Se ejecuta el comando
                _reader = _command.ExecuteReader();

                //Variable para almacenar los datos de la base datos
                Usuario temp = null;

                //se valida si hay datos
                if (_reader.Read())
                {
                    //Si hay datos se rellena el objeto con los datos
                    temp = new Usuario();

                    temp.Email = _reader.GetValue(0).ToString();
                    temp.NombreCompleto = _reader.GetValue(1).ToString();
                    temp.Password = _reader.GetValue(2).ToString();
                    temp.FechaRegistro = DateTime.Parse(_reader.GetValue(3).ToString());
                    temp.Estado = char.Parse( _reader.GetValue(4).ToString());  
                }
                //Se debe cerrar la conexion  siempre
                _connection.Close();

                //Se liberan los recursos 
                _connection.Dispose();
                _command.Dispose(); 

                return temp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataSet BuscarUsuarios(string nombre)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);//Se intancia una conexión
                _connection.Open();//se abre la conexión

                _command = new SqlCommand(); //se instancia el comando
                _command.Connection = _connection; //se asigna la conexión al comando
                _command.CommandType = CommandType.StoredProcedure; //se indica el tipo de comando
                _command.CommandText = "[Sp_Buscar_Usuarios]"; //se indica el procedimiento
                _command.Parameters.AddWithValue("@Nombre",nombre); //se asigna el valor al parámetro

                SqlDataAdapter adapter = new SqlDataAdapter(); //se instancia un adaptador
                DataSet datos = new DataSet(); //Objeto permite almacenar una tabla de datos
                adapter.SelectCommand = _command; //se asigna el comando al adaptador
                adapter.Fill(datos); //se rellena el dataset con todos los datos de la table

                _connection.Close(); // ojo siempre se debe cerrar el comando
                _connection.Dispose();   //se librera la memoria de la conexión 
                _command.Dispose(); //se libera la memoria del comando
                adapter.Dispose(); //se libera la memoria del adaptador
                //se retorna todos los datos tabulados
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarUsuario(string email)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Del_Usuario]";
                _command.Parameters.AddWithValue("@Email",email);

                _command.ExecuteNonQuery(); //Solamente para ejecutar
                
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void GuardarUsuario(Usuario usuario)
        {
            try
            {
                _connection = new SqlConnection(StringConexion); //se intancia la conexión
                _connection.Open(); //se abre la conexión
                _command = new SqlCommand(); //se instancia el comando
                _command.Connection = _connection; //se asigna la conexión
                _command.CommandType = CommandType.StoredProcedure; //se indica el tipo de comando
                _command.CommandText = "[Sp_Ins_Usuarios]";  //se asigna el nombre del procedimiento almacenado
                _command.Parameters.AddWithValue("@Email",usuario.Email);  //se asigna el valor del email
                _command.Parameters.AddWithValue("@NombreComp",usuario.NombreCompleto); //se asigna el valor del nombre completo
                _command.Parameters.AddWithValue("@Passwd",usuario.Password); //se asigna el valor del password

                _command.ExecuteNonQuery (); //Ojo se ejecuta el comando
                _connection.Close(); //se cierra la conexión 
                _connection.Dispose(); //se liberan los recursos
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarUsuario(Usuario usuario)
        {
            try
            {
                _connection = new SqlConnection(StringConexion); //se intancia la conexión
                _connection.Open(); //se abre la conexión
                _command = new SqlCommand(); //se instancia el comando
                _command.Connection = _connection; //se asigna la conexión
                _command.CommandType = CommandType.StoredProcedure; //se indica el tipo de comando
                _command.CommandText = "[Sp_Upd_Usuarios]";  //se asigna el nombre del procedimiento almacenado
                _command.Parameters.AddWithValue("@Email", usuario.Email);  //se asigna el valor del email
                _command.Parameters.AddWithValue("@NombreComp", usuario.NombreCompleto); //se asigna el valor del nombre completo
                _command.Parameters.AddWithValue("@Passwd", usuario.Password); //se asigna el valor del password

                _command.ExecuteNonQuery(); //Ojo se ejecuta el comando
                _connection.Close(); //se cierra la conexión 
                _connection.Dispose(); //se liberan los recursos
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }//cierre class
} //cierre namespaces
