using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Battleship.Server.Shared.Enums;
using Battleship.Server.Shared.Interfaces;

namespace Battleship.Server.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServerService : IServerContract
    {
        private List<Player> _players;

        public ServerService()
        {
            _players = new List<Player>();
        }

        public Guid RegisterClient(string playerName)
        {
            if (_players.Any(p => p.Name == playerName))
                throw new ArgumentException("Player with this name is already connected");

            var callback = OperationContext.Current.GetCallbackChannel<IPlayerCallback>();

            var guid = Guid.NewGuid();
            _players.Add(new Player { Name = playerName, PlayerId = guid, CallBack = callback });

            return guid;
        }

        public void StartGame(Guid playerId, bool[][] field)
        {
            if (!_players.Any(p => p.PlayerId == playerId))
                throw new ArgumentException("Player with this name is not connected");

            if(!ValidateField(field))
                throw new ArgumentException("Invalid field");

        }

        public void LeaveGame(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public ShootResult Shoot(Guid playerId, int x, int y)
        {
            throw new NotImplementedException();
        }

        private bool ValidateField(bool[][] field)
        {
            return true;
        }
    }
}
