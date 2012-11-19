using System;
using System.ServiceModel;
using Battleship.Server.Shared;

namespace Battleship.Server
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof (IPlayerCallback))]
    public interface IServerContract
    {
        [OperationContract]
        [FaultContract(typeof(GameFault))]
        Guid RegisterClient(string playerName);

        [OperationContract]
        [FaultContract(typeof(GameFault))]
        void StartGame(Guid playerId, bool[][] field);

        [OperationContract]
        [FaultContract(typeof(GameFault))]
        void LeaveGame(Guid playerId);

        [OperationContract]
        [FaultContract(typeof(GameFault))]
        ShootResult Shoot(Guid playerId, int x, int y);
    }
}