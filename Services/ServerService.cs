using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Battleship.Server.Enums;
using Battleship.Server.Interfaces;

namespace Battleship.Server.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServerService : IServerContract
    {
        private Dictionary<string, Guid> _connectedPlayers;

        public ServerService()
        {
            _connectedPlayers = new Dictionary<string, Guid>();
        }

        public Guid RegisterClient(string playerName)
        {
            if (_connectedPlayers.ContainsKey(playerName))
                throw new ArgumentException("Player with this name is already connected");

            var guid = Guid.NewGuid();
            _connectedPlayers.Add(playerName, guid);

            return guid;
        }

        public void StartGame(Guid playerId, bool[][] field)
        {
            //TODO: Just test code
            var _callback = OperationContext.Current.GetCallbackChannel<IPlayerCallback>();
            _callback.SendStatus(PlayerStatus.YourTurn);
        }

        public void LeaveGame(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public Enums.ShootResult Shoot(Guid playerId, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
