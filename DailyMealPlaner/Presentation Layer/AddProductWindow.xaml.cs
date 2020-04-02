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
    public partial class AddProductWindow : Window
    {
        IService service = new Service();

        string ChosenCategory
        {
            get; set;
        }

        string ChosenProduct
        {
            get; set;
        }

        public AddProductWindow(string chosenCategory, string chosenProduct)
        {
            InitializeComponent();
            ChosenProduct = chosenProduct;
            ChosenCategory = chosenCategory;
            if (ChosenCategory == null) ChosenCategory = service.GetCategoryByProduct(ChosenProduct).Name;
        }

        private bool IsAllTextBoxesFilled()
        {
            if(name.Text != "" && calories.Text != "" && protein.Text != "" && fats.Text != "" && carbs.Text != "")
            {
                return true;
            }
            return false;
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsAllTextBoxesFilled()) ok.IsEnabled = true; 
        }

        private void calories_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsAllTextBoxesFilled()) ok.IsEnabled = true;
        }

        private void protein_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsAllTextBoxesFilled()) ok.IsEnabled = true;
        }

        private void fats_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsAllTextBoxesFilled()) ok.IsEnabled = true;
        }

        private void carbs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsAllTextBoxesFilled()) ok.IsEnabled = true;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;
            if(!Double.TryParse(calories.Text, out temp) || !Double.TryParse(protein.Text, out temp) || !Double.TryParse(fats.Text, out temp) || !Double.TryParse(carbs.Text, out temp))
            {
                wrongInput.Visibility = Visibility.Visible;
            }
            else if (!service.CheckProductExistence(name.Text, ChosenCategory))
            {
                wrongInput.Visibility = Visibility.Hidden;
                service.AddNewProduct(service.CreateNewProduct(name.Text, calories.Text, protein.Text, fats.Text, carbs.Text), ChosenCategory); ;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Продукт с данным названием уже существует!");
                name.Text = "";
            }
        }
    }
}
