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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User user;
        public UserWindow(User user)
        {
            InitializeComponent();

            this.user = user;
            UserNameLbl.Content = user.SurName + " " + user.Name + " " + user.Patronymic;
            UserRoleLbl.Content = user.Role.RoleName;

            dataGridReception.Columns[1].ClipboardContentBinding.StringFormat = "d";
            ReceptionDB();
        }

        public void ReceptionDB()
        {
            DBClass.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            var pr_list = from reception in DBClass.GetContext().Reception
                          join animal in DBClass.GetContext().Animal on reception.IdAnimal equals animal.IdAnimal
                          join service in DBClass.GetContext().Service on reception.IdService equals service.IdService
                          select new
                          {
                              idAnim = reception.IdReception,                              
                              nameAni = animal.Name,
                              dateP = reception.DateReception,
                              timeP = reception.TimeReception,
                              Serv = service.ServiceName + "\n" + service.Price + " рублей"
                          };
                       
            dataGridReception.ItemsSource = pr_list.ToList();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;
            AnimalCardWindow animalCardWindow = new AnimalCardWindow(label.Tag);
            animalCardWindow.Show();
        }

        private void edBut_Click(object sender, RoutedEventArgs e)
        {
            Button editbutton = sender as Button;
            //EditReceptionWindow editReceptionWindow = new EditReceptionWindow((Reception)editbutton.Tag);            
            var editReceptionWindow = new EditReceptionWindow(DBClass.GetContext().Reception.Where(p => p.IdReception == (int)editbutton.Tag).First());
            editReceptionWindow.Owner = this;
            editReceptionWindow.Show();

        }        

        private void AddReception_Click(object sender, RoutedEventArgs e)
        {
            Button addbutton = sender as Button;
            AddReceptionWindow addReceptionWindow = new AddReceptionWindow { Owner = this };
            addReceptionWindow.Show();
        }
    }
}
