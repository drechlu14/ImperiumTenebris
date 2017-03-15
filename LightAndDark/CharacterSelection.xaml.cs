using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CharacterSelection.xaml
    /// </summary>
    public partial class CharacterSelection : Window
    {
        ObservableCollection<Statistics> itemsFromDb;
        ObservableCollection<Statistics> itemsFromDbChar;
        public int check;

        public CharacterSelection()
        {
            InitializeComponent();

            itemsFromDb = new ObservableCollection<Statistics>(Database.GetItemsNotDoneAsync().Result);
            for (int i = 0; i <= 2; i++)
            {
                if (itemsFromDb.Count < 3)
                {

                    Statistics player01 = new Statistics();
                    player01.ID = 1;
                    player01.check = 1;
                    player01.Name = "Lumen";
                    player01.Type = "The White Light";
                    player01.HP = 1800;
                    player01.AP = 170;
                    Database.SaveItemAsync(player01);

                    Statistics player02 = new Statistics();
                    player02.ID = 2;
                    player02.check = 2;
                    player02.Name = "Carol";
                    player02.Type = "Shiny Bullet";
                    player02.HP = 1500;
                    player02.AP = 200;
                    Database.SaveItemAsync(player02);

                    Statistics player03 = new Statistics();
                    player01.check = 3;
                    player03.Name = "Sheen";
                    player03.Type = "Angel of Justice";
                    player03.HP = 1700;
                    player03.AP = 160;
                    Database.SaveItemAsync(player03);

                }
            }

            var itemsFromDb1 = Database.GetItemsNotDoneAsync1().Result;
            ToolTipChar01.DataContext = itemsFromDb1;
            NameChar01.DataContext = itemsFromDb1;
            var itemsFromDb2 = Database.GetItemsNotDoneAsync2().Result;
            ToolTipChar02.DataContext = itemsFromDb2;
            NameChar02.DataContext = itemsFromDb2;
            var itemsFromDb3 = Database.GetItemsNotDoneAsync3().Result;
            ToolTipChar03.DataContext = itemsFromDb3;
            NameChar03.DataContext = itemsFromDb3;
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Char01Button_Click(object sender, RoutedEventArgs e)
        {
            /*int check = 1;
            List<Statistics> items = Database.GetItemsNotDoneAsyncCheck1(check).Result;
            if(items != null)
            {
                if(items.Count > 0)
                {
                    Map mapWindow = new Map(items[1]);
                    mapWindow.Show();
                    this.Close();
                }
            }*/
            int check = 1;
            itemsFromDbChar = new ObservableCollection<Statistics>(Database.GetItemsNotDoneAsync().Result);
            if (NameChar01.Content.ToString() == "Lumen")
            {
                Map mapWindow = new Map(check);
                mapWindow.Show();
                this.Close();
            }

        }

        private void Char03Button_Click(object sender, RoutedEventArgs e)
        {
            /*if (Name == "Sheen")
            {
                string Name01 = Convert.ToString(NameChar03.Content);
            }*/

            /*int check = 2;
            List<Statistics> items = Database.GetItemsNotDoneAsyncCheck1(check).Result;
            if (items != null)
            {
                if (items.Count > 0)
                {
                    Map mapWindow = new Map(items[1]);
                    mapWindow.Show();
                    this.Close();
                }
            }*/
            int check = 3;
            
            if (NameChar03.Content.ToString() == "Sheen")
            {
                Map mapWindow = new Map(check);
                mapWindow.Show();
                this.Close();
            }
        }

        private static StatisticsDatabase _database;
        public static StatisticsDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var fileHelper = new FileHelper();
                    _database = new StatisticsDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _database;
            }
        }
    }
}
