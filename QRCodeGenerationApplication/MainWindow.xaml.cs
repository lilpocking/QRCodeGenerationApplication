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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QRCodeGenerationApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            ViewModel.NavigationService navigationService = new ViewModel.NavigationService();
            navigationService.NavigateToCreateQRCodePage.Execute(null);
            this.DataContext = navigationService;
            //MainFrame.Navigate(new QRCodeGenerationApplication.View.GenerateFromStringToQr());
            //MainFrame.Content = new QRCodeGenerationApplication.View.GenerateFromStringToQr();
            //MainFrame.Navigate(new QRCodeGenerationApplication.View.test());
        }
    }
}
