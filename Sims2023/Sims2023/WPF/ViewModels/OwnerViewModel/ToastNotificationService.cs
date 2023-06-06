using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using System;
using System.Windows;

public static class ToastNotificationService
{
    private static Notifier notifier;

    public static void Initialize(Window window)
    {
        notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: window,
                corner: Corner.TopRight,
                offsetX: 80,
                offsetY: 650);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(2),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }

    public static void ShowInformation(string message)
    {
        notifier?.ShowInformation(message);
    }
}
