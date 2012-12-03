How to connect to and interact with Battleship server in your app:

1) Add reference to `Battleship.Server.Shared.dll`
2) Create instance of class `ClientCallback` and subscribe to events
```	var callback = new ClientCallback();
    callback.OnEnemyShoot += callback_OnEnemyShoot;
    callback.OnServerMessage += callback_OnServerMessage;```
3) Create server client
``` var client = new ServerContractClient(new InstanceContext(callback));```
4) Register your app at server
``` Guid clientId = client.RegisterClient("USERNAME");```
5) Start your game, send youy field to server (true means ship exists in cell, false - not)
``` client.StartGame(clientId, field);```
6) In callback_OnServerMessage, when you get PlayerMessage.YourTurn shoot
``` ShootResult res = client.Shoot(clientId, pos.X, pos.Y);```
