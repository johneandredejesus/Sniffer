namespace Sniffer.Engine.Types.Protocols
{
    public abstract class IPProtocol : Protocol
    {
        public IPProtocol(byte[] data, ref byte[] dataReturn) : base(data)
        { }
    }
}