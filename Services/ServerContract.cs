using System;
using Battleship.Server.Enums;
using Battleship.Server.Interfaces;

namespace Battleship.Server.Services
{
    public class ServerContract : IServerContract
    {
        public Guid RegisterClient(string playerName)
        {
            throw new NotImplementedException();
        }

        public void StartGame(Guid playerId, bool[][] field)
        {
            throw new NotImplementedException();
        }

        public void LeaveGame(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public ShootResult Shoot(Guid playerId, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
