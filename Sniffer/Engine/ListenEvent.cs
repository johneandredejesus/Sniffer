using Sniffer.Engine.Types.Args;
using Sniffer.Engine.Types;
using Sniffer.Engine.Types.Protocols;

namespace Sniffer.Engine
{
    public delegate void IPV4EventHandler(object sender, IPV4EventArgs args);
    public delegate void IPV6EventHandler(object sender, IPV6EventArgs args);

    internal sealed class ListenEvent : IListenEvent
    {
        public event IPV4EventHandler IPV4Event;
        public event IPV6EventHandler IPV6Event;
        public void OnMessageReceived(object reference, IPVersion iPVersion, byte[] buffer)
        {
            byte[] dataReturn = new byte[65536];

            switch (iPVersion)
            {
                case IPVersion.IPV4:
                    IPV4 ipv4 = new IPV4(buffer, ref dataReturn);
                    this.IPV4Event.Invoke(reference, new IPV4EventArgs(ipv4));
                    switch (ipv4.Protocol)
                    {
                        case 1:
                            break;
                    }
                    break;
                case IPVersion.IPV6:
                    IPV6 ipv6 = new IPV6(buffer, ref dataReturn);
                    this.IPV6Event.Invoke(reference, new IPV6EventArgs(ipv6));
                    break;
            }
        }
    }
}