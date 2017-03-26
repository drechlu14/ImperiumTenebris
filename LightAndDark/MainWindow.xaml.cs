using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LightAndDark
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //Background setting
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri(@"pack://application:,,,/pics/menu_background.jpg", UriKind.Absolute));
            this.Background = myBrush;

            string rootLocation = System.AppDomain.CurrentDomain.BaseDirectory;
            SoundPlayer player = new SoundPlayer(System.IO.Path.GetFullPath(rootLocation + "/music/game_menu.wav"));
            player.Load();
            player.Play();
        }       

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterSelection selectionWindow = new CharacterSelection();
            selectionWindow.Show();
            this.Close();          
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.Show();
            this.Close();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
