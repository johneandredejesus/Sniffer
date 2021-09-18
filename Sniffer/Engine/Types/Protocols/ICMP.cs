using System;
namespace Sniffer.Engine.Types.Protocols
{
    public sealed class ICMP : IProtocol
    {
        public int Type { get;  private set;}
        public int Code { get;  private set;}
        public int Checksum { get;  private set;} 
        public ICMP(byte[] data)
        {
             
        }
    }
}