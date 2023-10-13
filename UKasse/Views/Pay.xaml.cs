using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace UKasse.Views
{
    /// <summary>
    /// Interaction logic for Pay.xaml
    /// </summary>
    public partial class Pay : Window
    {
        MainWindow window;
        public Pay(MainWindow win, List<string> SelectedProducts)
        {
            InitializeComponent();
            window = win;
            string text = "";
            int i = 0; 
            foreach (string p in SelectedProducts)
            {
                text += $"{++i}" + p + '\n';
            }
            productsResiveReview.AppendText(text);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            window.Visibility = Visibility.Visible;
            window.Activate();
            base.OnClosing(e);
        }
    }
}
