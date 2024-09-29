using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;

namespace AppPuntoVentaG2
{
    public partial class FrmMantUsuarios : Form
    {
        //variable para manejar la conexión con el servidor
        private Conexion _conexion = null;

        //variable para almacenar los datos del usuario
        private Usuario _usuario = null;

        public FrmMantUsuarios()
        {
            InitializeComponent();
            //se intancia la conexión y se enviar como parámetro el string de conexión
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();//cerrar el formulario
        }

        private void btnAcciones_Click(object sender, EventArgs e)
        {
            try
            {
                //se valida la función del botón
                if (this.btnAcciones.Text.Equals("Modificar"))
                {
                    //aquí se realiza el proceso de modificar los datos
                    ModificarUsuario();
                    //se muestra un mensaje al usuario que se almaceno correctamente
                    MessageBox.Show("Datos actualizado correctamente", "Proceso realizado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { ///proceso de guardar
                    GuardarUsuario();//se utiliza el método

                    //se muestra un mensaje al usuario que se almaceno correctamente
                    MessageBox.Show("Usuario registrado correctamente", "Ingreso realizado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();//se cierra el formulario

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarUsuario()
        {
            try
            {
                //se instancia el usuario
                this._usuario = new Usuario();

                //se rellenan los datos 
                this._usuario.Email = this.txtEmail.Text.Trim();
                this._usuario.NombreCompleto = this.txtNombreCompleto.Text.Trim();
                this._usuario.Password = this.txtPassword.Text.Trim();

                if (this._usuario.ConfirmarPassword(this.txtConfirmar.Text.Trim()))
                {
                    _conexion.GuardarUsuario(this._usuario);
                }
                else
                {
                    throw new Exception("La confirmación del password es incorrecto");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FrmMantUsuarios_Load(object sender, EventArgs e)
        {

        }

        public void FuncionRealizar(int pFunc)
        {    //se valida que función va ha realizar el botón
            if (pFunc == 1)
            {   //se cambian el texto del botón
                this.btnAcciones.Text = "Modificar";
            }
        }

        private void ModificarUsuario()
        {
            try
            {
                //se instancia el usuario
                this._usuario = new Usuario();

                //se rellenan los datos 
                this._usuario.Email = this.txtEmail.Text.Trim();
                this._usuario.NombreCompleto = this.txtNombreCompleto.Text.Trim();
                this._usuario.Password = this.txtPassword.Text.Trim();

                if (this._usuario.ConfirmarPassword(this.txtConfirmar.Text.Trim()))
                {  //proceso de modificar
                    _conexion.ModificarUsuario(this._usuario);
                }
                else
                {
                    throw new Exception("La confirmación del password es incorrecto");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    } //cierre formulario
} //cierre namespaces
