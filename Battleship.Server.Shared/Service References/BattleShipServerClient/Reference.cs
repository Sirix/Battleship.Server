﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Battleship.Server.Shared.BattleShipServerClient {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameFault", Namespace="http://schemas.datacontract.org/2004/07/Battleship.Server.Shared")]
    [System.SerializableAttribute()]
    public partial class GameFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ShootResult", Namespace="http://schemas.datacontract.org/2004/07/Battleship.Server")]
    public enum ShootResult : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NonSpecified = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Miss = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Damaged = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Destroyed = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlayerMessage", Namespace="http://schemas.datacontract.org/2004/07/Battleship.Server")]
    public enum PlayerMessage : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        YourTurn = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        EnemyTurn = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        YouWin = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        EnemyWin = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BattleShipServerClient.IServerContract", CallbackContract=typeof(Battleship.Server.Shared.BattleShipServerClient.IServerContractCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IServerContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerContract/RegisterClient", ReplyAction="http://tempuri.org/IServerContract/RegisterClientResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Battleship.Server.Shared.BattleShipServerClient.GameFault), Action="http://tempuri.org/IServerContract/RegisterClientGameFaultFault", Name="GameFault", Namespace="http://schemas.datacontract.org/2004/07/Battleship.Server.Shared")]
        System.Guid RegisterClient(string playerName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerContract/StartGame", ReplyAction="http://tempuri.org/IServerContract/StartGameResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Battleship.Server.Shared.BattleShipServerClient.GameFault), Action="http://tempuri.org/IServerContract/StartGameGameFaultFault", Name="GameFault", Namespace="http://schemas.datacontract.org/2004/07/Battleship.Server.Shared")]
        void StartGame(System.Guid playerId, bool[][] field);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerContract/LeaveGame", ReplyAction="http://tempuri.org/IServerContract/LeaveGameResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Battleship.Server.Shared.BattleShipServerClient.GameFault), Action="http://tempuri.org/IServerContract/LeaveGameGameFaultFault", Name="GameFault", Namespace="http://schemas.datacontract.org/2004/07/Battleship.Server.Shared")]
        void LeaveGame(System.Guid playerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerContract/Shoot", ReplyAction="http://tempuri.org/IServerContract/ShootResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Battleship.Server.Shared.BattleShipServerClient.GameFault), Action="http://tempuri.org/IServerContract/ShootGameFaultFault", Name="GameFault", Namespace="http://schemas.datacontract.org/2004/07/Battleship.Server.Shared")]
        Battleship.Server.Shared.BattleShipServerClient.ShootResult Shoot(System.Guid playerId, int x, int y);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServerContractCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServerContract/ProcessMessage")]
        void ProcessMessage(Battleship.Server.Shared.BattleShipServerClient.PlayerMessage status);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServerContract/InformAboutShoot")]
        void InformAboutShoot(int x, int y, Battleship.Server.Shared.BattleShipServerClient.ShootResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServerContractChannel : Battleship.Server.Shared.BattleShipServerClient.IServerContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServerContractClient : System.ServiceModel.DuplexClientBase<Battleship.Server.Shared.BattleShipServerClient.IServerContract>, Battleship.Server.Shared.BattleShipServerClient.IServerContract {
        
        public ServerContractClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServerContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServerContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServerContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServerContractClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public System.Guid RegisterClient(string playerName) {
            return base.Channel.RegisterClient(playerName);
        }
        
        public void StartGame(System.Guid playerId, bool[][] field) {
            base.Channel.StartGame(playerId, field);
        }
        
        public void LeaveGame(System.Guid playerId) {
            base.Channel.LeaveGame(playerId);
        }
        
        public Battleship.Server.Shared.BattleShipServerClient.ShootResult Shoot(System.Guid playerId, int x, int y) {
            return base.Channel.Shoot(playerId, x, y);
        }
    }
}
