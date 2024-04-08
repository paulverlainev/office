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

public partial class Tovars : Window
{
    private string connString = "server=localhost;database=office_supplies;port=3307;User Id=root;password=$t@1Krnl";
    private MySqlConnection conn;
    private List<Tovar> tovar;
    private List<Firm> firm;
    private MySqlConnection connection;
    public Tovars()
    {
        InitializeComponent();
        ShowTable(full);
        FillFirm();
    }

    private string full = "";
    public void ShowTable(string sql)
    {
        firm = new List<Firm>();
        conn = new MySqlConnection(connString);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Tovar = new Tovar()
            {
                id = reader.GetInt32("id"),
                product_name = reader.GetString("product_name"),
                product_type = reader.GetInt32("product_type"),
                firm = reader.GetInt32("firm"),
                matherial = reader.GetInt32("matherial"),
                kathegory = reader.GetInt32("kathegory"),
                provider = reader.GetInt32("provider"),
                price = reader.GetInt32("price")
            };
            tovar.Add(Tovar);
        }
        conn.Close();
        Tovaars.ItemsSource = tovar;
    }
    private void SearchSN(object? sender, TextChangedEventArgs e)
    {
        var gds =tovar;
        gds = gds.Where(x => x.product_name.Contains(Search_Product.Text)).ToList();
        Tovaars.ItemsSource = gds;
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
        Search_Product.Text = string.Empty;
    }

    private void Deleter(object? sender, RoutedEventArgs e)
    {
        try
        {
            Tovar tovars = Tovaars.SelectedItem as Tovar;
            if (tovar == null)
            {
                return;
            }
            conn = new MySqlConnection(connString);
            conn.Open();
            string sql = "DELETE FROM `tovar` WHERE id =" + tovars.id;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            tovar.Remove(tovars);
            ShowTable(full);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void Cmb_Firm(object? sender, SelectionChangedEventArgs e)
    {
        var firmComboBox = (ComboBox)sender;
        var currentFirm = firmComboBox.SelectedItem as Firm;
        var filteredFirm = tovar
            .Where(x => x.firm == currentFirm.firm)
            .ToList();
        Tovaars.ItemsSource = filteredFirm;
    }
    private void SortsV(object? sender, RoutedEventArgs e)
    {
        var sortedPrice = Tovaars.ItemsSource.Cast<Tovar>().OrderBy(s => s.price).ToList();
        Tovaars.ItemsSource = sortedPrice;
    }
    private void SortsU(object? sender, RoutedEventArgs e)
    {
        var sortedPrice =   Tovaars.ItemsSource.Cast<Tovar>().OrderByDescending(s => s.price).ToList();
        Tovaars.ItemsSource = sortedPrice;
    }

    public void FillFirm()
    {
        firm = new List<Firm>();
        conn = new MySqlConnection(connString);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from `firm_s`", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentFirm = new Firm()
            {
                id = reader.GetInt32("id"),
                firm = reader.GetString("firm"),
            };
            firm.Add(currentFirm);
        }
        conn.Close();
        var firmComboBox = this.Find<ComboBox>("CmbFirm");
        firmComboBox.ItemsSource = firm;
    }
    private void Adder(object? sender, RoutedEventArgs e)
    {
        Tovar newTovar = new Tovar();
        Adder1 add1 = new Adder1(newTovar, tovar);
        add1.Show(); 
        this.Close();
    }
   
    private void Edit(object? sender, RoutedEventArgs e)
    {
        Tovar newTovar = Tovaars.SelectedItem as Tovar;
        if (newTovar==null)
            return;
        Editor1 edit1 = new Editor1(newTovar, tovar);
        edit1.Show(); 
        this.Close();
    }
}