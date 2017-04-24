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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvidenceFramePage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            user = ToDoItemsListView.SelectedItems[0];
            T1.Text = user.T1;
            LastNameTB.Text = user.LastName;
            RCTB.Text = user.RC;
            GenderTB.Text = user.Gender;
            Detail customization = new Detail(user);
            customization.Show();
            this.Close();
        }
    }
}
