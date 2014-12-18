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
    public partial class CadastraTreino : PhoneApplicationPage
    {
        public DiaSemana diaSemana { get; set; }
        public Alunos aluno { get; set; }
        public Treinos Treino { get; set; }
        public CadastraTreino()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Treino != null)
            {
                txtId.Text = Treino.Id.ToString();
                txtNome.Text = Treino.Nome;
                txtDescricao.Text = Treino.Descricao;
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Treinos treino = new Treinos();

            if (txtId.Text != "")
            {
                treino.Id = Convert.ToInt32(txtId.Text);
                treino.IdAlunos = aluno.ID;
                treino.IdSemana = diaSemana.Id;
                treino.Nome = txtNome.Text;
                treino.Descricao = txtDescricao.Text;
            }
            else
            {
                treino.IdAlunos = aluno.ID;
                treino.IdSemana = diaSemana.Id;
                treino.Nome = txtNome.Text;
                treino.Descricao = txtDescricao.Text;                
            }

            if (Treino != null)
            {
                if (treino.Nome != "" && treino.Descricao != "")
                {
                TreinosDB.Atualizar(treino);
                MessageBox.Show(treino.Nome + " salvo com sucesso!");
                NavigationService.GoBack();
            }
                else
                MessageBox.Show("Por favor, informe um dia válido!", "Atenção", MessageBoxButton.OK);
            }
            else
            {
                if (treino.Nome != "" && treino.Descricao != "")
                {
                    TreinosDB.Salvar(treino);
                    MessageBox.Show(treino.Nome + " salvo com sucesso!");
                    NavigationService.GoBack();
                }
                else
                    MessageBox.Show("Não podem haver campos vazios!", "Atenção", MessageBoxButton.OK);
            }
        }
    }
}