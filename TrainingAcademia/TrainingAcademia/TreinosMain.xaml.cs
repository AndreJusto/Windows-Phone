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
    public partial class TreinosMain : PhoneApplicationPage
    {
        public Alunos Aluno;
        public DiaSemana diasemana;
        public Treinos treino;
        public TreinosMain()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            AtualizarLista();
        }

        private void onClickNovo(object sender, EventArgs e)
        {
            CadastraDia();
        }

        private void CadastraDia()
        {
            NavigationService.Navigate(new Uri("/CadastraTreino.xaml", UriKind.Relative));
        }

        private void AtualizarLista()
        {
            List<Treinos> lista = TreinosDB.GetTreinos(diasemana.Id, Aluno.ID);
            lstTreinos.ItemsSource = lista;
        }

        private void OnClickEditar(object sender, EventArgs e)
        {
            if (treino != null)
            {
                NavigationService.Navigate(new Uri("/CadastraTreino.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Selecione um treino!");
            }
        }

        private void OnClickDeletar(object sender, EventArgs e)
        {
            if (treino != null)
            {
                if (MessageBox.Show("Deletar " + treino.Nome + "?", "Atenção",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    TreinosDB.Deletar(treino);
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("Selecione um treino!");
            }
        }
        private void OnSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            treino = (sender as ListBox).SelectedItem as Treinos;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            CadastraTreino page = e.Content as CadastraTreino;
            if (page != null)
            {
                page.aluno = Aluno;
                page.diaSemana = diasemana;
                page.Treino = treino;
            }
        }
    }
}