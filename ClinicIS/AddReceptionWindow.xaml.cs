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
    /// Логика взаимодействия для AddReceptionWindow.xaml
    /// </summary>
    public partial class AddReceptionWindow : Window
    {
        public AddReceptionWindow()
        {
            InitializeComponent();

            
            AnimalNameCmB.ItemsSource = DBClass.GetContext().Animal.ToList();
            ServiceNameCmB.ItemsSource = DBClass.GetContext().Service.ToList();
            UserNameCmB.ItemsSource = DBClass.GetContext().User.ToList();

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (AnimalNameCmB.SelectedItem == null)
                error.AppendLine("Вы не выбрали кличку животного");
            if (DateRecepDP.SelectedDate == null)
                error.AppendLine("Вы не указали дату приема");
            if (TimeRecepTxB.Text == null)
                error.AppendLine("Вы не указали время приема");
            if (AnamnesTxB.Text == null)
                error.AppendLine("Вы не указали анамнез пациента");
            if (ServiceNameCmB.SelectedItem == null)
                error.AppendLine("Вы не выбрали услугу");
            if (UserNameCmB.SelectedItem == null)
                error.AppendLine("Вы не выбрали сотрудника");

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }

            DBClass.GetContext().Reception.Add(new Reception()
            {
                DateReception = DateRecepDP.SelectedDate.Value,
                TimeReception = TimeSpan.Parse(TimeRecepTxB.Text),
                Animal = AnimalNameCmB.SelectedItem as Animal,
                Anamnesis = AnamnesTxB.Text,
                Service = ServiceNameCmB.SelectedItem as Service,
                User = UserNameCmB.SelectedItem as User
            });
            DBClass.GetContext().SaveChanges();
            ((UserWindow)this.Owner).ReceptionDB();
            MessageBox.Show("Данные успешно добавлены");
            Close();

        }
    }
}
