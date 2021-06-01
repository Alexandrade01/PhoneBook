using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Exercicio4WF
{
    public partial class Form1 : Form
    {
        Dados novonumero;
        List<Dados> Lista;
        int Quantidade = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Nome Invalido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                txtNome.Focus();
                return;

            }

            if (!masekdtel.MaskCompleted)
            {
                MessageBox.Show("Numero Invalido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                masekdtel.Focus();
                return;
            }

            novonumero = new Dados(txtNome.Text, masekdtel.Text);
            string dados = novonumero.Nome + "|" + novonumero.Telefone+Environment.NewLine;
            File.AppendAllText("dados.txt", dados);
            MessageBox.Show("Done !");
            txtNome.Clear();
            masekdtel.Clear();

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists("dados.txt"))
            {
                string[] dados = File.ReadAllLines("dados.txt");
                Lista = new List<Dados>();
                foreach (var dado in dados)
                {
                    string[] novodado = dado.Split('|');
                    Dados dadoslistado = new Dados(novodado[0], novodado[1]);
                    Lista.Add(dadoslistado);
                }

                MessageBox.Show("Load Sucess!");

            }
            else
                MessageBox.Show("Not Found !", "Error", MessageBoxButtons.OK);
           
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Quantidade == Lista.Count -1 )
            {
                return;
            }
            Quantidade++;
            txtNome.Text = Lista[Quantidade].Nome;
            masekdtel.Text = Lista[Quantidade].Telefone;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Quantidade == 0 || Quantidade == -1)
            {
                return;
            }
            Quantidade--;
            txtNome.Text = Lista[Quantidade].Nome;
            masekdtel.Text = Lista[Quantidade].Telefone;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (File.Exists("dados.txt"))
            {
                if(MessageBox.Show("Do you want to delete?", "Caution!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Lista.RemoveAt(Quantidade);
                    string dadoatulizado = string.Empty;

                    foreach (var dado in Lista)
                    {
                        dadoatulizado += dado.Nome + "|" + dado.Telefone + Environment.NewLine;

                    }
                    File.WriteAllText("dados.txt", dadoatulizado);
                    txtNome.Clear();
                    masekdtel.Clear();
                }

              

                
            }
            else
                MessageBox.Show("Not Found !", "Error", MessageBoxButtons.OK);
        }
    }
}
