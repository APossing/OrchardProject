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

// ReSharper disable ExplicitCallerInfoArgument

namespace OrchardProject.ViewModels
{
    public class BulkInventoryVM : INotifyPropertyChanged, IDataErrorInfo, INavigation
    {
        private INavigation _nav;
        private DataWriter _dataWriter;
        private DataReader _dataReader;
        private Translator _translator;
        public string backText => _translator.getTranslation("Back");

        public InventoryItemDataGridVM DataGridVm { get; private set; }

        private List<String> _clasification;

        public List<String> Clasification
        {
            get
            {
                _clasification.Add(_translator.getTranslation("Organic"));
                _clasification.Add(_translator.getTranslation("Conventional"));
                return _clasification;
            }
        }

        private string _selectedClasification;

        public string SelectedClasification
        {
            get => _selectedClasification;
            set
            {
                _selectedClasification = value;
                NotifyPropertyChanged();
            }
        }

        public string ChemicalText => _translator.getTranslation("Chemical");

        public string QuantityText => _translator.getTranslation("Quantity");

        public string AddToOrderText => _translator.getTranslation("Add To Order");

        public string OrderNameText => _translator.getTranslation("Order Name");

        public string DateTakenText => _translator.getTranslation("Date");

        public string TypeText => _translator.getTranslation("Type");

        public string SubmitText => _translator.getTranslation("Submit");

        public string QuantityKTWOrganicText => _translator.getTranslation("KTW Organic");

        public string QuantitySMWOrganicText => _translator.getTranslation("SMW Organic");

        public string PrintText => _translator.getTranslation("Print");
        private string _orderNameTextBox;

        public string OrderNameTextBox
        {
            get => _orderNameTextBox;
            set
            {
                _orderNameTextBox = value;
                NotifyPropertyChanged();
            }
        }

        private int _selectedTableIndex;

        public int SelectedTableIndex
        {
            get => _selectedTableIndex;
            set
            {
                _selectedTableIndex = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SubmitButtonClickCommand => new GeneralCommand(Submit_Click);
        public ICommand OrderButtonClickCommand => new GeneralCommand(OrderButtonClick);
        public ICommand BackButtonClick => new GeneralCommand(BackButton_Click);
        public ICommand PrintButtonClickCommand => new GeneralCommand(PrintButton_Click);
        private double _quantityRemaining;
        public List<double> SMWQuantities => QuantityBuilder(_selectedSMW);
        public List<double> KTWQuantities => QuantityBuilder(_selectedKTW);
        public List<double> TKMQuantities => QuantityBuilder(_selectedTKM);

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly DAL.DataAccessLayer _dal;
        public Chemical[] Chemicals { get; }
        private double _selectedSMW;

        public double SelectedSMW
        {
            get => _selectedSMW;
            set
            {
                _quantityRemaining -= (value - _selectedSMW);
                NotifyPropertyChanged("KTWQuantities");
                NotifyPropertyChanged("SelectedKTW");
                NotifyPropertyChanged("TKMQuantities");
                NotifyPropertyChanged("SelectedTKM");
                _selectedSMW = value;
            }
        }

        private double _selectedKTW;

        public double SelectedKTW
        {
            get => _selectedKTW;
            set
            {
                _quantityRemaining -= (value - _selectedKTW);
                NotifyPropertyChanged("SMWQuantities");
                NotifyPropertyChanged("SelectedSMW");
                NotifyPropertyChanged("TKMQuantities");
                NotifyPropertyChanged("SelectedTKM");
                _selectedKTW = value;
            }
        }

        private double _selectedTKM;

        public double SelectedTKM
        {
            get => _selectedTKM;
            set
            {
                _quantityRemaining -= (value - _selectedTKM);
                NotifyPropertyChanged("KTWQuantities");
                NotifyPropertyChanged("SelectedKTW");
                NotifyPropertyChanged("SMWQuantities");
                NotifyPropertyChanged("SelectedSMW");
                _selectedTKM = value;
            }
        }

        private Chemical _selectedChemical;

        public Chemical SelectedChemical
        {
            get => _selectedChemical;
            set
            {
                QuantityType = value.QuantityType;
                NotifyPropertyChanged("QuantityType");
                Quantity = 0;
                NotifyPropertyChanged("Quantity");
                resetAndUpdateOrchardQuantities();
                _selectedChemical = value;
            }
        }

        private DataTable _dataTable;

        private double _quantity;

        public double Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                resetAndUpdateOrchardQuantities();
            }
        }

