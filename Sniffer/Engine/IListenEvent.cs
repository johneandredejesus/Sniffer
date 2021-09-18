using Sniffer.Engine.Types.Args;
using Sniffer.Engine.Types;

namespace Sniffer.Engine
{
    public interface IListenEvent
    {
        event IPV4EventHandler IPV4Event;
        event IPV6EventHandler IPV6Event;
        void OnMessageReceived(object reference, IPVersion iPVersion, byte[] buffer);
    }
}