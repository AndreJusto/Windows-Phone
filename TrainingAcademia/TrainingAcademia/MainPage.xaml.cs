using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TrainingAcademia.Resources;
using TrainingAcademia.Classes;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;

namespace TrainingAcademia
{
    public partial class MainPage : PhoneApplicationPage
    {
        Alunos aluno;
        private IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;
        private int acessos = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            List<Alunos> lista = AlunosDB.GetAlunos(null);
            lstAlunos.ItemsSource = lista;
        }

        private void onClickNovo(object sender, EventArgs e)
        {
            CadastraPessoa();
        }

        private void CadastraPessoa()
        {
            NavigationService.Navigate(new Uri("/CadastraAluno.xaml", UriKind.Relative));
        }

        private void OnClickDeletar(object sender, EventArgs e)
        {
            if (aluno != null)
            {
                if (MessageBox.Show("Deletar " + aluno.Nome + "?", "Atenção",
                                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    AlunosDB.Deletar(aluno);
                    AtualizarLista();
                }
            }
            else
            {
                MessageBox.Show("Selecione uma pessoa!");
            }
        }

        private void OnClickEditar(object sender, EventArgs e)
        {
            if (aluno != null)
            {
                NavigationService.Navigate(new Uri("/cadastraAluno.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Selecione uma pessoa!");
            }
        }

        private void OnClickDiaSemana(object sender, EventArgs e)
        {
            if (aluno != null)
            {
                NavigationService.Navigate(new Uri("/DiasSemana.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Selecione uma pessoa!");
            }

        }

        private void OnSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            aluno = (sender as ListBox).SelectedItem as Alunos;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            CadastraAluno page = e.Content as CadastraAluno;
            if (page != null)
                page.Aluno = aluno;

            DiasSemana page2 = e.Content as DiasSemana;
            if (page2 != null)
                page2.aluno = aluno;
        }
        private void txbPesquisar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Alunos> lista = AlunosDB.GetAlunos(txtPesquisar.Text);
            lstAlunos.ItemsSource = lista;
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (this.iso.Contains("login.Acesso")) //Apenas atualiza os valores das chaves
            {
                int acesso = 0;
                if (this.iso.TryGetValue<int>("login.Acesso", out acesso))
                {
                    this.acessos = acesso + 1;
                    this.iso["login.Acesso"] = acessos;

                    if (this.acessos == 3)
                    {
                        if (MessageBox.Show("Por favor, avalie nosso aplicativo, é rápido, prático e nos ajuda muito!\nSe possível deixe um comentário bacana!\n\nAgradecemos por tudo!", "EQUIPE TRAINING ACADEMY", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            MarketplaceReviewTask like = new MarketplaceReviewTask();
                            like.Show();
                        }
                        else
                        {
                            this.acessos = 2;
                            this.iso["login.Acesso"] = acessos;
                            iso.Save();
                        }
                    }
                }
            }
            else //Cria novas chaves
            {
                this.iso.Add("login.Acesso", 1);
            }
            this.iso.Save();

            while (NavigationService.BackStack.Any())
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnBackKeyPress(e);
        }

    }
}