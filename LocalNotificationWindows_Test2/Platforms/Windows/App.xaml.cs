using Microsoft.UI.Xaml;
using Microsoft.Windows.AppNotifications;


using Microsoft.Windows.AppLifecycle;




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
        private static string pendingNotificationArguments;

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

        //protected override void OnLaunched(LaunchActivatedEventArgs args)
        //{
        //    notificationManager.Init();

        //    //AppLaunchArguments.LaunchArguments = args.Arguments;

        //    //if (string.IsNullOrEmpty(AppLaunchArguments.LaunchArguments))
        //    //{
        //    //    string[] arguments = Environment.GetCommandLineArgs();
        //    //    AppLaunchArguments.LaunchArgumentsGetCommandLineArgs = string.Join(" ", arguments);
        //    //}

        //    base.OnLaunched(args);
        //}

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            notificationManager.Init();

            var currentInstance = AppInstance.GetCurrent();
            if (currentInstance != null)
            {
                var activationArgs = currentInstance.GetActivatedEventArgs();
                if (activationArgs.Kind == ExtendedActivationKind.AppNotification)
                {
                    var notificationActivatedEventArgs = activationArgs.Data as AppNotificationActivatedEventArgs;
                    if (notificationActivatedEventArgs != null)
                    {
                        notificationManager.ProcessLaunchActivationArgs(notificationActivatedEventArgs);
                    }
                }
            }

            base.OnLaunched(args);
        }



        //protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        //{
        //    notificationManager.Init();

        //    // Registriere den NotificationInvoked Handler sofort
        //    AppNotificationManager.Default.NotificationInvoked += (sender, notificationArgs) =>
        //    {
        //        // Speichere die Argumente für spätere Verarbeitung
        //        pendingNotificationArguments = notificationArgs.Argument;
        //        Console.WriteLine($"Notification-Argumente empfangen: {pendingNotificationArguments}");

        //        // Verarbeite die Argumente direkt
        //        if (!string.IsNullOrEmpty(pendingNotificationArguments))
        //        {
        //            notificationManager.ProcessLaunchActivationArgs(pendingNotificationArguments);
        //        }
        //    };

        //    // Wenn wir COM-aktiviert wurden und gespeicherte Argumente haben
        //    if (args.Arguments == "-Embedding" && !string.IsNullOrEmpty(pendingNotificationArguments))
        //    {
        //        Console.WriteLine($"Verarbeite pending Notification-Argumente: {pendingNotificationArguments}");
        //        notificationManager.ProcessLaunchActivationArgs(pendingNotificationArguments);
        //    }

        //    base.OnLaunched(args);
        //}


        private void OnProcessExit(object? sender, EventArgs e)
        {
            notificationManager.Unregister();
        }
    }




    //internal class NotificationManager
    //{
    //    private bool m_isRegistered;

    //    private Dictionary<int, Action<AppNotificationActivatedEventArgs>> c_map;

    //    public NotificationManager()
    //    {
    //        m_isRegistered = false;

    //        // When adding new a scenario, be sure to add its notification handler here.
    //        c_map = new Dictionary<int, Action<AppNotificationActivatedEventArgs>>();
    //        c_map.Add(9813, NotificationReceived);

    //        // c_map.Add(ToastWithAvatar.ScenarioId, ToastWithAvatar.NotificationReceived);
    //        // c_map.Add(ToastWithTextBox.ScenarioId, ToastWithTextBox.NotificationReceived);
    //    }

    //    //private void My9813NotificationReceived(AppNotificationActivatedEventArgs args)
    //    //{
    //    //    //File.WriteAllLines("C:\\Temp\\Notification9813.txt", new string[] { "Notification 9813" });
    //    //    //AppLaunchArguments.LaunchArgumentsGetCommandLineArgs = string.Join(" ", arguments);
    //    //}
    //    public static void NotificationReceived(AppNotificationActivatedEventArgs notificationActivatedEventArgs)
    //    {
    //        // Verarbeite die Benachrichtigungsargumente (String)
    //        string arguments = notificationActivatedEventArgs.Argument;

    //        // Zerlege die Argumente (z. B. "action=ToastClick&scenarioTag=9813")
    //        var keyValuePairs = arguments.Split('&')
    //            .Select(part => part.Split('='))
    //            .Where(parts => parts.Length == 2)
    //            .ToDictionary(parts => parts[0], parts => parts[1]);

    //        if (keyValuePairs.TryGetValue("action", out string action))
    //        {
    //            if (action == "ToastClick")
    //            {
    //                Console.WriteLine("Toast mit Avatar wurde geklickt!");
    //            }
    //            else if (action == "OpenApp")
    //            {
    //                Console.WriteLine("Toast hat die App geöffnet!");
    //            }
    //        }
    //    }

    //    ~NotificationManager()
    //    {
    //        Unregister();
    //    }

    //    //public void Init()
    //    //{
    //    //    // To ensure all Notification handling happens in this process instance, register for
    //    //    // NotificationInvoked before calling Register(). Without this a new process will
    //    //    // be launched to handle the notification.
    //    //    AppNotificationManager notificationManager = AppNotificationManager.Default;
    //    //    notificationManager.NotificationInvoked += OnNotificationInvoked;
    //    //    notificationManager.Register();
    //    //    m_isRegistered = true;
    //    //}
    //    public void Init()
    //    {
    //        try
    //        {
    //            AppNotificationManager notificationManager = AppNotificationManager.Default;
    //            notificationManager.NotificationInvoked += OnNotificationInvoked;
    //            // Registriere für Notifications
    //            notificationManager.Register();

    //            Console.WriteLine("NotificationManager erfolgreich initialisiert");
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Fehler bei NotificationManager Init: {ex.Message}");
    //        }
    //    }

    //    private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    //    {
    //        //File.WriteAllText("C:\\Temp\\GeneralNotification.txt", args.Argument);
    //        AppLaunchArguments.LaunchArgumentsOnNotificationInvoked = string.Join(" ", " OnNotificationInvoked -> " + args.Argument);
    //    }

    //    public void Unregister()
    //    {
    //        if (m_isRegistered)
    //        {
    //            AppNotificationManager.Default.Unregister();
    //            m_isRegistered = false;
    //        }
    //    }

    //    //public void ProcessLaunchActivationArgs(AppNotificationActivatedEventArgs notificationActivatedEventArgs)
    //    //{
    //    //    // Complete in Step 5
    //    //}
    //    //public void ProcessLaunchActivationArgs(string arguments)
    //    //{
    //    //    try
    //    //    {
    //    //        Console.WriteLine($"Verarbeite Notification-Argumente: {arguments}");

    //    //        var keyValuePairs = arguments.Split('&')
    //    //            .Select(part => part.Split('='))
    //    //            .Where(parts => parts.Length == 2)
    //    //            .ToDictionary(parts => parts[0], parts => parts[1]);

    //    //        // Prüfe auf conversationId
    //    //        if (keyValuePairs.TryGetValue("conversationId", out string conversationId) &&
    //    //            conversationId == "9813")
    //    //        {
    //    //            // Hier die Notification-Logik implementieren
    //    //            Console.WriteLine($"Notification für conversationId {conversationId} verarbeitet");
    //    //            // Optional: Event auslösen oder weitere Verarbeitung
    //    //        }
    //    //        else
    //    //        {
    //    //            Console.WriteLine($"Unbekannte conversationId: {conversationId}");
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        Console.WriteLine($"Fehler bei der Verarbeitung der Notification: {ex}");
    //    //    }
    //    //}
    //    //public void ProcessLaunchActivationArgs(string arguments)
    //    //{
    //    //    try
    //    //    {
    //    //        Console.WriteLine($"Verarbeite Argumente: {arguments}");

    //    //        // Hier deine Logik für die Verarbeitung der Argumente
    //    //        // z.B. Navigation zu einer bestimmten Seite basierend auf conversationId

    //    //        if (arguments.Contains("conversationId=9813"))
    //    //        {
    //    //            // Führe spezifische Aktion aus
    //    //            Console.WriteLine("Conversation 9813 wurde aktiviert");
    //    //            AppLaunchArguments.LaunchArgumentsOnNotificationInvoked = string.Join(" ", " ProcessLaunchActivationArgs -> " + arguments);
    //    //        }
    //    //        else
    //    //            AppLaunchArguments.LaunchArgumentsOnNotificationInvoked = string.Join(" ", " ProcessLaunchActivationArgs LEER -> " + arguments);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        Console.WriteLine($"Fehler bei der Verarbeitung: {ex.Message}");
    //    //    }
    //    //}
    //    // private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    //    public void ProcessLaunchActivationArgs(AppNotificationActivatedEventArgs args)
    //    {
    //        AppLaunchArguments.LaunchArgumentsOnNotificationInvoked = string.Join(" ", " OnNotificationInvoked -> " + args.Argument);
    //        // If the user clicks on the notification body, your app needs to launch the chat thread window
    //        //if (notificationActivatedEventArgs.Argument.Contains("openThread"))
    //        //{
    //        //    GenerateChatThreadWindow();
    //        //}
    //        //else // If the user responds to a message by clicking a button in the notification, your app needs to reply back to the other user with no window launched
    //        //if (notificationActivatedEventArgs.Argument.Contains("reply"))
    //        //{
    //        //    var input = notificationActivatedEventArgs.UserInput;
    //        //    var replyBoxText = input["replyBox"];

    //        //    // Process the reply text
    //        //    SendReplyToUser(replyBoxText);
    //        //}
    //    }

    //    //private void GenerateChatThreadWindow()
    //    //{
    //    //    // Implementation to generate the chat thread window
    //    //    Console.WriteLine("Generating chat thread window...");
    //    //}

    //    //private void SendReplyToUser(string replyBoxText)
    //    //{
    //    //    // Implementation to send the reply to the user
    //    //    Console.WriteLine($"Sending reply to user: {replyBoxText}");
    //    //}

    //}



    internal class NotificationManager
    {
        private bool m_isRegistered;
        private Dictionary<int, Action<AppNotificationActivatedEventArgs>> c_map;

        public NotificationManager()
        {
            m_isRegistered = false;

            // When adding new a scenario, be sure to add its notification handler here.
            c_map = new Dictionary<int, Action<AppNotificationActivatedEventArgs>>();
            c_map.Add(9813, NotificationReceived);
        }

        public static void NotificationReceived(AppNotificationActivatedEventArgs notificationActivatedEventArgs)
        {
            // Verarbeite die Benachrichtigungsargumente (String)
            string arguments = notificationActivatedEventArgs.Argument;

            // Zerlege die Argumente (z. B. "action=ToastClick&scenarioTag=9813")
            var keyValuePairs = arguments.Split('&')
                .Select(part => part.Split('='))
                .Where(parts => parts.Length == 2)
                .ToDictionary(parts => parts[0], parts => parts[1]);

            if (keyValuePairs.TryGetValue("action", out string action))
            {
                if (action == "ToastClick")
                {
                    Console.WriteLine("Toast mit Avatar wurde geklickt!");
                }
                else if (action == "OpenApp")
                {
                    Console.WriteLine("Toast hat die App geöffnet!");
                }
            }
        }

        ~NotificationManager()
        {
            Unregister();
        }

        public void Init()
        {
            try
            {
                AppNotificationManager notificationManager = AppNotificationManager.Default;
                notificationManager.NotificationInvoked += OnNotificationInvoked;
                // Registriere für Notifications
                notificationManager.Register();

                Console.WriteLine("NotificationManager erfolgreich initialisiert");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler bei NotificationManager Init: {ex.Message}");
            }
        }

        private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
        {
            // Speichere die Argumente für spätere Verarbeitung
            AppLaunchArguments.LaunchArgumentsOnNotificationInvoked = string.Join(" ", " OnNotificationInvoked -> " + args.Argument);

            // Rufe die Methode zur Verarbeitung der Benachrichtigungsargumente auf
            ProcessLaunchActivationArgs(args);
        }

        public void Unregister()
        {
            if (m_isRegistered)
            {
                AppNotificationManager.Default.Unregister();
                m_isRegistered = false;
            }
        }

        //public void ProcessLaunchActivationArgs(AppNotificationActivatedEventArgs args)
        //{
        //    AppLaunchArguments.LaunchArgumentsOnNotificationInvoked = string.Join(" ", " OnNotificationInvoked -> " + args.Argument);

        //    // If the user clicks on the notification body, your app needs to launch the chat thread window
        //    if (args.Argument.Contains("openThread"))
        //    {
        //        GenerateChatThreadWindow();
        //    }
        //    else // If the user responds to a message by clicking a button in the notification, your app needs to reply back to the other user with no window launched
        //    if (args.Argument.Contains("reply"))
        //    {
        //        var input = args.UserInput;
        //        var replyBoxText = input["replyBox"];

        //        // Process the reply text
        //        SendReplyToUser(replyBoxText);
        //    }
        //}

        //private void GenerateChatThreadWindow()
        //{
        //    // Implementation to generate the chat thread window
        //    Console.WriteLine("Generating chat thread window...");
        //}

        //private void SendReplyToUser(string replyBoxText)
        //{
        //    // Implementation to send the reply to the user
        //    Console.WriteLine($"Sending reply to user: {replyBoxText}");
        //}

        public void ProcessLaunchActivationArgs(AppNotificationActivatedEventArgs args)
        {
            //DispatchNotification(args);
            AppLaunchArguments.LaunchArgumentsOnNotificationInvoked = string.Join(" ", " ProcessLaunchActivationArgs -> " + args.Argument);
        }

        //public bool DispatchNotification(AppNotificationActivatedEventArgs notificationActivatedEventArgs)
        //{
        //    var scenarioId = notificationActivatedEventArgs.Arguments. .Lookup(Common.scenarioTag);
        //    if (!string.IsNullOrEmpty(scenarioId))
        //    {
        //        try
        //        {
        //            int scenarioIdInt = int.Parse(scenarioId);
        //            if (c_notificationHandlers.ContainsKey(scenarioIdInt))
        //            {
        //                c_notificationHandlers[scenarioIdInt](notificationActivatedEventArgs);
        //                return true;
        //            }
        //            else
        //            {
        //                return false; // Couldn't find a NotificationHandler for scenarioId.
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return false; // Couldn't find a NotificationHandler for scenarioId.
        //        }
        //    }
        //    else
        //    {
        //        return false; // No scenario specified in the notification
        //    }
        //}


    }

}