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
namespace AppPuntoVentaG2
{
    public partial class FrmBuscarUsuarios : Form
    {
        private Conexion _conexion = null;
        public FrmBuscarUsuarios()
        {
            InitializeComponent();
            //Se intancia el objeto  y se envia como parámetro el string de conexión almacenado dentro del archivo Appconfig
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //se llama al método de buscat
                Buscar(this.txtNombre.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Buscar(string pNombre)
        {
            try
            {
                //OJO se utiliza la clase conexión  para buscar los usuarios en la db
                this.dtgDatos.DataSource = _conexion.BuscarUsuarios(pNombre).Tables[0];
                this.dtgDatos.AutoResizeColumns();//se auto ajustan los anchos de columna
                this.dtgDatos.ReadOnly = true;   //se marca los datos como solo lectura
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dtgDatos.SelectedRows.Count>0)
                {
                    //se procede a eliminar los datos del usuario
                    _conexion.EliminarUsuario(this.dtgDatos.SelectedRows[0].Cells["Email"].Value.ToString());

                    //se actualiza la lista
                    this.Buscar("");
                }
                else
                {
                    throw new Exception("Seleccione la fila del usuario que desea eliminar");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                //ojo el valor 0 indica que la función es registrar un usuario
                MostrarMantUsuarios( 0 );

                //se actualiza el catálogo de usuarios
                Buscar("");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private  void MostrarMantUsuarios(int funcion)
        {
            try
            {   //variable de tipo formulario
                FrmMantUsuarios formulario = new FrmMantUsuarios();

                
               formulario.FuncionRealizar(funcion);
                
                formulario.ShowDialog(); //se muestra el formulario
                formulario.Dispose();  //se liberan los recursos
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FrmBuscarUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ojo el valor 0 indica que la función es registrar un usuario
                MostrarMantUsuarios(1);

                //se actualiza el catálogo de usuarios
                Buscar("");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarInforme();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Método encargado de mostrar el informe
        private void MostrarInforme()
        {
            try
            {
                //se declara e instancia una variable de tipo formulario
                FrmRepUsuarios formulario = new FrmRepUsuarios();

                //se muestra el formulario
                formulario.ShowDialog();

                //se liberan los recursos
                formulario.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    } //cierre formulario 
}//cierre namespaces
