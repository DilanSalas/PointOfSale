using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPuntoVentaG2
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }
       /// <summary>
       /// Evento encargado se cerrar el App
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Confirmarle al usuario si está seguro de cerrar la aplicación
            if (MessageBox.Show("Está seguro cerrar el sistema.","Confirmar",
                MessageBoxButtons.YesNo,
               MessageBoxIcon.Question ) == DialogResult.Yes)
            {
                //afirmativo, se cierra la aplicación
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// Este evento se ejecuta cuando inicial el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //se muestra  un mensaje de notificación
            notifyIcon1.ShowBalloonTip(25);

            MostrarLogin();
        }

        private void MostrarLogin()
        {
            //se declara e instancia un  formulario
            FrmLogin  formulario = new FrmLogin();

            //se muestra de forma exclusiva
            formulario.ShowDialog();

            //Se libera la memoria una vez finalizada la autenticación
            formulario.Dispose();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarBuscarUsuarios();//se utiliza el método para mostrar el formulario
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void MostrarBuscarUsuarios()
        {
            try
            {
                //se declara una variable de tipo formulario y se asigna una instancia
                FrmBuscarUsuarios frm = new FrmBuscarUsuarios();
                frm.ShowDialog();  //se muestra el formulario
                frm.Dispose(); //Se liberan los recursos
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }//Cierre formulario
} //cierre namespaces
