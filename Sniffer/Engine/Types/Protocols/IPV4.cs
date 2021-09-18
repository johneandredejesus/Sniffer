using System;
using System.Net;

namespace Sniffer.Engine.Types.Protocols
{

    public sealed class IPV4 : IPProtocol
    {
        public int Version { get; private set; }
        public int IHL { get; private set; }
        public int TypeOfService { get; private set; }
        public int TotalLength { get; private set; }
        public int Identification { get; private set; }
        public int Flags { get; private set; }
        public int FragmentOffset { get; private set; }
        public int TimeToLive { get; private set; }
        public int Protocol { get; private set; }
        public int HeaderChecksum { get; private set; }
        public IPAddress SourceAddress { get; private set; }
        public IPAddress DestinationAddress { get; private set; }
        public int Options { get; private set; }
        public int Padding { get; private set; }
        /// <summary> 
        ///  rfc 760 
        /// </summary>
        public IPV4(byte[] data, ref byte[] dataReturn) : base(data, ref dataReturn)
        {
            int place = 0;
            this.Version = data[place] >> 4 & 0b00001111;
            this.IHL = data[place] & 0b00001111;
            place += sizeof(byte);
            this.TypeOfService = data[place];
            place += sizeof(byte);
            this.TotalLength = BitConverter.ToInt16(data, place);
            place += sizeof(Int16);
            this.Identification = BitConverter.ToInt16(data, place);
            place += sizeof(Int16);
            this.Flags = data[place] >> 13 & 0b0111;
            int fragment = BitConverter.ToInt16(data, place);
            this.FragmentOffset = fragment & 0b0001111111111111;
            place += sizeof(Int16);
            this.TimeToLive = data[place] >> 4 & 0b00001111;
            place += sizeof(byte);
            this.Protocol = data[place] & 0b00001111;
            place += sizeof(byte);
            this.HeaderChecksum = BitConverter.ToInt16(data, place);
            place += sizeof(Int16);
            byte[] source = new byte[sizeof(Int32)];
            Buffer.BlockCopy(data, place, source, 0, source.Length);
            this.SourceAddress = new IPAddress(source);
            place += sizeof(Int32);
            byte[] destination = new byte[sizeof(Int32)];
            Buffer.BlockCopy(data, place, destination, 0, destination.Length);
            this.DestinationAddress = new IPAddress(destination);
            place += sizeof(Int32);
            int optionPadding = BitConverter.ToInt32(data, place);
            this.Options = optionPadding >> 8;
            this.Padding = optionPadding & 0b11111111;
            place += sizeof(Int32);
        }
    }
}