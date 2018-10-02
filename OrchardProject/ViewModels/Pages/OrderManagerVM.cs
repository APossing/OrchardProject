using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows.Input;
using System.Data;
using OrchardProject.Models;
using System.Windows.Controls;
using OrchardProject.BusinessLogic;
using System.Collections.ObjectModel;

namespace OrchardProject.ViewModels
{
    public class OrderManagerVM : INotifyPropertyChanged, IDataErrorInfo, INavigation
    {
        private INavigation _nav;
        private DataReader _dataReader;
        private Translator _translator;
        public InventoryItemDataGridVM DataGridVm { get; set; }
        public string backText => _translator.getTranslation("Back");

        public ICommand BackButtonClick => new GeneralCommand(BackButton_Clicked);
        public BasicOrderInfoDataGridVM BasicOrderGridVm { get; set; }

        public OrderManagerVM()
        {
            _dataReader = new DataReader();
            _translator = new Translator();

            DataGridVm = new InventoryItemDataGridVM();
            BasicOrderGridVm = new BasicOrderInfoDataGridVM();
            DataGridVm.waitForDataEvent(BasicOrderGridVm);
            BasicOrderGridVm.WaitForItemDeletion(DataGridVm);
            FillOrders();
        }

        private void FillOrders()
        {
            List<OrderFile> orders = _dataReader.getOrders();
            foreach (OrderFile newOrder in orders)
            {
                BasicOrderGridVm.AddItem(newOrder);
            }
        }

        public OrderManagerVM(INavigation nav) : this()
        {
            _nav = nav;
        }

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        public void Navigate(Page p)
        {
            _nav.Navigate(p);
        }

        public void BackButton_Clicked()
        {
            Views.MainMenu newMainMenu = new Views.MainMenu();
            newMainMenu.DataContext = new ViewModels.MainMenuVM(this);
            _nav.Navigate(newMainMenu);
        }
    }
}