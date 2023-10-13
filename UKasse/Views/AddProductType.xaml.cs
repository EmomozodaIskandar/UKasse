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
using System.ComponentModel;

namespace UKasse.Views
{
    /// <summary>
    /// Interaction logic for ProductType.xaml
    /// </summary>
    public partial class ProductType : Window
    {
        SQLiteConnection connection;
        MainWindow window;
        public ProductType( MainWindow n)
        {
            InitializeComponent();
            connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            connection.Open();
            window = n;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            window.Create();
            this.Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            window.Create();
            base.OnClosing(e);
        }
        private void ok(object sender, RoutedEventArgs e)
        {
            try
            {
                using(SQLiteConnection _connection = new SQLiteConnection(connection))
                {
                    SQLiteCommand cmd = new SQLiteCommand(_connection);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO PTYPE(NAME) VALUES(@PARAM1);";
                    cmd.Parameters.Add(new SQLiteParameter("@PARAM1", PtypeNameTb.Text.Trim()));
                    cmd.ExecuteNonQuery();
                    if(MessageBox.Show("Möchten Sie weitere PRODUCT TYPES hinzufügen?", "Akyeptieren", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PtypeNameTb.Text = string.Empty;
                    }
                    else
                    {
                        window.Create();
                        this.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainWindow window = new MainWindow();
                window.ShowDialog();
            }
        }
    }
}
