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

namespace ClinicIS
{
    /// <summary>
    /// Логика взаимодействия для EditReceptionWindow.xaml
    /// </summary>
    public partial class EditReceptionWindow : Window
    {
        public EditReceptionWindow(Reception reception)
        {
            InitializeComponent();

            DataContext = reception;
            AnimalNameCmB.ItemsSource = DBClass.GetContext().Animal.ToList();
            ServiceNameCmB.ItemsSource = DBClass.GetContext().Service.ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            DBClass.GetContext().SaveChanges();
            ((UserWindow)this.Owner).ReceptionDB();
            MessageBox.Show("Информация изменена");
            Close();
        }
    }
}
