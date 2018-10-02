using System.Windows.Controls;

namespace OrchardProject.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        private ViewModels.MainMenuVM _ViewModel { get; set; }

        public MainMenu()
        {
            InitializeComponent();
            _ViewModel = new ViewModels.MainMenuVM();
            DataContext = _ViewModel;
        }
    }
}