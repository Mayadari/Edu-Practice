using System;
using System.Windows;
using System.Windows.Input;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnTheme_Checked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dark = new ResourceDictionary
            {
                Source = new Uri("/Themes/DarkTheme.xaml", UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(dark);
        }

        private void btnTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary Light = new ResourceDictionary
            {
                Source = new Uri("/Themes/LightTheme.xaml", UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(Light);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
