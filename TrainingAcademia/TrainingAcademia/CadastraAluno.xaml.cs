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
    public partial class CadastraAluno : PhoneApplicationPage
    {
        public Alunos Aluno { get; set; }
        public CadastraAluno()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Aluno != null)
            {
                    txtId.Text = Aluno.ID.ToString();
                    txtNome.Text = Aluno.Nome;
            }
        }
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Alunos aluno = new Alunos();
            if (txtId.Text != "")
            {
                aluno.ID = Convert.ToInt32(txtId.Text);
                aluno.Nome = txtNome.Text;
            }
            else
            {
                aluno.Nome = txtNome.Text;
            }

            if (Aluno != null)
            {
                if (aluno.Nome != "")
                {
                    AlunosDB.Atualizar(aluno);
                    MessageBox.Show(aluno.Nome + " salvo com Sucesso.");
                    NavigationService.GoBack();
                }
                else
                    MessageBox.Show("Por favor, informe um nome válido!", "Atenção", MessageBoxButton.OK);
            }
            else
            {
                if (aluno.Nome != "")
                {
                    AlunosDB.Salvar(aluno);
                    MessageBox.Show(aluno.Nome + " salvo com Sucesso.");
                    NavigationService.GoBack();
                }
                else
                    MessageBox.Show("Por favor, informe um nome válido!", "Atenção", MessageBoxButton.OK);
            }
        }
    }
}