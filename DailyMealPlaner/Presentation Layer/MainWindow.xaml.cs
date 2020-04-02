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
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using DailyMealPlaner.Business_Layer;
using DailyMealPlaner.Service_Layer;
using DailyMealPlaner.Data_Layer;

namespace DailyMealPlaner.Presentation_Layer
{
    public partial class MainWindow : Window
    {
        IService service = new Service();
        public MainWindow()
        {

            InitializeComponent();

            DrawCategoriesTreeView(service.GetCategories());
            DrawMealtimesTreeView();
            FillUserInfo();


        }

        #region Properties
        string ChosenCategory
        {
            get; set;
        }

        string ChosenProduct
        {
            get; set;
        }

        string ChosenMealtime
        {
            get; set;
        }

        #endregion

        #region Methods

        public double UpdateCaloriesRate()
        {
            return service.GetDailyCaloriesRate();
        }

        //public void SetImagesToCategories(ObservableCollection<Category> categories)
        //{
        //    foreach (Category c in categories)
        //    {
        //        try
        //        {
        //            c.ImageSource = new BitmapImage(
        //                new Uri("pack://application:,,,/Resources/" + c.Name + ".png", UriKind.RelativeOrAbsolute));
        //        }
        //        catch
        //        {
        //            c.ImageSource = new BitmapImage(
        //                new Uri("pack://application:,,,/Resources/lunch.png", UriKind.RelativeOrAbsolute));
        //        }
        //    }
        //}

