using Microsoft.UI.Xaml;
using Microsoft.Windows.AppNotifications;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LocalNotificationWindows_Test2.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        private NotificationManager notificationManager;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            notificationManager = new NotificationManager();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            notificationManager.Init();

            AppLaunchArguments.LaunchArguments = args.Arguments;

            if (string.IsNullOrEmpty(AppLaunchArguments.LaunchArguments) ) 
            {
                string[] arguments = Environment.GetCommandLineArgs();
                AppLaunchArguments.LaunchArgumentsGetCommandLineArgs = string.Join(" ", arguments);
            }

            base.OnLaunched(args);
        }

        private void OnProcessExit(object? sender, EventArgs e)
        {
            notificationManager.Unregister();
        }
    }

    internal class NotificationManager
    {
        private bool m_isRegistered;

        private Dictionary<int, Action<AppNotificationActivatedEventArgs>> c_map;

        public NotificationManager()
        {
            m_isRegistered = false;

            // When adding new a scenario, be sure to add its notification handler here.
            c_map = new Dictionary<int, Action<AppNotificationActivatedEventArgs>>();
            c_map.Add(9813, My9813NotificationReceived);

            // c_map.Add(ToastWithAvatar.ScenarioId, ToastWithAvatar.NotificationReceived);
            // c_map.Add(ToastWithTextBox.ScenarioId, ToastWithTextBox.NotificationReceived);
        }

        private void My9813NotificationReceived(AppNotificationActivatedEventArgs args)
        {
            File.WriteAllLines("C:\\Temp\\Notification9813.txt", new string[] { "Notification 9813" });
        }

        ~NotificationManager()
        {
            Unregister();
        }

        public void Init()
        {
            // To ensure all Notification handling happens in this process instance, register for
            // NotificationInvoked before calling Register(). Without this a new process will
            // be launched to handle the notification.
            AppNotificationManager notificationManager = AppNotificationManager.Default;
            notificationManager.NotificationInvoked += OnNotificationInvoked;
            notificationManager.Register();
            m_isRegistered = true;
        }

        private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
        {
            File.WriteAllText("C:\\Temp\\GeneralNotification.txt", args.Argument);
        }

        public void Unregister()
        {
            if (m_isRegistered)
            {
                AppNotificationManager.Default.Unregister();
                m_isRegistered = false;
            }
        }

        public void ProcessLaunchActivationArgs(AppNotificationActivatedEventArgs notificationActivatedEventArgs)
        {
            // Complete in Step 5
        }
    }
}