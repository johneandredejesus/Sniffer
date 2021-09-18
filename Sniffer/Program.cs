using System;
using Sniffer.Engine;
using Sniffer.Engine.Types;
using Sniffer.Engine.Types.Args;
using Sniffer.Engine.Types.Protocols;

namespace Sniffer
{
    class Program
    {
        static void Main(string[] args)
        { 

            IAnalyzerHandler analyzer = new Analyzer("0.0.0.0",0);

            analyzer.ListenEvent.IPV4Event += IPV4Event;
           
            analyzer.ListenAsync(IPVersion.IPV4, ProtocolOption.ICMP);
            Console.ReadLine();
        
        }
        public static void IPV4Event (object sender, IPV4EventArgs args)
        {   
            IPV4 protocol = args.Protocol;
            Console.WriteLine($"Protocol: {protocol.Protocol}");
            Console.WriteLine($"Source: {protocol.SourceAddress}");
            Console.WriteLine($"Destination: {protocol.DestinationAddress}");
            
        }
    }
}
