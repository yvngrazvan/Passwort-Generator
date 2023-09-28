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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace Passwort_Generator_Grafisch
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string pass;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_password_random_Click(object sender, RoutedEventArgs e)
        {
            btn_close.Visibility = Visibility.Hidden;
            btn_passwort_own.Visibility = Visibility.Hidden;
            btn_webseite.Visibility = Visibility.Hidden;
            label_passwort.Visibility = Visibility.Visible;

            byte[] password = new byte[10];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(password);

            pass = Convert.ToBase64String(password);

            label_passwort.Content = "Dein zufällig generiertes Passwort: \n" + pass;

            btn_back.Visibility = Visibility.Visible;
            btn_copy.Visibility = Visibility.Visible;
        }

        private void wnd_Initialized(object sender, EventArgs e)
        {
            btn_back.Visibility = Visibility.Hidden;
            btn_copy.Visibility = Visibility.Hidden;
            label_passwort.Visibility = Visibility.Hidden;
            cb_grossbuchstaben.Visibility = Visibility.Hidden;
            cb_sonderzeichen.Visibility = Visibility.Hidden;
            cb_zahlen.Visibility = Visibility.Hidden;
            tb_laenge.Visibility = Visibility.Hidden;
            btn_create_own_pass.Visibility = Visibility.Hidden;
            cb_kleinbuchstaben.Visibility = Visibility.Hidden;
        }

        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(pass);
            back();
        }

        public void back()
        {
            btn_close.Visibility = Visibility.Visible;
            btn_passwort_own.Visibility = Visibility.Visible;
            btn_webseite.Visibility = Visibility.Visible;
            btn_password_random.Visibility = Visibility.Visible;

            label_passwort.Visibility = Visibility.Hidden;
            btn_back.Visibility = Visibility.Hidden;
            btn_copy.Visibility = Visibility.Hidden;
            btn_create_own_pass.Visibility = Visibility.Hidden;

            cb_grossbuchstaben.Visibility = Visibility.Hidden;
            cb_kleinbuchstaben.Visibility = Visibility.Hidden;
            cb_sonderzeichen.Visibility = Visibility.Hidden;
            cb_zahlen.Visibility = Visibility.Hidden;
            tb_laenge.Visibility = Visibility.Hidden;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            back();
        }

        public void own_passwort(int lenght, bool sz, bool zahlen, bool gb, bool kb)
        {
            char[] lower_case = new char[] { 'z', 'y', 'x', 'w', 'v', 'u', 't', 's', 'r', 'q', 'p', 'o', 'n', 'm', 'l', 'k', 'j', 'i', 'h', 'g', 'f', 'e', 'd', 'c', 'b', 'a', };
            char[] upper_case = new char[] { 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', };
            char[] numbers = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] symbols = new char[] { '!', '"', '§', '$', '%', '&', '/', '(', ')', '=', '?', '+', '*', '~', '#', '-', '_', '.', ':', ',', ';' };

            System.Text.StringBuilder pool = new System.Text.StringBuilder();

            if (kb)
                pool.Append(lower_case);
            if (gb)
                pool.Append(upper_case);
            if (zahlen)
                pool.Append(numbers);
            if (sz)
                pool.Append(symbols);

            if (!kb && !gb && !zahlen && !sz)
            {
                MessageBox.Show("Nichts ausgewählt!");
                back();
            }

            Random random = new Random();

            pass = "";
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    char pass_tmp = pool[random.Next(pool.Length)];
                    pass = pass + pass_tmp;
                }

            }
            catch
            {
                btn_back.Visibility = Visibility.Hidden;
                btn_copy.Visibility = Visibility.Hidden;
            }

            label_passwort.Content = "Dein eigenes neues Passwort: \n" + pass;

            btn_back.Visibility = Visibility.Visible;
            btn_copy.Visibility = Visibility.Visible;
        }

        private void btn_passwort_own_Click(object sender, RoutedEventArgs e)
        {
            btn_close.Visibility = Visibility.Hidden;
            btn_password_random.Visibility = Visibility.Hidden;
            btn_webseite.Visibility = Visibility.Hidden;
            label_passwort.Visibility = Visibility.Visible;
            tb_laenge.Visibility = Visibility.Visible;
            cb_grossbuchstaben.Visibility = Visibility.Visible;
            cb_sonderzeichen.Visibility = Visibility.Visible;
            cb_zahlen.Visibility = Visibility.Visible;
            cb_kleinbuchstaben.Visibility = Visibility.Visible;
            btn_create_own_pass.Visibility = Visibility.Visible;
            label_passwort.Content = "";

        }

        private void btn_create_own_pass_Click(object sender, RoutedEventArgs e)
        {
            int laenge = Convert.ToInt32(tb_laenge.Text);
            bool sonderzeichen = (bool)cb_sonderzeichen.IsChecked;
            bool zahlen = (bool)cb_zahlen.IsChecked;
            bool grosbuchstaben = (bool)cb_grossbuchstaben.IsChecked;
            bool kleinbuchstaben = (bool)cb_kleinbuchstaben.IsChecked;

            own_passwort(laenge, sonderzeichen, zahlen, grosbuchstaben, kleinbuchstaben);
        }

        private void btn_webseite_Click(object sender, RoutedEventArgs e)
        {
            string webseite = "https://www.curtes.de/";
            System.Diagnostics.Process.Start(webseite);
        }
    }
}
