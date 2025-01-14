namespace LocalNotificationWindows_Test2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "LocalNotificationWindows_Test2" };
        }
    }
}
