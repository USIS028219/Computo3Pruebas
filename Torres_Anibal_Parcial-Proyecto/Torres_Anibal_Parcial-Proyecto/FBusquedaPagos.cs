using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Torres_Anibal_Parcial
{
    public partial class FBusquedaPagos : Form
    {
        ConexionDB objConexion = new ConexionDB();
        public int _idpago;
        public FBusquedaPagos()
        {
            InitializeComponent();
            grdBusquedaproductos.DataSource = objConexion.Obtener_datos().Tables["pagos"].DefaultView;
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            Filtrar_datos(txtbuscar.Text);
        }
        void Filtrar_datos(String valor)
        {
            BindingSource ds = new BindingSource();
            ds.DataSource = grdBusquedaproductos.DataSource;
            ds.Filter = "cargo like '%" + valor + "%' or fecha like '%" + valor + "%' or tipopago like '%" + valor + "%'";
            grdBusquedaproductos.DataSource = ds;
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            if (grdBusquedaproductos.RowCount > 0)
            {
                _idpago = int.Parse(grdBusquedaproductos.CurrentRow.Cells["idpago"].Value.ToString());
                Close();
            }
            else
            {
                MessageBox.Show("NO hay datos que seleccionar", "Busqueda de Pagos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
