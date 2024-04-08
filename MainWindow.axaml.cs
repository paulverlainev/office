using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace office;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private string connectionString = "server=localhost; database=users;port=3307;User Id=root;password=";
    public void Authorization(object? sender, RoutedEventArgs e)
    {
        string login = Login.Text;
        string password = Password.Text;
        if (IsUserValid(login, password))
        {
           Choise choise = new Choise();
            Hide();
           choise.Show();
        }
        else
        {
            Console.WriteLine("Неверный логин или пароль");
        }
    }
        
    private bool IsUserValid(string login, string password) //проверка пользователей по БД
    {
        bool isValid = false;
        
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT COUNT(1) FROM login_s WHERE login = @Login AND password = @Password";
        
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", password);
        
                connection.Open();
        
                object result = command.ExecuteScalar();
                int count = Convert.ToInt32(result);
        
                if (count == 1)
                {
                    isValid = true;
                }
            }
        }
        return isValid;
    }
        
    public void Exit(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}