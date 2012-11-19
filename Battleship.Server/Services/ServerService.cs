using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Battleship.Server.Shared;

namespace Battleship.Server.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ServerService : IServerContract
    {
        private List<Player> _players;

        public ServerService()
        {
            _players = new List<Player>();
        }

        public Guid RegisterClient(string playerName)
        {
            if (_players.Count == 2)
                ThrowHelper("Server is reached user limit.");

            if (_players.Any(p => p.Name == playerName))
                ThrowHelper("Player with this name is already connected.");

            var callback = OperationContext.Current.GetCallbackChannel<IPlayerCallback>();

            var guid = Guid.NewGuid();
            _players.Add(new Player {Name = playerName, PlayerId = guid, CallBack = callback});
            T("Registered player: {0}, GUID: {1}", playerName, guid);
            return guid;
        }

        public void StartGame(Guid playerId, bool[][] field)
        {
            if (!_players.Any(p => p.PlayerId == playerId))
                ThrowHelper("Player with this name is not connected.");


            var fv = new FieldValidator();
            if (!fv.Validate(field))
                ThrowHelper("Your client has sent invalid field.");

            T("Player {0} started the game", playerId);
            _players.First(p => p.PlayerId == playerId).Ships = fv.Ships;

            if (_players.Count(p => p.IsReady) == 2)
            {
                SendStatusToPlayer(_players[0], PlayerMessage.YourTurn);
                SendStatusToPlayer(_players[1], PlayerMessage.EnemyTurn);
            }
        }

        public void LeaveGame(Guid playerId)
        {
            var leavingPlayer = _players.FirstOrDefault(p => p.PlayerId == playerId);

            if (leavingPlayer == null)
                ThrowHelper("Player with this name is not connected.");

            SendStatusToPlayer(leavingPlayer, PlayerMessage.EnemyWin);

            var winner = _players.First(p => p.PlayerId != leavingPlayer.PlayerId);
            SendStatusToPlayer(winner, PlayerMessage.YouWin);

            _players.Clear();
        }

        public ShootResult Shoot(Guid playerId, int x, int y)
        {
            var player = _players.FirstOrDefault(p => p.PlayerId == playerId);

            if (player == null)
                ThrowHelper("Player with this name is not connected.");

            var secondPlayer = _players.First(p => p.PlayerId != player.PlayerId);

            var result = secondPlayer.ProcessShoot(x, y);

            InformAboutShoot(secondPlayer, x, y, result);
            if (result == ShootResult.Damaged || result == ShootResult.Destroyed)
            {
                SendStatusToPlayer(player, PlayerMessage.YourTurn);
                SendStatusToPlayer(secondPlayer, PlayerMessage.EnemyTurn);
            }
            else
            {
                SendStatusToPlayer(player, PlayerMessage.EnemyTurn);
                SendStatusToPlayer(secondPlayer, PlayerMessage.YourTurn);
            }
            return result;
        }

        private void ThrowHelper(string message)
        {
            T(message);
            var ge = new GameFault(message);
            throw new FaultException<GameFault>(ge, message);
        }

        private void SendStatusToPlayer(Player player, PlayerMessage status)
        {
            T("Sending to {0} message {1}", player.Name, status);
            //   player.CallBack.ProcessMessage(status);

            ThreadStart action = () => player.CallBack.ProcessMessage(status);
            new Thread(action).Start();
        }

        private void InformAboutShoot(Player player, int x, int y, ShootResult result)
        {
            T("Informing {0} about enemy shoot {1} {2} {3}", player.Name, x, y, result);
            // player.CallBack.InformAboutShoot(x, y, result);

            ThreadStart action = () => player.CallBack.InformAboutShoot(x, y, result);
            new Thread(action).Start();
        }

        public static void T(string format, params object[] data)
        {
            Trace.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToLongTimeString(), string.Format(format, data)));
        }
    }
}