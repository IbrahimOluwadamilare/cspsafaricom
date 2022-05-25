using System;
namespace cspv3.Services
{
    public interface IGmailSender
    {
        string SendLinkEmailAsync(string emailAdd, string subject, string message);
        string SendPlainEmailAsync(string emailAdd, string subject, string message);
    }
}
