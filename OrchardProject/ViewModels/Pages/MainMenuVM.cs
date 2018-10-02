using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;

namespace OrchardProject.ViewModels
{
    class MainMenuVM : INotifyPropertyChanged, INavigation
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand BulkInventoryButtonClick => new GeneralCommand(BulkInventory_Clicked);
        public ICommand ReturnInventoryButtonClick => new GeneralCommand(ReturnInventory_Clicked);
        public ICommand SettingsButtonClick => new GeneralCommand(Settings_Clicked);
        public ICommand OrderManagerButtonClick => new GeneralCommand(OrderManagerButton_Clicked);
        private INavigation _nav;

        public MainMenuVM(INavigation nav)
        {
            _nav = nav;
        }

        public MainMenuVM()
        {
        }

        private void BulkInventory_Clicked()
        {
            Views.BulkInventoryView newInventoryPage = new Views.BulkInventoryView();
            newInventoryPage.DataContext = new ViewModels.BulkInventoryVM(this);
            _nav.Navigate(newInventoryPage);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ReturnInventory_Clicked()
        {
            Views.ReturnInventoryView newReturnInventoryPage = new Views.ReturnInventoryView();
            _nav.Navigate(newReturnInventoryPage);
        }

        private void Settings_Clicked()
        {
            Views.SettingsView newSettingsView = new Views.SettingsView();
            newSettingsView.DataContext = new ViewModels.SettingsVM(this);
            _nav.Navigate(newSettingsView);
        }

        private void OrderManagerButton_Clicked()
        {
            Views.OrderManagerView newView = new Views.OrderManagerView();
            newView.DataContext = new ViewModels.OrderManagerVM(this);
            _nav.Navigate(newView);
        }

        public void Navigate(Page p)
        {
            _nav.Navigate(p);
        }
    }
}