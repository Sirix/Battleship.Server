using System.ServiceModel;

namespace Battleship.Server
{
    public interface IPlayerCallback
    {
        [OperationContract(IsOneWay = true)]
        void ProcessMessage(PlayerMessage status);

        [OperationContract(IsOneWay = true)]
        void InformAboutShoot(int x, int y, ShootResult result);
    }
}