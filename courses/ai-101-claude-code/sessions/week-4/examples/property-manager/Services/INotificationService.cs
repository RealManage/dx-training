namespace RealManage.PropertyManager.Services;

public interface INotificationService
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
    Task<bool> SendSmsAsync(string phoneNumber, string message);
    Task<bool> SendPushNotificationAsync(string userId, string title, string message);
}

// TODO: This should be mocked in tests, never implemented!