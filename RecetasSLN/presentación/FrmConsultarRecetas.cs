﻿using System;
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
    public partial class FrmConsultarRecetas : Form
    {
        public FrmConsultarRecetas()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmAltaReceta frmAltaReceta = new FrmAltaReceta();
            frmAltaReceta.ShowDialog();
            frmAltaReceta.Dispose();
        }
    }
}
