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
            var reception_list = from receptionservice in DBClass.GetContext().ServiceReception
                                 join reception in DBClass.GetContext().Reception on receptionservice.IdReception equals reception.IdReception
                                 join service in DBClass.GetContext().Service on receptionservice.IdService equals service.IdService
                                 join animal in DBClass.GetContext().Animal on reception.IdAnimal equals animal.IdAnimal
                                 select new
                                 {
                                     idAnim = receptionservice.IdReception,
                                     nameAni = animal.Name,
                                     dateP = reception.DateReception,
                                     timeP = reception.TimeReception,
                                     Serv = service.ServiceName + "\n" + service.Price + " рублей"
                                 };            
                       
            dataGridReception.ItemsSource = reception_list.ToList();
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
