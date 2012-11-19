using System.Runtime.Serialization;

namespace Battleship.Server.Shared
{
    /// <summary>
    /// This class stores information about errors during game play.
    /// </summary>
    [DataContract]
    public class GameFault
    {
        [DataMember]
        public string Message { get; set; }

        public GameFault(string message)
        {
            Message = message;
        }
    }
}