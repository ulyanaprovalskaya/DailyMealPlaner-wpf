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
using DailyMealPlaner.Service_Layer;

namespace DailyMealPlaner.Presentation_Layer
{
    public partial class AddMealtimeWindow : Window
    {
        IService service = new Service();

        public AddMealtimeWindow()
        {
            InitializeComponent();
        }

        private void newMealtimeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (newMealtimeName.Text != "") ok.IsEnabled = true;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (!service.CheckMealtimeExistence(newMealtimeName.Text))
            {
                service.AddNewMealtime(newMealtimeName.Text);
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Категория с данным названием уже существует!");
                newMealtimeName.Text = "";
            }
        }
    }
}
