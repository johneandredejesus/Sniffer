using Sniffer.Engine.Types;
using Sniffer.Engine.Types.Protocols;

namespace Sniffer.Engine
{
    public interface IListen
    {
        void ListenAsync(IPVersion iPVersion, ProtocolOption protocol);
        void Listen(IPVersion iPVersion, ProtocolOption protocol);
        void Stop();
    }
}