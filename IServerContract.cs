using System;
using System.ServiceModel;

namespace Battleship.Server
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
        ShootResult Shoot(Guid playerId, Cell cell);
    }

    public interface IPlayerCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendStatus(PlayerStatus status);
    }

    public enum PlayerStatus
    {
        None = 0,
        YourTurn = 1,
        EnemyTurn = 2,
        YouWin = 3,
        EnemyWin = 4
    }

    public enum ShootResult
    {
        NonSpecified = 0,
        Miss = 1,
        Damaged = 2,
        Destroyed = 3
    }
}