// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using FontAwesome.WPF;
using Wpf.Ui.Controls;

namespace AnotherByrd.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "WPF UI - UiDesktopApp1";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Icon = new SymbolIcon { Symbol = SymbolRegular.Mail48, ToolTip = "Mails"},
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem
            {
                Icon = new SymbolIcon{Symbol = SymbolRegular.Book20, ToolTip = "Contacts"},
                TargetPageType = typeof(Views.Pages.Addressbook)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}