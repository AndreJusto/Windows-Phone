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
    public partial class DiasSemana : PhoneApplicationPage
    {
        public Alunos aluno;
        public DiaSemana diasemana;
        public DiasSemana()
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
            NavigationService.Navigate(new Uri("/CadastraDiaSemana.xaml", UriKind.Relative));
        }

        private void AtualizarLista()
        {
            List<DiaSemana> lista = DiaSemanaDB.GetDiaSemana(aluno.ID);
            lstDiaSemana.ItemsSource = lista;
        }

        private void OnClickEditar(object sender, EventArgs e)
        {
            if (diasemana != null)
            {
                NavigationService.Navigate(new Uri("/CadastraDiaSemana.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Selecione um dia da semana!");
            }
        }

        private void OnClickDeletar(object sender, EventArgs e)
        {
            if (diasemana != null)
            {
                if (MessageBox.Show("Deletar " + diasemana.Nome + "?", "Atenção",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    DiaSemanaDB.Deletar(diasemana);
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("Selecione um dia da semana!");
            }
        }
        private void OnClickTreinos(object sender, EventArgs e)
        {
            if (diasemana != null)
            {
                NavigationService.Navigate(new Uri("/TreinosMain.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Selecione um dia da semana!");
            }
        }
        private void OnSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            diasemana = (sender as ListBox).SelectedItem as DiaSemana;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            CadastraDiaSemana page = e.Content as CadastraDiaSemana;
            if (page != null)
            {
                page.diaSemana = diasemana;
                page.Aluno = aluno;
            }

            TreinosMain page2 = e.Content as TreinosMain;
            if (page2 != null)
            {
                page2.diasemana = diasemana;
                page2.Aluno = aluno;
            }
        }
    }
}

