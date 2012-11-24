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
        private bool _inGame;

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
                _inGame = true;
                _players[0].HisTurn = true;
                SendStatusToPlayer(_players[0], PlayerMessage.YourTurn);

                _players[1].HisTurn = false;
                SendStatusToPlayer(_players[1], PlayerMessage.EnemyTurn);
            }
        }

        public void LeaveGame(Guid playerId)
        {
            if (!_inGame) return;

            var leavingPlayer = _players.FirstOrDefault(p => p.PlayerId == playerId);

            if (leavingPlayer == null)
                return;

            T("Player {0} leaves..", leavingPlayer.Name);

            SendStatusToPlayer(leavingPlayer, PlayerMessage.EnemyWin);

            var winner = _players.First(p => p.PlayerId != leavingPlayer.PlayerId);
            SendStatusToPlayer(winner, PlayerMessage.YouWin);

            _players.Clear();
            _inGame = false;
        }

        public ShootResult Shoot(Guid playerId, int x, int y)
        {
            if (!_inGame) return ShootResult.NonSpecified;

            var player = _players.FirstOrDefault(p => p.PlayerId == playerId);

            if (player == null)
                ThrowHelper("Player with this name is not connected.");

            if (x < 0 || y < 0 || x > GameConfiguration.FieldSize - 1 || y > GameConfiguration.FieldSize - 1)
            {
                ThrowHelper(string.Format("-----------Nuclear launch detected by {0}", player.Name));
            }
            if(!player.HisTurn)
            {
                T("Player {0} tries to shoot in not his turn..Waiting", player.Name);
                while (!player.HisTurn)
                    Thread.Sleep(100);
            }

            var secondPlayer = _players.First(p => p.PlayerId != player.PlayerId);

            var result = secondPlayer.ProcessShoot(x, y);

            InformAboutShoot(secondPlayer, x, y, result);
            if (secondPlayer.IsAlive)
            {
                if (result == ShootResult.Damaged || result == ShootResult.Destroyed)
                {
                    player.HisTurn = true;
                    SendStatusToPlayer(player, PlayerMessage.YourTurn);

                    secondPlayer.HisTurn = false;
                    SendStatusToPlayer(secondPlayer, PlayerMessage.EnemyTurn);
                }
                else
                {
                    player.HisTurn = false;
                    SendStatusToPlayer(player, PlayerMessage.EnemyTurn);

                    secondPlayer.HisTurn = true;
                    SendStatusToPlayer(secondPlayer, PlayerMessage.YourTurn);
                }
            }
            else
            {
                player.HisTurn = secondPlayer.HisTurn = false;

                SendStatusToPlayer(player, PlayerMessage.YouWin);
                SendStatusToPlayer(secondPlayer, PlayerMessage.EnemyWin);

                _inGame = false;
                _players.Clear();
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

            ThreadStart action = () =>
                                     {
                                         Thread.Sleep(250);
                                         player.CallBack.ProcessMessage(status);
                                     };
            new Thread(action).Start();
        }

        private void InformAboutShoot(Player player, int x, int y, ShootResult result)
        {
            T("Informing {0} about enemy shoot {1} {2} {3}", player.Name, x, y, result);

            ThreadStart action = () =>
                                     {
                                         Thread.Sleep(250);
                                         player.CallBack.InformAboutShoot(x, y, result);
                                     };
            new Thread(action).Start();
        }

        public static void T(string format, params object[] data)
        {
            Trace.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToLongTimeString(), string.Format(format, data)));
        }
    }
}