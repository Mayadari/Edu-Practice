using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminMainView.xaml
    /// </summary>
    public partial class AdminMainView : Window
    {
        public AdminMainView()
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

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            EditUserView view = new EditUserView();
            view.Show();
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            RemoveUserView view = new RemoveUserView();
            view.Show();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserView view = new AddUserView();
            view.Show();
        }
    }
}