        private void SetImagesToMealtimes(ObservableCollection<Mealtime> mealtimes)
        {
            foreach (Mealtime m in mealtimes)
            {
                try
                {
                    m.ImageSource = new BitmapImage(
                        new Uri("pack://application:,,,/Resources/" + m.Name + ".png", UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    m.ImageSource = new BitmapImage(
                        new Uri("pack://application:,,,/Resources/mealtime.png", UriKind.RelativeOrAbsolute));
                }
            }
        }

        void DrawCategoriesTreeView(ObservableCollection<Category> categories)
        {
            //SetImagesToCategories(categories);
            categoryTreeView.ItemsSource = categories;
        }

        void DrawMealtimesTreeView()
        {
            SetImagesToMealtimes(service.GetMealtimes());
            mealtimeTreeView.ItemsSource = service.GetMealtimes();
        }

        void FillUserInfo()
        {
            AgeTextBox.Text = service.GetUserAge().ToString();
            HeightTextBox.Text = service.GetUserHeight().ToString();
            WeightTextBox.Text = service.GetUserWeight().ToString();

            if (service.GetUserAge() == 0) AgeTextBox.Text = "";
            if(service.GetUserHeight() == 0) HeightTextBox.Text = "";
            if(service.GetUserWeight() == 0) WeightTextBox.Text = "";

            if(service.GetUserAge() != 0 && service.GetUserHeight() != 0 && service.GetUserWeight() != 0)
            {
                userCalories.Visibility = Visibility.Visible;
                calories.Text = ((int)service.GetDailyCaloriesRate()).ToString();
                clearUserInfo.Visibility = Visibility.Visible;

                if (service.RationProductsCount() != 0)
                {
                    progressBar.Value = service.GetRationCalories();
                    progressBar.Maximum = UpdateCaloriesRate();

                    CheckRemainingCalories();
                    ShowRationInfo();
                    ShowFirstRationProduct();
                }
            }

            if (service.GetUserActivity() == DailyActivity.Low) lowActivity.IsChecked = true;
            if (service.GetUserActivity() == DailyActivity.Normal) normalActivity.IsChecked = true;
            if (service.GetUserActivity() == DailyActivity.Average) averageActivity.IsChecked = true;
            if (service.GetUserActivity() == DailyActivity.High) highActivity.IsChecked = true;

        }

        private void CalculateProductInfo(string product)
        {
            productNameValue.Text = product;
            productWeightValue.Text = ((int)Slider.Value).ToString();
            productCaloriesValue.Text = service.GetCalories(product, (int)Slider.Value).ToString();
            productCarbsValue.Text = service.GetCarbs(product, (int)Slider.Value).ToString();
            productFatsValue.Text = service.GetFats(product, (int)Slider.Value).ToString();
            productProteinValue.Text = service.GetProtein(product, (int)Slider.Value).ToString();
        }

        private void CheckRemainingCalories()
        {
            int remainingCaloriesValue = (int)(service.GetDailyCaloriesRate() - service.GetRationCalories());
            if (remainingCaloriesValue < 0)
            {
                remainingCaloriesTextBlock.Visibility = Visibility.Hidden;
                remainingCalories.Text = "Норма привышена!\nКоличество ккал:" + ((int)service.GetRationCalories()).ToString();
            }
            else
            {
                remainingCaloriesTextBlock.Visibility = Visibility.Visible;
                remainingCalories.Text = remainingCaloriesValue.ToString();
            }
        }

        private bool IsSuitableItem(String item, List<String> suitableItems)
        {
            foreach (String suitableName in suitableItems)
            {
                if (item == suitableName) return true;
            }
            return false;
        }

        private void SelectSuitableCategories(List<String> suitableCategories, ObservableCollection<Category> categories)
        {
            foreach (Category treeViewItem in categoryTreeView.Items)
            {
                if (IsSuitableItem(treeViewItem.Name, suitableCategories))
                {
                    treeViewItem.IsSelected = true;
                    categories.Add(treeViewItem);
                }
                else
                {
                    treeViewItem.IsSelected = false;
                    categories.Remove(treeViewItem);
                }
            }
        }

        private ObservableCollection<Category> SelectSuitableProducts(List<String> suitableProducts, ObservableCollection<Category> categories)
        {
            foreach (Category treeViewItem in service.GetCategories())
            {
                int selectedProductsCount = 0;

                Category c = new Category(treeViewItem.Name);
                categories.Add(c);
                c.IsExpanded = true;
                foreach (var treeViewProduct in treeViewItem.Products)
                {
                    if (treeViewProduct != null)
                    {
                        if (IsSuitableItem(treeViewProduct.Name, suitableProducts))
                        {
                            //treeViewProduct.IsSelected = true;
                            c.Add(treeViewProduct);
                            selectedProductsCount++;
                        }
                        else
                        {
                            //treeViewProduct.IsSelected = false;
                            c.Remove(treeViewProduct);
                        }
                    }
                }

                if (selectedProductsCount == 0) categories.Remove(c);
            }
            return categories;
        }

        private void UnselectCategoryTreeViewItems()
        {
            foreach (Category c in service.GetCategories())
            {
                c.IsSelected = false;
                foreach (Product p in c.Products)
                {
                    p.IsSelected = false;
                }
            }
        }

        private void ShowRationInfo()
        {
            if(service.RationProductsCount() != 0)
            {
                saveRation.IsEnabled = true;
                clearRation.IsEnabled = true;

                rationInfo.Visibility = Visibility.Visible;
                productInfo.Visibility = Visibility.Visible;
                Slider.Visibility = Visibility.Visible;
                remainingCaloriesInfo.Visibility = Visibility.Visible;
                CheckRemainingCalories();
            }
        }

        private void ShowFirstRationProduct()
        {
            if(service.GetRationFirstProduct() != null)
            {
                CalculateProductInfo(service.GetRationFirstProduct().Name);
                productInfo.Visibility = Visibility.Visible;
                Slider.Visibility = Visibility.Hidden;

                //Slider.Value = service.GetWeight(service.GetRationFirstProduct().Name);
            }
        }

        private void HideRationInfo()
        {
            productInfo.Visibility = Visibility.Hidden;
            //progressBar.Visibility = Visibility.Hidden;
            remainingCaloriesInfo.Visibility = Visibility.Hidden;
            Slider.Visibility = Visibility.Hidden;
            saveRation.IsEnabled = false;
            clearRation.IsEnabled = false;
        }

        #endregion

        #region UserInfoHandling

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            service.SetAge(textBox.Text);
        }

        private void HeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            service.SetHeight(textBox.Text);
        }

