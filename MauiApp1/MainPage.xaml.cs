using MauiApp1.CustomCode;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            new Thread(() =>
            {
                bool running = true;

                while (running)
                {
                    Thread.Sleep(1000 / 60);

                    if (Application.Current.Windows[0].Page.IsEnabled == false) running = false;

                    if (running) Application.Current.Dispatcher.Dispatch(() => Canvas.Invalidate());
                }
            }).Start();
        }
    }
}