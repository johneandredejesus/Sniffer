using Sniffer.Engine.Types;
using Sniffer.Engine.Types.Protocols;

namespace Sniffer.Engine
{
  public sealed class Analyzer : IAnalyzerHandler
    {
        private readonly IListenEvent listenEvent;
        public IListenEvent ListenEvent => listenEvent;
        private IListen analyzerHandler; 
        public Analyzer(string address, int port)
        {
            listenEvent = new ListenEvent();
            this.analyzerHandler = new AnalyzerHandler(address, port, listenEvent);
        }
        public void ListenAsync(IPVersion iPVersion, ProtocolOption protocol)
        {
            this.analyzerHandler.ListenAsync(iPVersion, protocol);
        }
        public void Listen(IPVersion iPVersion, ProtocolOption protocol)
        {
            this.analyzerHandler.Listen(iPVersion, protocol);
        }
        public void Stop()
        {
            this.analyzerHandler.Stop();
        }
    }
}