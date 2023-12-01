using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProtoLauncher.ViewModels;

namespace ProtoLauncher.Views;

public partial class MainWindow : Window
{
    private int _pressedTimes;
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();
    }
}