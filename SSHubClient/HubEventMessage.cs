using System;

namespace HubClient
{
    public class HubMessageEventArgs : EventArgs
    {
        public string Sender { get; set; }
        public string Message { get; set; }

        public HubMessageEventArgs(string sender, string message)
        {
            this.Sender = sender;
            this.Message = message;
        }
    }

}
