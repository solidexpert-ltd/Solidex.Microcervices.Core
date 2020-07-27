namespace Microcervices.Core.Infrasructure.SignalR
{
    public class BroadcastMessage
    {
        /// <summary>
        /// Type of Message
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Type of Data
        /// </summary>
        public object Data { get; set; }
    }
}