        private void WeightTextBox__TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            service.SetWeight(textBox.Text);
        }

        private void lowActivity_Click(object sender, RoutedEventArgs e)
        {
            service.SetDailyActivity("Low");
        }

        private void normalActivity_Click(object sender, RoutedEventArgs e)
        {
            service.SetDailyActivity("Normal");
        }

        private void averageActivity_Click(object sender, RoutedEventArgs e)
        {
            service.SetDailyActivity("Average");
        }

        private void highActivity_Click(object sender, RoutedEventArgs e)
        {
            service.SetDailyActivity("High");
        }

        private void getCalories_Button_Click(object sender, RoutedEventArgs e)
        {
            if (service.GetUserAge() != 0 && service.GetUserHeight() != 0 && service.GetUserWeight() != 0)
            {
                clearUserInfo.Visibility = Visibility.Visible;
                //remainingCaloriesInfo.Visibility = Visibility.Visible;
                wrongUserData.Visibility = Visibility.Hidden;
                CheckRemainingCalories();
                userCalories.Visibility = Visibility.Visible;
                calories.Text = UpdateCaloriesRate().ToString();
                ShowFirstRationProduct();
                ShowRationInfo();

                progressBar.Maximum = UpdateCaloriesRate();
            }
            else
            {
                wrongUserData.Visibility = Visibility.Visible;
                userCalories.Visibility = Visibility.Hidden;
                remainingCaloriesInfo.Visibility = Visibility.Hidden;
            }
        }

        private void clearUserInfo_Click(object sender, RoutedEventArgs e)
        {
            remainingCaloriesInfo.Visibility = Visibility.Hidden;
            wrongUserData.Visibility = Visibility.Hidden;
            userCalories.Visibility = Visibility.Hidden;
            clearUserInfo.Visibility = Visibility.Hidden;
            AgeTextBox.Text = "0";
            WeightTextBox.Text = "0";
            HeightTextBox.Text = "0";
            normalActivity.IsChecked = true;
        }

        #endregion

        #region DragDrop

