using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroAgenda
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void agendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();  
            form1.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPersonalizado formPersonalizado = new FormPersonalizado();  
            formPersonalizado.ShowDialog(); 

        }
    }
}
