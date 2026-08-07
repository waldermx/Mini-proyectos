
using System; // <-- Necesario para [STAThread]
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace MediatorWpfScript
{
    // -------------------------------------------------------------------
    // 1. Interfaz del Mediador
    // -------------------------------------------------------------------
    public interface IMediator
    {
        void Notify(object sender, string ev, string message);
    }

    // -------------------------------------------------------------------
    // 2. Clase Base para los Colegas (Componentes)
    // -------------------------------------------------------------------
    public abstract class BaseComponent
    {
        protected IMediator? _mediator;

        public void SetMediator(IMediator mediator) => _mediator = mediator;
    }

    // -------------------------------------------------------------------
    // 3. Componentes Concretos
    // -------------------------------------------------------------------

    // UI Principal (Ventana WPF)
    public class MainWindowComponent : Window
    {
        private IMediator? _mediator;

        public MainWindowComponent()
        {
            Title = "Patrón Mediador - WPF Script";
            Width = 400;
            Height = 300;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var panel = new StackPanel { Margin = new Thickness(20) };
            var txtInput = new TextBox { Text = "Mensaje de prueba...", Margin = new Thickness(0, 0, 0, 15), Padding = new Thickness(5) };

            var btnConsole = new Button { Content = "Enviar a Consola", Margin = new Thickness(0, 0, 0, 5), Padding = new Thickness(5) };
            var btnToast = new Button { Content = "Enviar Toast (In-App Popup)", Margin = new Thickness(0, 0, 0, 5), Padding = new Thickness(5) };
            var btnWin = new Button { Content = "Enviar Notificación de Windows", Margin = new Thickness(0, 0, 0, 5), Padding = new Thickness(5) };
            var btnAll = new Button { Content = "Enviar a Todos", Padding = new Thickness(5) };

            btnConsole.Click += (s, e) => _mediator?.Notify(this, "BTN_CONSOLE", txtInput.Text);
            btnToast.Click += (s, e) => _mediator?.Notify(this, "BTN_TOAST", txtInput.Text);
            btnWin.Click += (s, e) => _mediator?.Notify(this, "BTN_WIN", txtInput.Text);
            btnAll.Click += (s, e) => _mediator?.Notify(this, "BTN_ALL", txtInput.Text);

            panel.Children.Add(new TextBlock { Text = "Escribe un mensaje:", Margin = new Thickness(0, 0, 0, 5) });
            panel.Children.Add(txtInput);
            panel.Children.Add(btnConsole);
            panel.Children.Add(btnToast);
            panel.Children.Add(btnWin);
            panel.Children.Add(btnAll);

            Content = panel;
        }

        public void SetMediator(IMediator mediator) => _mediator = mediator;
    }

    // Notificador por Consola
    public class ConsoleNotificationComponent : BaseComponent
    {
        public void SendConsoleMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[Consola WPF]: {message}");
            Console.ResetColor();
        }
    }

    // Notificador Toast (Pop-up gráfico dentro de la app)
    public class ToastNotificationComponent : BaseComponent
    {
        public void ShowToast(string message)
        {
            var toast = new Window
            {
                Width = 280,
                Height = 60,
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Background = new SolidColorBrush(Color.FromArgb(230, 40, 40, 40)),
                Topmost = true,
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            var txt = new TextBlock
            {
                Text = $"🥪 Toast: {message}",
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };

            toast.Content = txt;
            toast.Show();

            // Auto-cerrar el Toast tras 2 segundos
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += (s, e) => { toast.Close(); timer.Stop(); };
            timer.Start();
        }
    }

    // Notificador de Windows (Mensaje de Sistema Nativo)
    public class WindowsNotificationComponent : BaseComponent
    {
        public void ShowWindowsNotification(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    // -------------------------------------------------------------------
    // 4. Mediador Concreto
    // -------------------------------------------------------------------
    public class AppMediator : IMediator
    {
        private readonly MainWindowComponent _window;
        private readonly ConsoleNotificationComponent _console;
        private readonly ToastNotificationComponent _toast;
        private readonly WindowsNotificationComponent _windows;

        public AppMediator(
            MainWindowComponent window,
            ConsoleNotificationComponent console,
            ToastNotificationComponent toast,
            WindowsNotificationComponent windows)
        {
            _window = window;
            _window.SetMediator(this);

            _console = console;
            _console.SetMediator(this);

            _toast = toast;
            _toast.SetMediator(this);

            _windows = windows;
            _windows.SetMediator(this);
        }

        public void Notify(object sender, string ev, string message)
        {
            switch (ev)
            {
                case "BTN_CONSOLE":
                    _console.SendConsoleMessage(message);
                    break;

                case "BTN_TOAST":
                    _toast.ShowToast(message);
                    break;

                case "BTN_WIN":
                    _windows.ShowWindowsNotification("Aviso de Sistema", message);
                    break;

                case "BTN_ALL":
                    _console.SendConsoleMessage(message);
                    _toast.ShowToast(message);
                    _windows.ShowWindowsNotification("Alerta Global", message);
                    break;
            }
        }
    }

    // -------------------------------------------------------------------
    // 5. Punto de Entrada
    // -------------------------------------------------------------------
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();

            var window = new MainWindowComponent();
            var console = new ConsoleNotificationComponent();
            var toast = new ToastNotificationComponent();
            var windows = new WindowsNotificationComponent();

            _ = new AppMediator(window, console, toast, windows);

            app.Run(window);
        }
    }
}