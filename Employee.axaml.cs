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

public partial class Employee : Window
{
    private string connString = "server=localhost;database=office_supplies;port=3307;User Id=root;password=";
    private MySqlConnection conn;
    private List<Employe> employe;
    private List<Post> post;
    private MySqlConnection connection;
    public Employee()
    {
        InitializeComponent();
        ShowTable(full);
        FillPost();
    }

    private string full = "SELECT employee.id, employee.surname, employee.firstname,employee.lastname, series_s.series, number_s.number, post_s.post, employee.salary,  contract_s.contract, card_s.card FROM `employee` JOIN    `series_s` ON employee.series = series_s.id    JOIN `number_s` ON employee.number = number_s.id  JOIN post_s ON employee.post = post_s.id\n    JOIN contract_s ON employee.contract = contract_s.id   JOIN card_s ON employee.card = card_s.id;";
    public void ShowTable(string sql)
    {
        employe = new List<Employe>();
        conn = new MySqlConnection(connString);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Employe = new Employe()
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                firstname = reader.GetString("firstname"),
                lastname = reader.GetString("lastname"),
                series = reader.GetInt32("series"),
                number = reader.GetInt32("number"),
                post = reader.GetString("post"),
                salary = reader.GetInt32("salary"),
                contract = reader.GetString("contract"),
                card = reader.GetString("card")
            };
            employe.Add(Employe);
        }
        conn.Close();
        Employeer.ItemsSource = employe;
    }
    private void SearchSN(object? sender, TextChangedEventArgs e)
    {
        var gds =employe;
        gds = gds.Where(x => x.surname.Contains(Search_Surname.Text)).ToList();
        Employeer.ItemsSource = gds;
    }
    private void Backs(object? sender, RoutedEventArgs e)
    {
        Choise back = new Choise();
        Close();
        back.Show(); 
    }

    private void Resets(object? sender, RoutedEventArgs e)
    {
        ShowTable(full);
        Search_Surname.Text = string.Empty;
    }

    private void Deleter(object? sender, RoutedEventArgs e)
    {
        try
        {
            Employe employee = Employeer.SelectedItem as Employe;
            if (employe == null)
            {
                return;
            }
            conn = new MySqlConnection(connString);
            conn.Open();
            string sql = "DELETE FROM `employee` WHERE id =" +employee.id;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            employe.Remove(employee);
            ShowTable(full);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void Cmb_Post(object? sender, SelectionChangedEventArgs e)
    {
        var postComboBox = (ComboBox)sender;
        var currentPost = postComboBox.SelectedItem as Post;
        var filteredPost = employe
            .Where(x => x.post == currentPost.post)
            .ToList();
        Employeer.ItemsSource = filteredPost;
    }
    private void SortsV(object? sender, RoutedEventArgs e)
    {
        var sortedSalary = Employeer.ItemsSource.Cast<Employe>().OrderBy(s => s.salary).ToList();
        Employeer.ItemsSource = sortedSalary;
    }
    private void SortsU(object? sender, RoutedEventArgs e)
    {
        var sortedSalary =  Employeer.ItemsSource.Cast< Employe>().OrderByDescending(s => s.salary).ToList();
        Employeer.ItemsSource = sortedSalary;
    }

    public void FillPost()
    {
        post = new List<Post>();
        conn = new MySqlConnection(connString);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from `post_s`", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentPost = new Post()
            {
                id = reader.GetInt32("id"),
                post = reader.GetString("post"),

            };
            post.Add(currentPost);
        }
        conn.Close();
        var postComboBox = this.Find<ComboBox>("CmbPost");
        postComboBox.ItemsSource = post;
    }
    private void Adder(object? sender, RoutedEventArgs e)
    {
        Employe newEmploye = new Employe();
        Adder add = new Adder(newEmploye, employe);
        add.Show(); 
        this.Close();
    }
   
    private void Edit(object? sender, RoutedEventArgs e)
    {
        Employe newEmploye = Employeer.SelectedItem as Employe;
        if (newEmploye==null)
            return;
        Editor edit = new Editor(newEmploye, employe);
        edit.Show(); 
        this.Close();
    }
}