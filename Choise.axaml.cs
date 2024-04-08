using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace office;

public partial class Choise : Window
{
    public Choise()
    {
        InitializeComponent();
    }
    private void Employee(object? sender, RoutedEventArgs e)
    {
        Employee empl = new Employee();
        this.Close();
       empl.Show();
    }

    private void Tovar(object? sender, RoutedEventArgs e)
    {
       // Tovar tovar = new Tovar();
        this.Close();
        //tovar.Show();
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}