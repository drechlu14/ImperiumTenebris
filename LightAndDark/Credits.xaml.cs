using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LightAndDark
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class Credits : Window
    {
        //Basic information about the game mechanics and story
        public Credits()
        {
            InitializeComponent();

            //Background setting
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri(@"pack://application:,,,/pics/menu_background.jpg", UriKind.Absolute));
            this.Background = myBrush;
        }

        //Button for back to menu
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}

