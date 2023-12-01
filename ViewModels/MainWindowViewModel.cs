using System;
using System.Collections.Generic;
using System.Windows.Input;
using Avalonia.Controls;
using Microsoft.CodeAnalysis;
using ProtoLauncher.Library.Classes;
using ProtoLauncher.ViewModels.Interfaces;
using ReactiveUI;

namespace ProtoLauncher.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentPageView = new MainInterfaceViewModel();
    private double _normalSidebarWidth = 230;
    private double _compactSidebarWidth = 60;
    private double _currentSidebarWidth;
    private string _sidebarToggleButtonContent = "<";
    private bool _isSidebarExtended = true;
    private SidebarItemTemplate _selectedSidebarItem;
    private List<SidebarItemTemplate> _sidebarItems = new()
    {
        new SidebarItemTemplate(typeof(MainInterfaceViewModel), "Home", "HomeRegular"),
        new SidebarItemTemplate(typeof(InstancesInterfaceViewModel), "Instances", "AppsRegular"),
        new SidebarItemTemplate(typeof(MainInterfaceViewModel), "Home", "BlockRegular"),
        new SidebarItemTemplate(typeof(MainInterfaceViewModel), "Home", "home_regular"),
    };

    public MainWindowViewModel()
    {
        CurrentSidebarWidth = _normalSidebarWidth;
        SelectedSidebarItem = SidebarItems[0];
    }
    
    public ViewModelBase CurrentPageView
    {
        get => _currentPageView;
        set => this.RaiseAndSetIfChanged(ref _currentPageView, value);
    }

    public bool IsSidebarExtended
    {
        get => _isSidebarExtended;
        set => this.RaiseAndSetIfChanged(ref _isSidebarExtended, value);
    }
    
    public double CurrentSidebarWidth
    {
        get => _currentSidebarWidth;
        set => this.RaiseAndSetIfChanged(ref _currentSidebarWidth, value);
    }

    public double NormalSidebarWidth
    {
        get => _normalSidebarWidth;
        set => this.RaiseAndSetIfChanged(ref _normalSidebarWidth, value);
    }
    
    public double CompactSidebarWidth
    {
        get => _compactSidebarWidth;
        set => this.RaiseAndSetIfChanged(ref _compactSidebarWidth, value);
    }

    public string SidebarToggleButtonContent
    {
        get => _sidebarToggleButtonContent;
        set => this.RaiseAndSetIfChanged(ref _sidebarToggleButtonContent, value);
    }

    public List<SidebarItemTemplate> SidebarItems
    {
        get => _sidebarItems;
        set => this.RaiseAndSetIfChanged(ref _sidebarItems, value);
    }
    
    public SidebarItemTemplate SelectedSidebarItem
    {
        get => _selectedSidebarItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedSidebarItem, value);
            OnSelectedListItemChanged(value);
        }
    }

    public void ToggleSidebar()
    {
        IsSidebarExtended = !IsSidebarExtended;
        CurrentSidebarWidth = IsSidebarExtended ? NormalSidebarWidth : CompactSidebarWidth;
        SidebarToggleButtonContent = IsSidebarExtended ? "<" : ">";
    }

    public void OnSelectedListItemChanged(SidebarItemTemplate value)
    {
        // var instance = Activator.CreateInstance(value.ModelType);
        var instance = value.ViewModel;
        if (instance is null) return;
        CurrentPageView = instance;
    }
}