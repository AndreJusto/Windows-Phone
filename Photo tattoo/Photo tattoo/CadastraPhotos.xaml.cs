using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using Photo_tattoo.Classes;
using Microsoft.Phone.Tasks;
using System.IO;

namespace Photo_tattoo
{
    public partial class CadastraPhotos : PhoneApplicationPage
    {
        private CameraCaptureTask camera { get; set; }
        private ShareMediaTask compartilhar { get; set; }
        public Photos Photo { get; set; }
        private string caminho { get; set; }

        public byte[] imageBytes {get; set;}

        public CadastraPhotos()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Photo != null)
            {
                txtId.Text = Photo.id.ToString();
                txbCategory.Text = Photo.category;
                Photo.img = imageBytes;
            }
        }
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Photos photo = new Photos();
            if (txtId.Text != "")
            {
                photo.id = Convert.ToInt32(txtId.Text);
                photo.category = txbCategory.Text;
                photo.img = imageBytes;
            }
            else
            {
                photo.category = txbCategory.Text;
                photo.img = imageBytes;
            }

            if (Photo != null)
            {
                if (photo.category != "")
                {
                    PhotosDB.Atualizar(photo);
                    MessageBox.Show(photo.category + "saved Successfully.");
                    NavigationService.GoBack();
                }
                else
                    MessageBox.Show("Please, enter a valid category!", "Heads up", MessageBoxButton.OK);
            }
            else
            {
                if (photo.category != "")
                {
                    PhotosDB.Salvar(photo);
                    MessageBox.Show(photo.category + "saved successfully.");
                    NavigationService.GoBack();
                }
                else
                    MessageBox.Show("Please, enter a valid category!", "Heads up", MessageBoxButton.OK);
            }
        }

        private void OnClickCapturar(object sender, RoutedEventArgs e)
        {
            camera = new CameraCaptureTask();
            camera.Completed += camera_Completed;
            camera.Show();
        }

        void camera_Completed(object sender, PhotoResult e)
        {
            BitmapImage imgTemp = new BitmapImage();
            caminho = e.OriginalFileName;
            imgTemp.SetSource(e.ChosenPhoto);
            imgcapture.Source = imgTemp;
            imageBytes = ImageToBytes(imgTemp);
        }

        public static byte[] ImageToBytes(BitmapImage img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap(img);
                System.Windows.Media.Imaging.Extensions.SaveJpeg(btmMap, ms, img.PixelWidth, img.PixelHeight, 0, 100);
                img = null;
                return ms.ToArray();
            }
        }

        //public static byte[] ConvertToBytes(BitmapImage bitmapimages)
       // {
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    var wBitmap = new WriteableBitmap(bitmapImage);
            //    wBitmap.SaveJpeg(stream, wBitmap.PixelWidth, wBitmap.PixelHeight, 0, 100);
            //    stream.Seek(0, SeekOrigin.Begin);
            //    return stream.ToArray();
            //}
       // }
       
        private void OnClickCompartilhar(object sender, RoutedEventArgs e)
        {
            compartilhar = new ShareMediaTask();
            compartilhar.FilePath = caminho;
            compartilhar.Show();
        }
    }
}
