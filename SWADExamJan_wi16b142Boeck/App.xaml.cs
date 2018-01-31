using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace SWADExamJan_wi16b142Boeck
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
