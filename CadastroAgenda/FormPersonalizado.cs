﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices; // Importação necessária

namespace CadastroAgenda
{
    public partial class FormPersonalizado : Form
    {
        // Importação de funções do Windows para mover a janela
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();
        [DllImport("user32.dll")]


        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        //vamos chamar o BD
        string conexao = "Data Source = JUN0684686W11-1\\BDSENAC; Initial Catalog = BDTI46; User ID = senaclivre; Password=senaclivre";

        public FormPersonalizado()
        {
            InitializeComponent();
            CarregarDados();

            // Adiciona evento para mover a janela ao clicar na barra personalizada
            pnlTitulo.MouseDown += new MouseEventHandler(pnlTitulo_MouseDown);


        }


        private void pnlTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string query = "INSERT INTO Cliente (nome, email, telefone) VALUES (@Nome, @email, @telefone)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Cadastro realizado com sucesso!");
                limpadados();
                CarregarDados();

            }
        }

        private void limpadados()
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string query = "UPDATE Cliente SET Nome=@Nome, Email=@Email, Telefone=@telefone WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Registro atualizado!");
                limpadados();
                CarregarDados();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string query = "DELETE FROM Cliente WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Registro excluído!");
                limpadados();
                CarregarDados();
            }
        }

        private void CarregarDados()
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string query = "SELECT * FROM Cliente";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgCadatroPersonalizado.DataSource = dt;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarDados();
        }
        

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string query = "SELECT * FROM Cliente WHERE Nome LIKE @Nome";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", "%" + txtConsulta.Text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgCadatroPersonalizado.DataSource = dt;
            }
        }

        private void dgCadatroPersonalizado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica se uma linha válida foi clicada
            {
                DataGridViewRow row = dgCadatroPersonalizado.Rows[e.RowIndex];

                txtID.Text = row.Cells["Id"].Value.ToString();
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtTelefone.Text = row.Cells["Telefone"].Value.ToString();
            }
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
           // Application.Exit(); // Fecha o programa
            this.Close();
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // Minimiza a janela
        }
    }
    
}
