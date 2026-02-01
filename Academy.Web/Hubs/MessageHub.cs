using Microsoft.AspNetCore.SignalR;


namespace Academy.Web.Hubs
{
    public class MessageHub : Hub
    {
        public async Task RegisterEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return;

            email = email.Trim().ToLowerInvariant();

            await Groups.AddToGroupAsync(Context.ConnectionId, email);

            // اختياري: تأكيد للعميل
            await Clients.Caller.SendAsync("Registered", $"Registered to {email}");
        }
    }
}
