using System;
using System.ServiceModel;
using Battleship.Server.Enums;

namespace Battleship.Server.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof (IPlayerCallback))]
    public interface IServerContract
    {
        [OperationContract]
        Guid RegisterClient(string playerName);

        [OperationContract]
        void StartGame(Guid playerId, bool[][] field);

        [OperationContract(IsOneWay = true)]
        void LeaveGame(Guid playerId);

        [OperationContract]
        ShootResult Shoot(Guid playerId, int x, int y);
    }
}