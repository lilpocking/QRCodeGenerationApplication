using System.Windows;
using System.Windows.Navigation;

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
        }
    }
}
