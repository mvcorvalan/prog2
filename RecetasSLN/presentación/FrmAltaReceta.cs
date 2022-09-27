using RecetasSLN.dominio;
using RecetasSLN.Servicios;
using RecetasSLN.Servicios.Interfaces;
using RecetasSLN.datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecetasSLN.presentación
{
    public partial class FrmAltaReceta : Form
    {
        private IService oServicio;
        private Receta oReceta;
        private HelperDao oDatos;
        public FrmAltaReceta()
        {
            InitializeComponent();

            oServicio = new ServiceFactoryImp().CrearService();
            oReceta = new Receta();
            oDatos = HelperDao.ObtenerInstancia();
        }

        private void FrmAltaReceta_Load(object sender, EventArgs e)
        {
            CargarIngredientes();
            //CargarTiposRecetas();
            //ProximaReceta();
            cboIngredientes.SelectedIndex = -1;
        }

        private void ProximaReceta()
        {
            int next = oServicio.GetProximaReceta();
            if (next > 0)
                lblNroReceta.Text = next.ToString();
            else
                MessageBox.Show("No se pudo obtener número de receta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //private void CargarTiposRecetas()
        //{
            
        //}

        private void CargarIngredientes()
        {
            DataTable tabla = oServicio.GetIngredientes();
            cboIngredientes.DataSource = tabla;
            cboIngredientes.ValueMember = tabla.Columns[0].ColumnName;
            cboIngredientes.DisplayMember = tabla.Columns[1].ColumnName;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Ingresar nombre de receta", "Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }
            if (txtCheff.Text == string.Empty)
            {
                MessageBox.Show("Ingresar un cheff", "Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCheff.Focus();
                return;
            }
            if (cboTipoReceta.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccionar un tipo de receta", "Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTipoReceta.Focus();
                return;
            }
            if (dgvIngredientes.Rows.Count<3)
            {
                MessageBox.Show("Ha olvidado ingredientes?", "Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvIngredientes.Focus();
                return;
            }
            GuardarReceta();
        }

        private void GuardarReceta()
        {
            Receta receta = new Receta();
            receta.Nombre= txtNombre.Text;
            receta.Cheff= txtCheff.Text;
            receta.TipoReceta = cboTipoReceta.SelectedIndex;
            if(oDatos.ConfirmarReceta(receta))
            {
                MessageBox.Show("Receta guardada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No se pudo guardar la receta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cboIngredientes.SelectedIndex==-1)
            {
                MessageBox.Show("Seleccione un ingrediente", "Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvIngredientes.Focus();
                return;
            }
            if(txtCantidad.Text==string.Empty)
            {
                MessageBox.Show("Ingrese una cantidad", "Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return;
            }
            //foreach (DataGridViewRow row in dgvIngredientes.Rows)
            //{
            //    if (row.Cells["colIngrediente"].Value.ToString().Equals(cboIngredientes.Text));
            //    {
            //        MessageBox.Show("Ese ingrediente ya está en la lista", "Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        cboIngredientes.Focus();
            //        return;
            //    }
            //}

            DataRowView item = (DataRowView)cboIngredientes.SelectedItem;

            Ingrediente ingrediente = new Ingrediente();
            ingrediente.Nombre = item.Row.ItemArray[1].ToString();
            ingrediente.IdIngrediente = Convert.ToInt32(item.Row.ItemArray[0]);

            DetalleReceta detalle = new DetalleReceta();
            detalle.Ingrediente = ingrediente;
            detalle.Cantidad = Convert.ToInt32(txtCantidad.Text);
            oReceta.AgregarDetalle(detalle);

            dgvIngredientes.Rows.Add(new object[] { item.Row.ItemArray[0], item.Row.ItemArray[1], txtCantidad.Text });

            CalcularTotal();
            
        }
        private void CalcularTotal()
        {
            double total = oReceta.CalcularTotal();
            lblTotalIngredientes.Text=total.ToString();
        }

        private void dgvIngredientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvIngredientes.CurrentCell.ColumnIndex==3)
            {
                oReceta.QuitarDetalle(dgvIngredientes.CurrentRow.Index);
                dgvIngredientes.Rows.Remove(dgvIngredientes.CurrentRow);
                CalcularTotal();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
