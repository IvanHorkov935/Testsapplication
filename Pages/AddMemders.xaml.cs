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
using Tests_application.Connect;

namespace Tests_application.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddMemders.xaml
    /// </summary>
    public partial class AddMemders : Page
    {
        public AddMemders()
        {
            InitializeComponent();

            ListUnallocated.ItemsSource = Helper.connect.Users.Where(x => x.ID_Group == 7).ToList();
        }
    }
}
