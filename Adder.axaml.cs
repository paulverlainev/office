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

public partial class Adder : Window
{
    private List<Employe> Empl;
    private Employe NewEmploye;
    private List<Post> posts;
    public Adder(Employe newEmploye, List<Employe> empl)
    {
        InitializeComponent();
        NewEmploye = newEmploye;
        this.DataContext = newEmploye;
        Empl = empl;
    }
    private MySqlConnection conn;
    string connString = "server=localhost;database=office_supplies;port=3307;User Id=root;password=";
private void SaveSrs(object? sender, RoutedEventArgs e)
    {
        var emp = Empl.FirstOrDefault(x => x.id == NewEmploye.id);
        if (emp == null)
        {
            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();
                string addsrs = "INSERT INTO `series_s`(series) VALUES ('"+ Convert.ToInt32(series.Text) + "');";
                string addnmb = "INSERT INTO `number_s`(number) VALUES ('"+ Convert.ToInt32(number.Text) + "');";
                string addcnt = "INSERT INTO `contract_s`(contract) VALUES ('"+ contract.Text + "');";
                string addcrd = "INSERT INTO `card_s`(card) VALUES ('"+ card.Text + "');";
                MySqlCommand cmdsrs = new MySqlCommand(addsrs, conn);
                MySqlCommand cmdnmb = new MySqlCommand(addnmb, conn);
                MySqlCommand cmdcnt = new MySqlCommand(addcnt, conn);
                MySqlCommand cmdcrd = new MySqlCommand(addcrd, conn);
                cmdsrs.ExecuteNonQuery();
                cmdnmb.ExecuteNonQuery();
                cmdcnt.ExecuteNonQuery();
                cmdcrd.ExecuteNonQuery();
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
    private void Addss(object? sender, RoutedEventArgs e)
    {
        Employe newEmploye = new Employe();
        AdderE ae = new AdderE(newEmploye, Empl);
        this.Close();
        ae.Show();
    }
}