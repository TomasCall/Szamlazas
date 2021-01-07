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
using MySql.Data.MySqlClient;
namespace Számlázás
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string cmmnd = "Select distinct nev From adatok order by nev";
            MySqlConnection connect = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=szamla");
            MySqlCommand command = new MySqlCommand(cmmnd, connect);
            connect.Open();
            MySqlDataReader myreader = command.ExecuteReader();
            while (myreader.Read())
            {
                customerName.Items.Add(myreader.GetValue(0).ToString());
            }
            connect.Close();
        }
        private void query_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.Show();
            inputs.Close();
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            DateTime accountStartVar = DateTime.Parse(accountStart.Text.ToString());
            DateTime deadlineVar = DateTime.Parse(deadline.SelectedDate.ToString()); //accountStartVar.Year + "-" + accountStartVar.Month + "-" + accountStartVar.Day;
            string accountStartVarS = accountStartVar.Year + "-" + accountStartVar.Month + "-" + accountStartVar.Day;
            string deadlineVarS = deadlineVar.Year + "-" + deadlineVar.Month + "-" + deadlineVar.Day;
            string cmmnd = string.Format("Insert into adatok(szam,nev,osszeg,kiallitas,hatarido,teljesitve) Values(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"" + false + "\");", accountNumber.Text.ToString(), customerName.Text.ToString(), accountPrice, accountStartVarS, deadlineVarS);
            MySqlConnection connect = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=szamla");
            MySqlCommand command = new MySqlCommand(cmmnd, connect);
            connect.Open();
            command.ExecuteReader();
            Console.WriteLine(cmmnd);
            connect.Close();
            accountNumber.Text = "";
            customerName.Text = "";
            accountPrice.Text = "";
            accountStart.Text = "";
            deadline.Text = "";
        }
    }
}