        private void product_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock chosenProduct = (TextBlock)sender;
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed && service.GetProductByName(chosenProduct.Text) != null)
            {
                DataObject data = new DataObject(typeof(string), chosenProduct.Text);
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void mealTime_Drop(object sender, DragEventArgs e)
        {
            TextBlock mealTime = (TextBlock)sender;
            if (e.Data.GetDataPresent(typeof(string)))
            {
                string product = e.Data.GetData(typeof(string)) as string;
                service.AddProductInMealtime(product, mealTime.Text);
                progressBar.Value += service.GetCalories(product, (int)Slider.Value);

                DrawMealtimesTreeView();
                CheckRemainingCalories();
                ShowRationInfo();

                service.SaveMealtimeChanges();
            }

            if (!(mealtimeTreeView.SelectedItem is Product) && service.RationProductsCount() != 0) ShowFirstRationProduct();
        }

        #endregion

        #region FormattingCategories

        private void addCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow addCategoryWindow = new AddCategoryWindow();
            addCategoryWindow.ShowDialog();
            DrawCategoriesTreeView(service.GetCategories());
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow(ChosenCategory, ChosenProduct);
            if (addProductWindow.ShowDialog() == true)
            {
                if (ChosenCategory != null) MessageBox.Show("Продукт был добавлен в категорию " + service.GetCategoryByName(ChosenCategory).Name);
                else MessageBox.Show("Продукт был добавлен в категорию " + service.GetCategoryByProduct(ChosenProduct).Name);
            }
            ChosenCategory = null;
            ChosenProduct = null;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (ChosenCategory != null) service.DeleteCategory(ChosenCategory);
            if(ChosenProduct != null) service.DeleteProductFromCategory(service.GetCategoryByProduct(ChosenProduct).Name, ChosenProduct);
            ChosenCategory = null;
            ChosenProduct = null;
        }

        private void categoryTreeView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (service.GetCategoryByName((sender as TextBlock).Text) != null) ChosenCategory = (sender as TextBlock).Text;
            else ChosenCategory = null;
            if (ChosenCategory == null) ChosenProduct = (sender as TextBlock).Text;
            else ChosenProduct = null;

            if ((ChosenCategory != null && !service.IsOriginalCategory(ChosenCategory)) ||
                (ChosenProduct != null && !service.IsOriginalProduct(ChosenProduct, service.GetCategoryByProduct(ChosenProduct).Name)))
            {
                //addProduct.IsEnabled = true;
                delete.IsEnabled = true;
            }
            else
            {
                //addProduct.IsEnabled = false;
                delete.IsEnabled = false;
            }
            
        }

        private void categoryTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UnselectCategoryTreeViewItems();

            if (categoryTreeView.SelectedItem is Category)
            {
                var selectedItem = categoryTreeView.SelectedItem as Category;
                selectedItem.IsSelected = true;
            }
            if (categoryTreeView.SelectedItem is Product)
            {
                var selectedItem = categoryTreeView.SelectedItem as Product;
                selectedItem.IsSelected = true;
            }
        }

        #endregion

        #region FormattingRation

        private void mealtimeTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView treeView = (TreeView)sender;
            if (treeView.SelectedItem is Product)
            {
                Product p = treeView.SelectedItem as Product;
                Slider.Value = service.GetWeight(p.Name);

                CalculateProductInfo(p.Name);
                ShowRationInfo();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mealtimeTreeView.SelectedItem is Product)
            {
                string product = (mealtimeTreeView.SelectedItem as Product).Name;
                CalculateProductInfo(product);
                service.SetProductWeight((int)Slider.Value, product, service.GetMealtimeByProduct(product).Name);
                progressBar.Value = service.GetRationCalories();
                CheckRemainingCalories();
            }
        }

        private void mealTime_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (service.getMealtimeByName((sender as TextBlock).Text) != null) ChosenMealtime = (sender as TextBlock).Text;
            else ChosenMealtime = null;
            if(ChosenMealtime == null) ChosenProduct = (sender as TextBlock).Text;
            else ChosenProduct = null;

            if ((ChosenMealtime != null && !service.IsOriginalMealtime(ChosenMealtime)) || ChosenProduct !=null)
            {
                deleteFromMealtime.IsEnabled = true; 
            }
            else delete.IsEnabled = false;
        }

        private void addMealtime_Click(object sender, RoutedEventArgs e)
        {
            AddMealtimeWindow addMealtimeWindow = new AddMealtimeWindow();
            addMealtimeWindow.ShowDialog();
            DrawMealtimesTreeView();
        }

        private void deleteFromMealtime_Click(object sender, RoutedEventArgs e)
        {
            if (ChosenMealtime != null) service.DeleteMealtime(ChosenMealtime);
            if (ChosenProduct != null) service.DeleteProductFromMealtime(service.GetMealtimeByProduct(ChosenProduct).Name, ChosenProduct);
            ChosenMealtime = null;
            ChosenProduct = null;

            progressBar.Value = service.GetRationCalories();
            CheckRemainingCalories();
            if (service.RationProductsCount() == 0) HideRationInfo();
            else ShowFirstRationProduct();
        }

        private void saveRation_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + @"\Рацион";
            saveFileDialog.Filter = "Pdf file (*.pdf)|*.pdf";
            saveFileDialog.FileName = "Рацион " + DateTime.Today.ToShortDateString() + ".pdf";

            if (saveFileDialog.ShowDialog() == true)
                PDF.CreatePDF(saveFileDialog.FileName);
        }

        private void clearRation_Click(object sender, RoutedEventArgs e)
        {
            service.ClearRation();
            HideRationInfo();
        }

        #endregion

        #region Search

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<String> suitableCategories = service.SearchCategories(search.Text);
            List<String> suitableProducts= service.SearchProducts(search.Text);

            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            //UnselectCategoryTreeViewItems();

            if (search.Text == "") DrawCategoriesTreeView(service.GetCategories());
            else DrawCategoriesTreeView(SelectSuitableProducts(suitableProducts, categories));

        }

        #endregion

    }
}
