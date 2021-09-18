using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Sniffer.Engine.Types;
using Sniffer.Engine.Types.Args;
using Sniffer.Engine.Types.Protocols;

namespace Sniffer.Engine
{
    internal sealed class AnalyzerHandler : IListen
    {
        private string address;
        private int port;
        private static readonly object _lock = new object();
        private bool running;
        private bool Running { get { lock (_lock) return this.running; } set { lock (_lock) this.running = value; } }
        private CancellationTokenSource cancel;
        private Dictionary<IPVersion, AddressFamily> addressFamily;
        private Dictionary<ProtocolOption, ProtocolType> protocolType;
        private IListenEvent listenEvent;
        public AnalyzerHandler(string address, int port, IListenEvent listenEvent)
        {
            this.address = address;
            this.port = port;
            this.cancel = new CancellationTokenSource();
            this.addressFamily = new Dictionary<IPVersion, AddressFamily>();
            this.addressFamily.Add(IPVersion.IPV4, AddressFamily.InterNetwork);
            this.addressFamily.Add(IPVersion.IPV6, AddressFamily.InterNetworkV6);
            this.protocolType = new Dictionary<ProtocolOption, ProtocolType>();
            this.protocolType.Add(ProtocolOption.TCP, ProtocolType.Tcp);
            this.protocolType.Add(ProtocolOption.UDP, ProtocolType.Udp);
            this.protocolType.Add(ProtocolOption.ICMP, ProtocolType.Icmp);
            this.listenEvent = listenEvent;
        }
        private Socket CreateSocket(IPVersion iPVersion, ProtocolOption protocol)
        {
            AddressFamily family = addressFamily[iPVersion];

            ProtocolType protocol_type = protocolType[protocol];

            Socket sock = new Socket(family, SocketType.Raw, protocol_type);

            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

            sock.Bind(new IPEndPoint(IPAddress.Parse(this.address), this.port));

            return sock;
        }
        private void Handle(Socket sock, IPVersion iPVersion)
        {
            byte[] buffer = new byte[65536];

            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            EndPoint remoteEndPoint = (EndPoint)remote;

            sock.ReceiveFrom(buffer, ref remoteEndPoint);

            this.listenEvent.OnMessageReceived(this, iPVersion, buffer);
        }
        private async void HandlerAsync(IPVersion iPVersion, ProtocolOption protocol)
        {
            if (!cancel.IsCancellationRequested)
            {
                Socket sock = CreateSocket(iPVersion, protocol);
                while (Running)
                {
                    this.Handle(sock, iPVersion);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
        }
        private void Handler(IPVersion iPVersion, ProtocolOption protocol)
        {
            if (!cancel.IsCancellationRequested)
            {
                Socket sock = CreateSocket(iPVersion, protocol);
                while (Running)
                {
                    this.Handle(sock, iPVersion);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                }
            }
        }
        public async void ListenAsync(IPVersion iPVersion, ProtocolOption protocol)
        {
            Running = true;
            await Task.Run(() => this.HandlerAsync(iPVersion, protocol), this.cancel.Token);
        }
        public void Listen(IPVersion iPVersion, ProtocolOption protocol)
        {
            Running = true;
            Task.Run(() => this.Handler(iPVersion, protocol), this.cancel.Token).Wait();
        }
        public void Stop()
        {
            Running = false;
            cancel.Cancel();
        }
    }
}