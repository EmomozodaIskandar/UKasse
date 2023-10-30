using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace UKasse.Views
{
    /// <summary>
    /// Interaction logic for Pay.xaml
    /// </summary>
    public partial class Pay : Window
    {
        MainWindow window;
        string expressionCal = string.Empty;
        public Pay(MainWindow win, List<string> SelectedProducts, decimal Price)
        {
            InitializeComponent();
            window = win;
            convenientbutton.Content = ((int)Price+1).ToString();
            string text = createResive(SelectedProducts);
            ClientMustpaySumTxb.Text = Price.ToString();
            
            productsResiveReview.AppendText(text);
        }
        
        private string createResive(List<string> products)
        {
            try
            {
                HashSet<string> Uproducts = new HashSet<string>(products);
                string text = "";
                int i = 0;
                foreach (string Up in Uproducts)
                {
                    foreach(string p in products)
                    {
                        if (p == Up)
                            i++;
                    }
                    text += $"{i}" + "x" + $"{Up}" + '\n';
                    i = 0; 
                }
                return text;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }

        private void CashClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal typedSum = Convert.ToDecimal(keyboardTabTxb.Text);
                decimal proSum = Convert.ToDecimal(ClientMustpaySumTxb.Text);
                decimal res =  Math.Abs((typedSum-proSum));
                if (typedSum >= proSum)
                {
                    OpenCheckout();                
                    MessageBox.Show($"{res}");
                    window.Close();
                    MainWindow wn = new MainWindow();
                    wn.ShowDialog();
                    this.Close();
                    
                }
                else
                {
                    keyboardTabTxb.Text = "0,00";
                    ClientMustpaySumTxb.Text = (proSum - typedSum).ToString();
                    convenientbutton.Content = ((int)res + 1 ).ToString();
                    expressionCal = string.Empty;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show (ex.Message);
            }
        }
        private void OpenCheckout()
        {
            MessageBox.Show("Kasse ist geoffnet.");
        }

        private void CardClicked(object sender, RoutedEventArgs e)
        {
            ActivateECPay(ClientMustpaySumTxb.Text);
        }
        private void ActivateECPay(string cost)
        {
            try
            {
                MessageBox.Show("payment will be done with card!!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExactClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientMustpaySumTxb.Text = "0,00";
                OpenCheckout();
                window.Close();
                MainWindow wn = new MainWindow();
                wn.ShowDialog();
                this.Close();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            
        }

        private void ConvenientClicked(object sender, RoutedEventArgs e)
        {
            ClientMustpaySumTxb.Text = (Convert.ToDecimal(convenientbutton.Content.ToString()) - Convert.ToDecimal(ClientMustpaySumTxb.Text)).ToString("F2");
            MessageBox.Show(ClientMustpaySumTxb.Text);
            OpenCheckout();
            window.Close ();
            MainWindow wn = new MainWindow();
            wn.ShowDialog();
            this.Close();
        }

        private void Clicked5(object sender, RoutedEventArgs e)
        {
            decimal clientDept = Convert.ToDecimal(ClientMustpaySumTxb.Text.Trim());
           
            if (clientDept-5 > 0)
            {
                ClientMustpaySumTxb.Text = (clientDept - 5).ToString("F2");
                convenientbutton.Content = ((int)(clientDept - 5) + 1).ToString();
            }
            else
            {
                OpenCheckout();
                MessageBox.Show($"{5 - clientDept}");
                window.Close();
                MainWindow wn = new MainWindow();
                wn.ShowDialog();
                this.Close();
            }
        }

        private void Clicked20(object sender, RoutedEventArgs e)
        {
            decimal clientDept = Convert.ToDecimal(ClientMustpaySumTxb.Text.Trim());
            if (clientDept - 20 > 0)
            {
                ClientMustpaySumTxb.Text = (clientDept - 20).ToString("F2");
                convenientbutton.Content = ((int)(clientDept - 20) + 1).ToString();
            }
            else
            {
                OpenCheckout();
                MessageBox.Show($"{20 - clientDept}");
                window.Close();
                MainWindow wn = new MainWindow();
                wn.Show();
                this.Close();
            }
        }

        private void Clicked50(object sender, RoutedEventArgs e)
        {
            decimal clientDept = Convert.ToDecimal(ClientMustpaySumTxb.Text.Trim());
            if (clientDept - 50 > 0)
            {
                ClientMustpaySumTxb.Text = (clientDept - 50).ToString("F2");
                convenientbutton.Content = ((int)(clientDept - 50) + 1).ToString();
            }
            else
            {
                OpenCheckout();
                MessageBox.Show($"{50 - clientDept}");
                window.Close();
                MainWindow wn = new MainWindow();
                wn.Show();
                this.Close();
            }
        }

        private void Clicked10(object sender, RoutedEventArgs e)
        {
            decimal clientDept = Convert.ToDecimal(ClientMustpaySumTxb.Text.Trim());
            if (clientDept - 10 > 0)
            {
                ClientMustpaySumTxb.Text = (clientDept - 10).ToString("F2");
                convenientbutton.Content = ((int)(clientDept - 10) + 1).ToString();
            }
            else
            {
                OpenCheckout();
                MessageBox.Show($"{10 - clientDept}");
                window.Close();
                MainWindow wn = new MainWindow();
                wn.Show();
                this.Close();
            }
        }

        private void eraseClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(expressionCal))
            {
                expressionCal = expressionCal.Remove(expressionCal.Length - 1);
                keyboardTabTxb.Text = expressionCal;
                if(string.IsNullOrEmpty(expressionCal))
                    keyboardTabTxb.Text = "0,00";
            }
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            window.Visibility = Visibility.Visible;
            window.Activate();
            this.Close();
        }

        private void btn0Click(object sender, RoutedEventArgs e)
        {
            if(expressionCal != "0")
            {
                expressionCal += '0';
                expressionCal = decimalMaker(expressionCal);
                keyboardTabTxb.Text = expressionCal;
            }
        }

        private void btn2Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '2';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;
        }

        private void btn1Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '1';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;
        }

        private void btn3Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '3';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;
        }

        private void btn4Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '4';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;
        }

        private void btn5Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '5';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;
        }

        private void btn6Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '6';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;
        }

        private void btn7Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '7';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;
        }

        private void btn8Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '8';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;
        }
        
        private void btn9Click(object sender, RoutedEventArgs e)
        {
            expressionCal += '9';
            expressionCal = decimalMaker(expressionCal);
            keyboardTabTxb.Text = expressionCal;

        }
        private string decimalMaker(string exp)
        {
            int index;
            if(HasCommon(exp))
            {
                index = exp.IndexOf(',');
                return ((exp.Length - index) <= 2) ? exp : exp[0..(index + 3)];
            }
            return exp;
            
        }

        private void btnCommonClick(object sender, RoutedEventArgs e)
        {
            if(!HasCommon(expressionCal)&& expressionCal!=string.Empty)
            {
                expressionCal += ',';
                keyboardTabTxb.Text = expressionCal;
            }
            else if(expressionCal==string.Empty)
            {
                expressionCal = "0,";
                keyboardTabTxb.Text= expressionCal;
            }
        }
        private bool HasCommon(string expression)
        {
            for(int i=0; i<expression.Length; i++)
            {
                if (expression[i] == ',')
                { return true; }
            }
            return false;
        }
    }
}
