using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
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
using UKasse.Classes;

namespace UKasse
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public static SQLiteConnection? _connection;
        List<PType> pTypes = new List<PType>();
        public AddProduct()
        {
            InitializeComponent();
            try
            {
                _connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                _connection.Open();
                using(SQLiteConnection connection = new SQLiteConnection(_connection))
                {
                    SQLiteCommand cmd = new SQLiteCommand(connection);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from Ptype;";
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            pTypes.Add(new PType { Id = Convert.ToInt32(reader["ID"]), Name = Convert.ToString(reader["Name"])});
                        }
                    }
                }
                ProductsProductTypeCmb.ItemsSource = pTypes;
                ProductsProductTypeCmb.Items.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void Cancel(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
            this.Close();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            try
            {
                using(SQLiteConnection connection = new SQLiteConnection(_connection))
                {
                    SQLiteCommand cmd = new SQLiteCommand(_connection);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO PRODUCT(NAME, PRICE, ISOFFEN, BARCODE, PTYPEID) VALUES(@PARAM1,@PARAM2,@PARAM3,@PARAM4,@PARAM5)";
                    cmd.Parameters.Add(new SQLiteParameter("@PARAM1", ProductsNameTextBox.Text.Trim()));
                    cmd.Parameters.Add(new SQLiteParameter("@PARAM2",ProductsPriceTextBox.Text.Trim()));
                    cmd.Parameters.Add(new SQLiteParameter("@PARAM3",ProductsIsOffenCheckBox.IsChecked));
                    cmd.Parameters.Add(new SQLiteParameter("@PARAM4",ProductsBarcodeTextBox.Text.Trim()));
                    cmd.Parameters.Add(new SQLiteParameter("@PARAM5",ProductsProductTypeCmb.SelectedValue));
                    cmd.ExecuteScalar();
                    if(MessageBox.Show("Möchten Sie weiter Produkte hinzufügen?", "Akzeptieren", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        ProductsNameTextBox.Text = string.Empty;
                        ProductsPriceTextBox.Text = string.Empty;
                        ProductsIsOffenCheckBox.IsChecked = false;
                        ProductsBarcodeTextBox.Text = string.Empty;
                        ProductsProductTypeCmb.SelectedIndex = -1;
                    }
                    else
                    {
                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainWindow window = new MainWindow();
                window.ShowDialog();
            }

        }
    }
}
