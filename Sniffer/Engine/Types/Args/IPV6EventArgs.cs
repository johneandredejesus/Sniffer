using Sniffer.Engine.Types.Protocols;
using System;

namespace Sniffer.Engine.Types.Args
{
    public sealed class IPV6EventArgs : EventArgs
    {
        private IPV6 protocol;
        public IPV6 Protocol => this.protocol;
        public IPV6EventArgs(IPV6 protocol)
        {
            this.protocol = protocol;
        }
    }
}