using System.ComponentModel;
using System.Windows.Controls;

namespace OrchardProject.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged, INavigation
    {
        private Page _currentPage;

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                NotifyPropertyChanged("CurrentPage");
            }
        }

        public MainWindowVM()
        {
            Views.MainMenu p = new Views.MainMenu();
            p.DataContext = new ViewModels.MainMenuVM(this);

            CurrentPage = p;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void BulkInventoryClick()
        {
            CurrentPage = new Views.BulkInventoryView();
            NotifyPropertyChanged("CurrentPage");
        }

        public void Navigate(Page p)
        {
            CurrentPage = p;
        }
    }
}