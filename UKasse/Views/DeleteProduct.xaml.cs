using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Configuration;
using System.Data;

namespace UKasse.Views
{
    /// <summary>
    /// Interaction logic for DeleteProduct.xaml
    /// </summary>
    public partial class DeleteProduct : Window
    {
        SQLiteConnection conn;
        public DeleteProduct()
        {
            InitializeComponent();
            conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            conn.Open();
            FillProductsDg();
        }

        private void SearchProduct(object sender, TextChangedEventArgs e)
        {
            try
            {
                string text = SearchProductTxb.Text.Trim();
                if(!string.IsNullOrEmpty(text))
                {
                    for(int i = 0; i < ProductsDg.Items.Count; i++)
                    {
                        DataRowView? row = ProductsDg.Items[i] as DataRowView;
                        string? pName = row?["Name"].ToString()?.ToLower();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        if (pName.StartsWith(text))
                        {
                            object item = ProductsDg.Items[i];
                            ProductsDg.SelectedItem = item;
                            ProductsDg.ScrollIntoView(item);
                            SearchProductTxb.Background = new SolidColorBrush(Colors.White);
                            break;
                        }
                        else
                        {
                            SearchProductTxb.Background = new SolidColorBrush(Colors.Red);
                        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                }
                else
                {
                    SearchProductTxb.Background = new SolidColorBrush(Colors.White);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteProductClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie folgender Produkt löschen?!", "Akyeptieren!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using(SQLiteConnection connection = new SQLiteConnection(conn))
                    {
                        DataRowView? row = ProductsDg.SelectedItems[0] as DataRowView;
                        string? id = row["Id"].ToString();
                        SQLiteCommand command = new SQLiteCommand(connection);
                        command.CommandText = "Delete from Product where id = @param1;";
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SQLiteParameter("@param1", id));
                        command.ExecuteNonQuery();
                        FillProductsDg();
                    }
                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void FillProductsDg()
        {
            try
            {
                using (SQLiteConnection _con = new SQLiteConnection(conn))
                {
                    SQLiteCommand cmd = new SQLiteCommand(_con);

                    cmd.CommandText = "SELECT id as Id, p.Name AS Name, p.Price as Price, p.isOffen as isOffen,p.Barcode as Barcode, (select pt.Name from PType as pt where pt.id = p.PTypeId) as PTName FROM PRODUCT AS p;";
                    cmd.CommandType = System.Data.CommandType.Text;
                    
                    DataSet dataSet = new DataSet();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dataSet, "ProductsDgBinding");
                    ProductsDg.DataContext = dataSet;
                    ProductsDg.Items.Refresh();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
