using Avalonia.Controls;
using Avalonia; 
using Avalonia.Markup.Xaml; 
using System.Collections.Generic; 
using System.Linq; 
using Avalonia.Interactivity; 
using MySql.Data.MySqlClient; 
using System; 
using System.IO; 
using System.Windows; 

namespace office;

public partial class Editor : Window
{
    private List<Employe> Empl;
    private Employe NewEmploye;
    private List<Post> posts;
    public Editor(Employe newEmploye, List<Employe> empl)
    {
        InitializeComponent();
        NewEmploye = newEmploye;
        this.DataContext = newEmploye;
        Empl = empl;
    }
    private MySqlConnection conn;
    string connString = "server=localhost;database=office_supplies;port=3307;User Id=root;password=";
    private void Edit(object? sender, RoutedEventArgs e)
    {
        var emp = Empl.FirstOrDefault(x => x.id == NewEmploye.id);
        if (emp != null)
        {
            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();
                string edit =  "UPDATE `employee` SET surname = '" + surname.Text + "', firstname = '" + firstname.Text + "', lastname = '" + lastname.Text + "', series = " + Convert.ToInt32(series.Text) + ", number = " + Convert.ToInt32(number.Text) + ", post = " + Convert.ToInt32(post.Text) + ", salary = " + Convert.ToInt32(salary.Text) + ", contract = " + Convert.ToInt32(contract.Text) + ", card = " + Convert.ToInt32(card.Text) + " WHERE id = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(edit, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Employee emp = new Employee();
        this.Close();
        emp.Show();
    }
}