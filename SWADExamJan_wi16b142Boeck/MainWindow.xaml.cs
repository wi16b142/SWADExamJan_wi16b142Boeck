using System.Windows;
using SWADExamJan_wi16b142Boeck.ViewModel;

namespace SWADExamJan_wi16b142Boeck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}