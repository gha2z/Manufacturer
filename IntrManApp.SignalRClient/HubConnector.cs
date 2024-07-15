using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace IntrManApp.SignalRClient
{
    public class HubConnector
    {
        public HubConnection connection;
        public string sender { get; set; }
        public string message { get; set; }
        public bool isConnected() => connection?.State == HubConnectionState.Connected;

        public event EventHandler<HubMessageEventArgs> ReceiveMessage;
        public event EventHandler<EventArgs> StartingConnection;
        public event EventHandler<EventArgs> Reconnecting;
        public event EventHandler<EventArgs> Reconnected;
        public event EventHandler<EventArgs> Closed;
        public event EventHandler<string> EventMessage;

        public HubConnector()
        {

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:50000/chathub")
                .WithAutomaticReconnect()
                .Build();

            connection.On<string, string>("ReceiveMessage", (sender, message) =>
            {
                this.sender = sender;
                this.message = message;
                OnReceiveMessage(new HubMessageEventArgs(sender, message));
            });

            connection.Reconnected += (sender) =>
            {
                onReconnected(new EventArgs());
                return Task.CompletedTask;
            };

            connection.Reconnecting += (sender) =>
            {
                onReconnecting(new EventArgs());
                return Task.CompletedTask;
            };

            connection.Closed += (sender) =>
            {
                onClosed(new EventArgs());
                return Task.CompletedTask;
            };
        }

        public async Task Start()
        {
            OnStartingConnection(new EventArgs());
            try
            {
                await connection.StartAsync();
                OnEventMessage($"Connection started .. Is connected: {isConnected()}");

            }
            catch (Exception ex)
            {
                OnEventMessage(ex.Message + Environment.NewLine + "Technical error:" + Environment.NewLine + ex.ToString());
            }
        }

        public async Task SendMessage(string sender, string message) =>
            await connection.InvokeAsync("SendMessage", sender, message);

        public async Task SendMessage(string sender, string message, string jsonString) =>
           await connection.InvokeAsync("SendMessage", sender, message, jsonString);

        protected virtual void onClosed(EventArgs e) =>
            Closed?.Invoke(this, e);
        protected virtual void onReconnected(EventArgs e) =>
            Reconnected?.Invoke(this, e);
        protected virtual void onReconnecting(EventArgs e) =>
            Reconnecting?.Invoke(this, e);
        protected virtual void OnReceiveMessage(HubMessageEventArgs e)
        {
            try
            {
                ReceiveMessage?.Invoke(this,e);
            }
            catch(Exception ex) {
                OnEventMessage(ex.Message + Environment.NewLine + "Technical error:" + Environment.NewLine + ex.ToString());
            }
        }
        protected virtual void OnStartingConnection(EventArgs e) =>
            StartingConnection?.Invoke(this, e);

        protected virtual void OnEventMessage(string e) =>
            EventMessage?.Invoke(this, e);
    }
}