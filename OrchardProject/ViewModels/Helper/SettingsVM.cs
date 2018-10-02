using System;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;

namespace OrchardProject.ViewModels
{
    public class SettingsVM : INotifyPropertyChanged, INavigation
    {
        public ICommand SpanishLanguageClicked => new GeneralCommand(SpanishLanguage_Clicked);
        public ICommand EnglishLanguageClicked => new GeneralCommand(EnglishLanguage_Clicked);
        public ICommand BackButtonClick => new GeneralCommand(BackButton_Clicked);

        private INavigation _nav;

        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush LanguageSpanishBackground
        {
            get
            {
                if (Properties.Settings.Default.Language == "es")
                {
                    Color color = Color.FromRgb(107, 107, 107);
                    return new SolidColorBrush(color);
                }
                else
                {
                    Color color = Color.FromArgb(100, 107, 107, 107);
                    return new SolidColorBrush(color);
                }
            }
            set
            {
                Properties.Settings.Default.Language = "es";
                Properties.Settings.Default.Save();
                NotifyPropertyChanged("LanguageEnglishBackground");
                NotifyPropertyChanged("LanguageSpanishBackground");
            }
        }

        public SolidColorBrush LanguageEnglishBackground
        {
            get
            {
                if (Properties.Settings.Default.Language == "en")
                {
                    Color color = Color.FromRgb(107, 107, 107);
                    return new SolidColorBrush(color);
                }
                else
                {
                    Color color = Color.FromArgb(100, 107, 107, 107);
                    return new SolidColorBrush(color);
                }
            }
        }

        public SettingsVM(INavigation nav)
        {
            _nav = nav;
        }

        public SettingsVM()
        {
        }


        private void SpanishLanguage_Clicked()
        {
            Properties.Settings.Default.Language = "es";
            Properties.Settings.Default.Save();
            NotifyPropertyChanged("LanguageEnglishBackground");
            NotifyPropertyChanged("LanguageSpanishBackground");
        }

        private void EnglishLanguage_Clicked()
        {
            Properties.Settings.Default.Language = "en";
            Properties.Settings.Default.Save();
            NotifyPropertyChanged("LanguageEnglishBackground");
            NotifyPropertyChanged("LanguageSpanishBackground");
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Navigate(Page p)
        {
            _nav.Navigate(p);
        }

        private void BackButton_Clicked()
        {
            Views.MainMenu newMainMenu = new Views.MainMenu();
            newMainMenu.DataContext = new ViewModels.MainMenuVM(this);
            _nav.Navigate(newMainMenu);
        }
    }
}