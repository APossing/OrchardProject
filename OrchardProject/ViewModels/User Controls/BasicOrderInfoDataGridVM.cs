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
    public class SelectionChangedEventArgs : EventArgs
    {
        public OrderFile Order;
        public List<InventoryItem> itemList;
    }

    public class BasicOrderInfoDataGridVM : INotifyPropertyChanged
    {
        public EventHandler<SelectionChangedEventArgs> selectionChangedEvent;

        private void ChangeTable()
        {
            SelectionChangedEventArgs eventArgs = new SelectionChangedEventArgs();
            try
            {
                eventArgs.itemList = _orders[_selectedTableIndex].Order.Items;
                eventArgs.Order = _orders[_selectedTableIndex];
                selectionChangedEvent(this, eventArgs);
            }
            catch
            {
                eventArgs.itemList = new List<InventoryItem>();
                //eventArgs.Order = _orders[_selectedTableIndex];
                selectionChangedEvent(this, eventArgs);
            }
        }

        public void WaitForItemDeletion(InventoryItemDataGridVM vm)
        {
            vm.itemDeletedEvent += DeleteEventHandler;
        }

        private void DeleteEventHandler(object sender, ItemDeletedEventArgs e)
        {
            _orders[_selectedTableIndex].Order = e.Order;
            if (_orders[_selectedTableIndex].Order.NumberOfChems == 0)
                _orders.RemoveAt(_selectedTableIndex);
            NotifyPropertyChanged("Orders");
        }


        public BasicOrderInfoDataGridVM()
        {
            _translator = new Translator();
            SelectedTableIndex = -1;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private List<OrderFile> _orders = new List<OrderFile>();
        private readonly Translator _translator;
        public string TypeText => _translator.getTranslation("Type");
        public string OrderNameText => _translator.getTranslation("Order Name");
        public string QuantityKTWOrganicText => _translator.getTranslation("KTW Organic");
        public string QuantitySMWOrganicText => _translator.getTranslation("SMW Organic");
        public string DateTakenText => _translator.getTranslation("Date");
        public string TotalChemicalsText => _translator.getTranslation("Total Chemicals");
        public string ChemicalText => _translator.getTranslation("Chemical");
        public string RemoveItemText => _translator.getTranslation("Remove");
        public string QuantityText => _translator.getTranslation("Quantity");
        public ICommand RomoveSelectedIndexClickCommand => new GeneralCommand(RemoveSelectedIndex_Click);

        /// <summary>
        /// No duplicated allowed
        /// </summary>
        /// <param name="newItem"></param>
        public void AddItem(OrderFile newItem)
        {
            //if (_newItems.Select(x => x.Chemical.Name).Contains(newItem.Chemical.Name))
            //{
            //    InventoryItem item = _newItems.First(x => x.Chemical.Name == newItem.Chemical.Name);
            //    item.QuantityKTWConventional += newItem.QuantityKTWConventional;
            //    item.QuantitySMWConventional += newItem.QuantitySMWConventional;
            //    item.QuantityKTWOrganic += newItem.QuantityKTWOrganic;
            //    item.QuantitySMWOrganic += newItem.QuantitySMWOrganic;
            //    item.QuantityTKM += newItem.QuantityTKM;
            //}
            //else
            _orders.Add(newItem);
            NotifyPropertyChanged("Orders");
        }

        private int _selectedTableIndex;

        public int SelectedTableIndex
        {
            get => _selectedTableIndex;
            set
            {
                
                _selectedTableIndex = value;
                if (SelectedTableIndex >= 0)
                    ChangeTable();

                NotifyPropertyChanged();
            }
        }

        private void RemoveSelectedIndex_Click()
        {
            if (SelectedTableIndex == -1)
                return;

            _orders[SelectedTableIndex].FileInfo.Delete();
            _orders.RemoveAt(SelectedTableIndex);
            Orders = new ObservableCollection<OrderFile>(_orders);
            SelectedTableIndex = -1;
            NotifyPropertyChanged("Orders");
            NotifyPropertyChanged("SelectedTableIndex");
        }


        public List<OrderFile> getOrdersList => _orders.ToList();

        public ObservableCollection<OrderFile> Orders
        {
            get => new ObservableCollection<OrderFile>(_orders);
            set
            {
                _orders = value.ToList();
                NotifyPropertyChanged("Orders");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}