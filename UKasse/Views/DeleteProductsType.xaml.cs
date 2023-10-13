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
using System.ComponentModel;

namespace UKasse.Views
{
    /// <summary>
    /// Interaction logic for DeleteProductsType.xaml
    /// </summary>
    public partial class DeleteProductsType : Window
    {
        SQLiteConnection connection;
        MainWindow win;
        public DeleteProductsType(MainWindow window)
        {
            InitializeComponent();
            try
            {
                connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                connection.Open();
                win = window;
                fill();
            }
            catch 
            {
                MessageBox.Show("connection error");
            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            win.Create();
            base.OnClosing(e);
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView? row = PtypeDg.SelectedItems[0] as DataRowView;
                string? id = row["Id"].ToString();
                using(SQLiteConnection  conn = new SQLiteConnection(connection))
                {
                    SQLiteCommand cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "delete from product where ptypeid = @param1;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SQLiteParameter("@param1", id));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.CommandText = "delete from ptype where id = @param1;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SQLiteParameter("@param1", id));
                    cmd.ExecuteNonQuery();

                    fill();
                    MessageBox.Show("deleted!!");
                    
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void fill()
        {
            try
            {
                using(SQLiteConnection conn = new SQLiteConnection(connection))
                {
                    SQLiteCommand cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "SELECT id as Id, name as Name, (Select count(id) from product where ptypeid = ptype.id) as Quantity from ptype;";
                    cmd.CommandType = CommandType.Text;

                    DataSet dataSet = new DataSet();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dataSet, "Ptype");
                    PtypeDg.DataContext = dataSet;
                    PtypeDg.Items.Refresh();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void edit(object sender, RoutedEventArgs e)
        {

        }
    }
}
