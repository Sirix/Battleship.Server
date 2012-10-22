using System.ServiceModel;
using Battleship.Server.Shared.Enums;

namespace Battleship.Server.Shared.Interfaces
{
    [ServiceContract]
    public interface IPlayerCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendStatus(PlayerStatus status);
    }
}
