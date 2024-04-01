using Microsoft.AspNetCore.SignalR;
using RealTime.Chat.App.API.DataService;
using RealTime.Chat.App.API.Models;

namespace RealTime.Chat.App.API.Hubs
{
    public class ChatHub : Hub
    {
        public readonly SharedDb _sharedDb;

        public ChatHub(SharedDb sharedDb) => _sharedDb = sharedDb;

        public async Task JoinChat(UserConnection connection)
        {
            await Clients.All.SendAsync("ReceivedMessage", "adimn", $"{connection.Username} has joined");
        }

        public async Task JoinSpecificChatRoom(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

            _sharedDb.connection[Context.ConnectionId] = connection;

            await Clients.Group(connection.ChatRoom).SendAsync("ReceivedMessage", "adimn", $"{connection.Username} has joined");
        }

        public async Task SendMessage(string message)
        {
            if(_sharedDb.connection.TryGetValue(Context.ConnectionId, out UserConnection connection))
            {
                await Clients.Group(connection.ChatRoom).SendAsync("ReceiveSpecificMessage", connection.Username, message);
            }
        }
    }
}
