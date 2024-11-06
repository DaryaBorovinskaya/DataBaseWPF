using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Timer = System.Windows.Threading.DispatcherTimer;




namespace DataBaseWPF
{
    /// <summary>
    /// Логика взаимодействия для LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        Timer timerForWindow = new Timer();
        Assembly assembly = Assembly.GetExecutingAssembly();

        public LogInWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timerForWindow.Interval = new System.TimeSpan(100);
            timerForWindow.Start();
            timerForWindow.Tick += Timer_Tick;

            textBlockVersion.Text = "Версия" + assembly.GetName().Version.ToString();
        }

        

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            if (Console.CapsLock)
                textBlockCapsLock.Text = "Клавиша CapsLock нажата";
            else
                textBlockCapsLock.Text = "";

            textBlockRusEng.Text = "Язык ввода " + InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
        }


        private void ButtonCancellationClick(object sender, RoutedEventArgs e)
        {
            textBoxName.Clear();
            textBoxPassword.Clear();
        }

        
    }
}
