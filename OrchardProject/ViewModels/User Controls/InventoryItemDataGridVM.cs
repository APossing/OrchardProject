using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using OrchardProject.BusinessLogic;
using OrchardProject.DAL;
using OrchardProject.Models;

namespace OrchardProject.ViewModels
{
    public class ItemDeletedEventArgs : EventArgs
    {
        public Order Order;
    }

    public class InventoryItemDataGridVM : INotifyPropertyChanged
    {
        private readonly Translator _translator;
        private readonly DataAccessLayer _dal;
        private readonly DataWriter _dataWriter;
        private List<InventoryItem> _newItems = new List<InventoryItem>();

        private OrderFile _orderFile;

        private BasicOrderInfoDataGridVM _orderInfoTable;

        private int _selectedTableIndex;

        public EventHandler<ItemDeletedEventArgs> itemDeletedEvent;

        public InventoryItemDataGridVM()
        {
            _translator = new Translator();
            _dal = new DataAccessLayer(false);
            _dataWriter = new DataWriter();
        }

        public BasicOrderInfoDataGridVM BasicOrderInfoDataGridVM
        {
            get => _orderInfoTable;
            set
            {
                _orderInfoTable = value;
                _orderInfoTable.selectionChangedEvent += OrderEventHandler;
            }
        }

        public string TypeText => _translator.getTranslation("Type");
        public string QuantityKTWOrganicText => _translator.getTranslation("KTW Organic");
        public string QuantitySMWOrganicText => _translator.getTranslation("SMW Organic");
        public string DateTakenText => _translator.getTranslation("Date");
        public string ChemicalText => _translator.getTranslation("Chemical");
        public string RemoveItemText => _translator.getTranslation("Remove");
        public string QuantityText => _translator.getTranslation("Quantity");
        public ICommand RomoveSelectedIndexClickCommand => new GeneralCommand(RemoveSelectedIndex_Click);

        public int SelectedTableIndex
        {
            get => _selectedTableIndex;
            set
            {
                _selectedTableIndex = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<InventoryItem> NewItems
        {
            get => new ObservableCollection<InventoryItem>(_newItems);
            set
            {
                _newItems = value.ToList();
                NotifyPropertyChanged("NewItems");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void waitForDataEvent(BasicOrderInfoDataGridVM vm)
        {
            vm.selectionChangedEvent += OrderEventHandler;
        }

        private void OrderEventHandler(object sender, SelectionChangedEventArgs e)
        {
            _newItems = e.itemList;
            _orderFile = e.Order;
            NotifyPropertyChanged("NewItems");
        }

        public void AddItem(InventoryItem newItem)
        {
            if (_newItems.Select(x => x.Chemical.Name).Contains(newItem.Chemical.Name))
            {
                var item = _newItems.First(x => x.Chemical.Name == newItem.Chemical.Name);
                item.QuantityKTWConventional += newItem.QuantityKTWConventional;
                item.QuantitySMWConventional += newItem.QuantitySMWConventional;
                item.QuantityKTWOrganic += newItem.QuantityKTWOrganic;
                item.QuantitySMWOrganic += newItem.QuantitySMWOrganic;
                item.QuantityTKM += newItem.QuantityTKM;
            }
            else
            {
                _newItems.Add(newItem);
            }

            NotifyPropertyChanged("NewItems");
        }

        private void RemoveSelectedIndex_Click()
        {
            if (SelectedTableIndex != -1)
            {
                _newItems.RemoveAt(SelectedTableIndex);
                NewItems = new ObservableCollection<InventoryItem>(_newItems);
                SelectedTableIndex = -1;
                NotifyPropertyChanged("NewItems");
                NotifyPropertyChanged("SelectedTableIndex");

                try
                {
                    _orderFile.Order.NumberOfChems--;
                    if (_orderFile.Order.NumberOfChems == 0)
                        _orderFile.FileInfo.Delete();
                    else
                        _dataWriter.XMLWriteOrder(_orderFile.Order);

                    var eventArgs = new ItemDeletedEventArgs();
                    eventArgs.Order = _orderFile.Order;
                    itemDeletedEvent(this, eventArgs);
                }
                catch
                {
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveItemsLocal()
        {
            _dal.InsertListMultipleInventoryItems(_newItems);
        }

        public void SaveToExcel()
        {
        }

        public void AddListItems(List<InventoryItem> items)
        {
            _newItems = items;
            NotifyPropertyChanged("NewItems");
        }
    }
}