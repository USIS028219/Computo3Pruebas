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
    public partial class FPagos_Empleados : Form
    {
        ConexionDB objConexion = new ConexionDB();
        int posicion = 0;
        string accion = "Nuevo";
        DataTable tbl = new DataTable();
        public FPagos_Empleados()
        {
            InitializeComponent();
        }

        private void FPagos_Empleados_Load(object sender, EventArgs e)
        {
            ActualizarDs();
            MostrarDatos();
        }
        void ActualizarDs()
        {
            tbl = objConexion.Obtener_datos().Tables["pagos"];
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["idpago"] };
        }
        void MostrarDatos()
        {
            try
            {
                cboEmpleados.DataSource = objConexion.Obtener_datos().Tables["empleados"];
                cboEmpleados.DisplayMember = "nombre";
                cboEmpleados.ValueMember = "empleados.idempleado";
                cboEmpleados.SelectedValue = tbl.Rows[posicion].ItemArray[1].ToString();

                lblidpago.Text = tbl.Rows[posicion].ItemArray[0].ToString();
                txtcargo.Text = tbl.Rows[posicion].ItemArray[2].ToString();
                txtfechapago.Text = tbl.Rows[posicion].ItemArray[3].ToString();
                txttipopago.Text = tbl.Rows[posicion].ItemArray[4].ToString();

                lblnregistros.Text = (posicion + 1) + " de " + tbl.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay Datos que mostrar", "Registros de Pagos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar_cajas();
            }
        }
        void Limpiar_cajas()
        {
            txtcargo.Text = "";
            txttipopago.Text = "";
            txtfechapago.Text = "";
        }
        void Controles(Boolean valor)
        {
            grbNavegacion.Enabled = valor;
            btneliminar.Enabled = valor;
            btnBuscar.Enabled = valor;
            grbDatosPagos.Enabled = !valor;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                accion = "Nuevo";

                Limpiar_cajas();
                Controles(false);
            }
            else
            {
                String[] valores = {
                    lblidpago.Text,
                    cboEmpleados.SelectedValue.ToString(),
                    txtcargo.Text,
                    txttipopago.Text,
                    txtfechapago.Text,

                };

                objConexion.Mantenimiento_pagos(valores, accion);
                ActualizarDs();
                posicion = tbl.Rows.Count - 1;
                MostrarDatos();

                Controles(true);

                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Modificar";
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (btnModificar.Text == "Modificar")
            {
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                accion = "Modificar";

                Controles(false);

                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
            }
            else
            {
                Controles(true);
                MostrarDatos();

                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Modificar";
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de eliminar a " + txtcargo.Text, "Registro de Pagos",
              MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                String[] valores = { lblidpago.Text };
                objConexion.Mantenimiento_usuarios(valores, "Eliminar");

                ActualizarDs();
                posicion = posicion > 0 ? posicion - 1 : 0;
                MostrarDatos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FBusquedaPagos frmBusquedapagos = new FBusquedaPagos();
            frmBusquedapagos.ShowDialog();

            if (frmBusquedapagos._idpago > 0)

                posicion = tbl.Rows.IndexOf(tbl.Rows.Find(frmBusquedapagos._idpago));
            MostrarDatos();
        }

        private void btnprimero_Click(object sender, EventArgs e)
        {
            posicion = 0;
            MostrarDatos();
        }

        private void btnanterior_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                MostrarDatos();
            }
            else
            {
                MessageBox.Show("Primer Registro...", "Registros de Pago",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < tbl.Rows.Count - 1)
            {
                posicion++;
                MostrarDatos();
            }
            else
            {
                MessageBox.Show("Ultimo Registro...", "Registros de Pago",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnultimo_Click(object sender, EventArgs e)
        {
            posicion = tbl.Rows.Count - 1;
            MostrarDatos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Fmenu cambio = new Fmenu();
            this.Hide();
            cambio.ShowDialog();
            this.Close();
        }
    }
}
