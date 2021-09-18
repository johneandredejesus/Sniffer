namespace Sniffer.Engine
{
    public interface IAnalyzerHandler : IListen
    {
       IListenEvent ListenEvent {get;}
    }
}