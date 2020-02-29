using System.Windows;
using Sharp_lab02_stavrovskyi.ViewModels;


namespace Sharp_lab02_stavrovskyi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
