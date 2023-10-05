﻿using System;
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
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using System.Configuration;
using UKasse.Classes;
using System.Collections.ObjectModel;
using UKasse.Views;

namespace UKasse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {

        public static SQLiteConnection? _connection;
        List<PType> PTypes = new List<PType>();
        List<Product> Products = new List<Product>();
        ObservableCollection<Product> SelectedProducts = new ObservableCollection<Product>();
        decimal Price;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                _connection.Open();
                Create();
                Price = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void Create()
        {
            try
            {
                using(SQLiteConnection connection = new SQLiteConnection(_connection))
                {
                    var cmd = new SQLiteCommand(connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from PType";
                    var reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PTypes.Add(new PType { Name = reader["Name"].ToString(), Id = Convert.ToInt32(reader["Id"]) });
                        }
                    }
                    HeadersDG.ItemsSource = PTypes;
                    HeadersDG.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExitAppClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PTypeClicked(object sender, RoutedEventArgs e)
        {
            try
            {

                PType? row = HeadersDG.SelectedItems[0] as PType;
                int PTid = row.Id;
                using (SQLiteConnection connection = new SQLiteConnection(_connection))
                {
                    SQLiteCommand? cmd = new SQLiteCommand(connection);
                    cmd.CommandText = "Select * from Product where PTypeId = @param1;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SQLiteParameter("@param1", PTid));
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    List<Product> products1 = new List<Product>();
                    List<Product> products2 = new List<Product>();
                    List<Product> products3 = new List<Product>();
                    List<Product> products4 = new List<Product>();
                    List<Product> products5 = new List<Product>();
                    if (reader.HasRows)
                    {
                        int i = 0; 
                        while (reader.Read())
                        {
                            if(i == 0)
                            {
                                products1.Add(new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Barcode = Convert.ToString(reader["Barcode"]),
                                    isOffen = Convert.ToBoolean(reader["isOffen"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    Price = Convert.ToString(reader["Price"]),
                                    PTypeId = PTid
                                });
                                i++;
                            }
                            else if(i == 1)
                            {
                                products2.Add(new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Barcode = Convert.ToString(reader["Barcode"]),
                                    isOffen = Convert.ToBoolean(reader["isOffen"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    Price = Convert.ToString(reader["Price"]),
                                    PTypeId = PTid
                                });
                                i++;
                            }
                            else if (i == 2)
                            {
                                products3.Add(new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Barcode = Convert.ToString(reader["Barcode"]),
                                    isOffen = Convert.ToBoolean(reader["isOffen"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    Price = Convert.ToString(reader["Price"]),
                                    PTypeId = PTid
                                });
                                i++;
                            }
                            else if (i == 3)
                            {
                                products4.Add(new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Barcode = Convert.ToString(reader["Barcode"]),
                                    isOffen = Convert.ToBoolean(reader["isOffen"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    Price = Convert.ToString(reader["Price"]),
                                    PTypeId = PTid
                                });
                                i++;
                            }
                            else
                            {
                                products5.Add(new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Barcode = Convert.ToString(reader["Barcode"]),
                                    isOffen = Convert.ToBoolean(reader["isOffen"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    Price = Convert.ToString(reader["Price"]),
                                    PTypeId = PTid
                                });
                                i = 0;
                            }
                        }
                    }
                    ProductsDG1.ItemsSource = products1;
                    ProductsDG2.ItemsSource = products2;
                    ProductsDG3.ItemsSource = products3;
                    ProductsDG4.ItemsSource = products4;
                    ProductsDG5.ItemsSource = products5;
                    ProductsDG1.Items.Refresh();
                    ProductsDG2.Items.Refresh();
                    ProductsDG3.Items.Refresh();
                    ProductsDG4.Items.Refresh();
                    ProductsDG5.Items.Refresh();
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteFromList(object sender, RoutedEventArgs e)
        {
            try
            {
                Product? p = SelectedProductsDG.SelectedItem as Product;
                SelectedProducts.Remove(p);
                Price -=  Convert.ToDecimal(p.Price);
                PriceTextBox.Text = Convert.ToString(Price);
                SelectedProductsDG.ItemsSource = SelectedProducts;
                SelectedProductsDG.Items.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ProductTyped1(object sender, RoutedEventArgs e)
        {
            Product? p = ProductsDG1.SelectedItems[0] as Product;
            SelectedProducts.Add(p);
            Price += Convert.ToDecimal(p.Price);
            PriceTextBox.Text = Convert.ToString(Price);
            SelectedProductsDG.ItemsSource = SelectedProducts;
        }

        private void ProductTyped2(object sender, RoutedEventArgs e)
        {
            Product? p = ProductsDG2.SelectedItems[0] as Product;
            SelectedProducts.Add(p);
            Price += Convert.ToDecimal(p.Price);
            PriceTextBox.Text = Convert.ToString(Price);
            SelectedProductsDG.ItemsSource = SelectedProducts;
        }

        private void ProductTyped3(object sender, RoutedEventArgs e)
        {
            Product? p = ProductsDG3.SelectedItems[0] as Product;
            SelectedProducts.Add(p);
            Price += Convert.ToDecimal(p.Price);
            PriceTextBox.Text = Convert.ToString(Price);
            SelectedProductsDG.ItemsSource = SelectedProducts;
        }

        private void ProductTyped4(object sender, RoutedEventArgs e)
        {
            Product? p = ProductsDG4.SelectedItems[0] as Product;
            SelectedProducts.Add(p);
            Price += Convert.ToDecimal(p.Price);
            PriceTextBox.Text = Convert.ToString(Price);
            SelectedProductsDG.ItemsSource = SelectedProducts;
        }

        private void ProductTyped5(object sender, RoutedEventArgs e)
        {
            Product? p = ProductsDG5.SelectedItems[0] as Product;
            SelectedProducts.Add(p);
            Price += Convert.ToDecimal(p.Price);
            PriceTextBox.Text = Convert.ToString(Price);
            SelectedProductsDG.ItemsSource = SelectedProducts;
        }

        private void GoToPay(object sender, RoutedEventArgs e)
        {
            if(SelectedProducts.Count > 0)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error");
                }
            }
            
        }

        private void AddProductViewOpen(object sender, RoutedEventArgs e)
        {
            try
            {
                Window window = new AddProduct();
                window.ShowDialog();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "error", "error");
            }
        }

        private void AddProductTypeViewOpen(object sender, RoutedEventArgs e)
        {
            try
            {
                Window window = new ProductType();
                window.ShowDialog();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"Error", "error");
            }
        }

        private void DeleteProductsTypeEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(
@"                  Achtung!!!
        Beachten Sie, dass alle Produkten
    die diese Produkttyp haben werden gelöscht.
                                            ");
                DeleteProductsType deleteProduct = new DeleteProductsType();
                deleteProduct.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteProductEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteProduct deleteProduct = new DeleteProduct();
                deleteProduct.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
