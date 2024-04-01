using RealTime.Chat.App.API.Models;
using System.Collections.Concurrent;

namespace RealTime.Chat.App.API.DataService
{
    public class SharedDb
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connection = new();

        public ConcurrentDictionary<string, UserConnection> connection => _connection;
    }
}