        public BulkInventoryVM()
        {
            DataGridVm = new InventoryItemDataGridVM();
            _dal = new DAL.DataAccessLayer(false);
            Chemicals = _dal.Chemicals;
            Quantity = 0;
            _dataWriter = new DataWriter("moneyExcel", "moneyXML", "moneyPDF");
            _dataReader = new DataReader();
            NotifyPropertyChanged("ChemicalText");
            _clasification = new List<string>();
            _translator = new Translator();
        }

        public BulkInventoryVM(INavigation nav) : this()
        {
            _nav = nav;
        }

        private void FillPastItems()
        {
            List<InventoryItem> readItems = _dataReader.ReadXML("moneyXML");
            if (readItems != null)
            {
                foreach (InventoryItem newItem in readItems)
                    DataGridVm.AddItem(newItem);
            }
        }

        private void resetAndUpdateOrchardQuantities()
        {
            _quantityRemaining = _quantity;
            _selectedSMW = 0;
            _selectedKTW = 0;
            _selectedTKM = 0;
            NotifyPropertyChanged("SelectedSMW");
            NotifyPropertyChanged("SelectedKTW");
            NotifyPropertyChanged("SelectedTKM");
            NotifyPropertyChanged("SMWQuantities");
            NotifyPropertyChanged("KTWQuantities");
            NotifyPropertyChanged("TKMQuantities");
        }

        public string QuantityType { get; set; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => Validate(columnName);

        private string Validate(string propertyName)
        {
            string validationMessage = "";
            switch (propertyName)
            {
                case "Quantity":
                    if (Quantity <= 0)
                        validationMessage = "Must be greater than 0";
                    else if (Quantity % SelectedChemical.QuantityInUnit != 0)
                        validationMessage = "Quantity must be a multiple of chemical unit amount";
                    break;
                case "SelectedChemical":
                    if (SelectedChemical == null)
                        validationMessage = "Must have a chemical selected";
                    break;
                case "SelectedKTW":
                case "SelectedSMW":
                case "SelectedTKM":
                    if (_quantityRemaining != 0.0)
                        validationMessage = "Not all quantity used";
                    break;
                case "SelectedClasification":
                    if (SelectedClasification == null)
                        validationMessage = "No Clasification selected";
                    break;
            }

            return validationMessage;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SubmitButtonReady()
        {
            if (DataGridVm.NewItems.Count <= 0)
                return false;

            return true;
        }
        private void Submit_Click()
        {
            if (SubmitButtonReady())
            {
                Order newOrder = new Order
                {
                    Name = _orderNameTextBox,
                    NumberOfChems = DataGridVm.NewItems.Count,
                    TimeSubmited = DateTime.Now,
                    Items = DataGridVm.NewItems.ToList<InventoryItem>()
                };
                _dataWriter.XMLWriteOrder(newOrder);
                _dal.SaveDataLocal(_dataTable, "LocalDataWINNN");
                BackButton_Click();
            }

        }

        private bool OrderButtonReady
        {
            get
            {
                if (Quantity > 0 && _quantityRemaining == 0.0)
                    return true;
                return false;
            }
        }

        private void OrderButtonClick()
        {
            if (OrderButtonReady == false)
                return;
            InventoryItem item = new InventoryItem
            {
                Chemical = SelectedChemical,
                DateTaken = DateTime.Now,
                QuantityTotal = Quantity
            };
            if (SelectedClasification == _translator.getTranslation("Conventional"))
            {
                item.QuantityKTWConventional += SelectedKTW;
                item.QuantitySMWConventional += SelectedSMW;
            }
            else
            {
                item.QuantityKTWOrganic += SelectedKTW;
                item.QuantitySMWOrganic += SelectedSMW;
            }

            item.QuantityTKM += SelectedTKM;
            DataGridVm.AddItem(item);
        }

        private void BackButton_Click()
        {
            Views.MainMenu newMainMenu = new Views.MainMenu {DataContext = new ViewModels.MainMenuVM(this)};
            _nav.Navigate(newMainMenu);
        }

        public void Navigate(Page p)
        {
            _nav.Navigate(p);
        }

        private void PrintButton_Click()
        {
        }

        private List<double> QuantityBuilder(double selectedQuantity)
        {
            List<double> quantitiesList = new List<double>();
            if (_quantityRemaining + selectedQuantity != 0 && SelectedChemical != null)
            {
                for (double i = 0; i <= _quantityRemaining + selectedQuantity; i += SelectedChemical.QuantityInUnit)
                {
                    quantitiesList.Add(i);
                }
            }
            else
            {
                quantitiesList.Add(0.0);
            }

            return quantitiesList;
        }
    }
}