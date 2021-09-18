using Sniffer.Engine.Types.Protocols;
using System;

namespace Sniffer.Engine.Types.Args
{
    public sealed class IPV4EventArgs : EventArgs
    {
        private IPV4 protocol;
        public IPV4 Protocol => this.protocol;
        public IPV4EventArgs(IPV4 protocol)
        {
            this.protocol = protocol;
        }
    }
}