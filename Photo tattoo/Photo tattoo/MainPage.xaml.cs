using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Photo_tattoo.Resources;
using Photo_tattoo.Classes;
using System.IO;
using System.Windows.Media.Imaging;

namespace Photo_tattoo
{
    public partial class MainPage : PhoneApplicationPage
    {
        Photos photo;
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
            List<Photos> lista = PhotosDB.GetPhotos(null);
            this.ConveterBlog(lista);
            lstPhotos.ItemsSource = lista;
        }


        private void ConveterBlog(List<Photos> lista)
        {
            foreach (Photos aux in lista)
            {
                if (aux.img != null && aux.img is byte[])
                {
                    byte[] bytes = aux.img as byte[];
                    MemoryStream stream = new MemoryStream(bytes);
                    BitmapImage imag = new BitmapImage();
                    imag.SetSource(stream);
                    aux.imagem = imag;
                }                
            }
        }

        private void txbPesquisar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Photos> lista = PhotosDB.GetPhotos(txtPesquisar.Text);
            this.ConveterBlog(lista);
            lstPhotos.ItemsSource = lista;
        }

        private void OnSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            photo = (sender as ListBox).SelectedItem as Photos;
        }

        private void OnClickEditar(object sender, EventArgs e)
        {
            if (photo != null)
            {
                NavigationService.Navigate(new Uri("/CadastraPhotos.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Select a photo!");
            }
        }

        private void OnClickDeletar(object sender, EventArgs e)
        {
            if (photo != null)
            {
                if (MessageBox.Show("Delete " + photo.category + "?", "Heads up",
                                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    PhotosDB.Deletar(photo);
                    AtualizarLista();
                }
            }
            else
            {
                MessageBox.Show("Select a photo!");
            }
        }

        private void OnClickTirarPhoto(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CadastraPhotos.xaml", UriKind.Relative));
        }

        private void OnClickFeeds(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Feeds.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            CadastraPhotos page = e.Content as CadastraPhotos;
            if (page != null)
                page.Photo = photo;
        }
    }
}