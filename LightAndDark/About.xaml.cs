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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LightAndDark
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        int count = 0;

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            TextBlock.Text = "But be careful, you'll find a lot of enemies on your jorney." + 
                            " They're called the Tenebris, hidden in the shadows and waiting for any light to consume it." +
                            " You can sence them with your light before they can attack you first.";
            TextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            count++;
            if (count == 2)
            {
                TextBlock.Text = "Still you must pay attention to all of them. They can be very dangeroues even in battle." + 
                                    " Remember if your light value reaches zero, your journey will end and the game is over.";
                NextPageButton.Visibility = Visibility.Hidden;
                TextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            /*if (count == 3)
            {
            
                InitializeComponent();
                pointChose fight = new pointChose();
                fight.Show();
                this.Close();
            }*/
        }
    }
}
