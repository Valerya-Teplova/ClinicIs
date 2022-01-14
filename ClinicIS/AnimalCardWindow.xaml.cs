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
    /// Логика взаимодействия для AnimalCardWindow.xaml
    /// </summary>
    public partial class AnimalCardWindow : Window
    {
        private int IdAn;
        public AnimalCardWindow(object id)
        {
            InitializeComponent();

            var IdAn = (int)id;
            var result = DBClass.GetContext().Animal.Where(p => p.IdAnimal == IdAn);
            AnimalIdTxB.Text += ": " + id.ToString();
            OwnerIdTxB.Text += ": " + result.First().Owner.SurName + " " + result.First().Owner.Name + " " + result.First().Owner.Patronymic; // тут ошибка. про последовательность...
            AnimalTypeTxB.Text += ": " + result.First().TypeAnimal.TypeName;
            AnimalNameTxB.Text += ": " + result.First().Name;
            AnimalAgeTxB.Text += ": " + result.First().Age;
            AnimalBreedTxB.Text += ": " + result.First().Breed;
            AnimalColorTxB.Text += ": " + result.First().Color;
            OwnerTelTxB.Text += ": " + result.First().Owner.Telephone;
            OwnerDesTxB.Text += ": " + result.First().Owner.Description;
        }
    }
}
