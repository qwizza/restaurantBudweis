using Microsoft.AspNetCore.SignalR;
using restaurantBudweis.Model.AuthApp;
using System.Security.Claims;

namespace restaurantBudweis
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string messageText)
        {
            if (string.IsNullOrWhiteSpace(messageText)) return;

            string email = Context.User?.Identity?.Name ?? "Гость";

            string role = Context.User?.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value ?? "User";

            string avatarUrl = Context.User?.FindFirst("AvatarUrl")?.Value ?? "/avatars/default.png";

            var msg = new ChatMessage
            {
                Email = email,
                Role = role,
                AvatarUrl = avatarUrl,
                Text = messageText
            };

            await Clients.All.SendAsync("ReceiveMessage", msg);
        }
    }
}