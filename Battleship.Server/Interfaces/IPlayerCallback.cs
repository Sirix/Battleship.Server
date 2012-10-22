using System.ServiceModel;
using Battleship.Server.Enums;

namespace Battleship.Server.Interfaces
{
    [ServiceContract]
    public interface IPlayerCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendStatus(PlayerStatus status);
    }
}
