using RentAppViewModels.ViewModels;
using System.Windows;

namespace RentAppViews.Views
{
    /// <summary>
    /// Логика взаимодействия для ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        public ContentWindow()
        {
            InitializeComponent();
            DataContext = new ContentVM();
        }
    }
}
