using System.ServiceModel;

namespace Battleship.Server.Shared
{
    [ServiceContract]
    public interface IPlayerCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendStatus(PlayerStatus status);
    }
}
