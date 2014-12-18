using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TrainingAcademia.Classes;

namespace TrainingAcademia
{
    public partial class CadastraDiaSemana : PhoneApplicationPage
    {
        public Alunos Aluno { get; set; }
        public DiaSemana diaSemana { get; set; }
        public CadastraDiaSemana()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (diaSemana != null)
            {
                txtId.Text = diaSemana.Id.ToString();
                txtNome.Text = diaSemana.Nome;
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            DiaSemana diasemana = new DiaSemana();

            if (txtId.Text != "")
            {
                diasemana.Id = Convert.ToInt32(txtId.Text);
                diasemana.IdAluno = Aluno.ID;
                diasemana.Nome = txtNome.Text;
            }
            else
            {
                diasemana.IdAluno = Aluno.ID;
                diasemana.Nome = txtNome.Text;
            }

            if (diaSemana != null)
            {
                if (diasemana.Nome != "")
                {
                    DiaSemanaDB.Atualizar(diasemana);
                    MessageBox.Show(diasemana.Nome + " salvo com sucesso!");
                    NavigationService.GoBack();
                }
                else
                    MessageBox.Show("Por favor, informe um dia válido!", "Atenção", MessageBoxButton.OK);
            }
            else
            {
                if (diasemana.Nome != "")
                {
                    DiaSemanaDB.Salvar(diasemana);
                    MessageBox.Show(diasemana.Nome + " salvo com sucesso!");
                    NavigationService.GoBack();
                }
                else
                    MessageBox.Show("Por favor, informe um dia válido!", "Atenção", MessageBoxButton.OK);
            }
 
        }
    }
}