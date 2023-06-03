namespace Shop.Abstraction.Services
{
    public interface IEmailService
    {
        void Send(string mailto, string subject, string body, bool isBodyHtml = false);

    }
}
