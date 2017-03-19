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
    public partial class About : Window
    {
        //Basic information about the game mechanics and story
        public About()
        {
            InitializeComponent();
        }

        //Button for back to menu
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        int count = 0;

        //Button for turning the page
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)FindResource("animate")).Begin(TextBlock);
            TextBlock.Text = "But be careful, you'll find a lot of enemies on your journey." + 
                            " They're called the Tenebris, hidden in the shadows and waiting for any light to consume it." +
                            " You can sence them with your light before they can attack you first.";
            TextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            count++;
            if (count == 2)
            {
                ((Storyboard)FindResource("animate")).Begin(TextBlock);
                TextBlock.Text = "Still you must pay attention to all of them. They can be very dangerous even in battle." +
                    " Remember if your light value reaches zero, your journey will end and the game is over.";
                TextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 3)
            {
                ((Storyboard)FindResource("animate")).Begin(TextBlock);
                TextBlock.Text =
                    "Attention!: This game is not meant to entertain player with it's combat system. It's meant" +
                    " to make player think about it and dive into the story. From the beginning till the end.";
                NextPageButton.Visibility = Visibility.Hidden;
                TextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
        }
    }
}
