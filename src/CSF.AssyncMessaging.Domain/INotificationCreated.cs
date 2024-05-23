namespace CSF.AssyncMessaging.Domain
{
    public enum NotificationType
    {
        Email,
        Push,
        Sms
    }

    public class INotificationCreated
    {
        DateTime NotificationDate { get; }
        string NotificationMessage { get; }
        NotificationType NotificationType { get; }
    }
}