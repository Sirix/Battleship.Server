using System;
using System.ServiceModel;
using Battleship.Server.Shared.BattleShipServerClient;

namespace Battleship.Server.Shared
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ClientCallback : IServerContractCallback
    {
        /// <summary>
        /// Raised to inform player about action from sever or enemy player 
        /// </summary>
        public event Action<PlayerMessage> OnServerMessage;
        /// <summary>
        /// Invokes when enemy palyer has shot on field of this player.
        /// </summary>
        public event Action<int, int, ShootResult> OnEnemyShoot;

        public void ProcessMessage(PlayerMessage message)
        {
            if (OnServerMessage != null)
                OnServerMessage(message);
        }

        public void InformAboutShoot(int x, int y, ShootResult result)
        {
            if (OnEnemyShoot != null)
                OnEnemyShoot(x, y, result);
        }
    }
}