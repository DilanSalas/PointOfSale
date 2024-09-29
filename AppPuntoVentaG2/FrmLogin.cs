using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//importa la libreria BLL
using BLL;
using DAL;

//Libreria permite utilizar los datos almacenados dentro del archivo App.Config
using System.Configuration;

namespace AppPuntoVentaG2
{
    public partial class FrmLogin : Form
    {
        //Variable de tipo  Objeto
        private Usuario  objUsuario =  null;

        //Variable para manejar la conexion
        private Conexion conexion = null;

        public FrmLogin()
        {
            InitializeComponent();

            //Se instancia la conexión enviando como parámetro el string de conexion almacenado dentro del App.Config
            conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IntentoAutenticacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0); //Finaliza la aplicación
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private  void IntentoAutenticacion()
        {
            try
            {
                //se crea una instancia de tipo usuario
                objUsuario = new Usuario();

                //Se rellena el objeto con los datos ingresados en el front-end
                objUsuario.Email = this.txtUsuario.Text.Trim();
                objUsuario.Password = this.txtPassword.Text.Trim();

                //Se realiza la validación de credenciales
                if ( conexion.ValidarUsuario(objUsuario.Email, objUsuario.Password ) != null)
                {
                    this.Close();// todo bien las credenciales, cerramos el formulario
                }
                else
                {
                    throw new Exception("Usuario o password incorrectos...");
                }

            }
            catch (Exception ex)
            {
                throw ex; //se retorna el error al evento que invoca al método
            }
        }

    } //cierre formulario
} //cierre namespaces
