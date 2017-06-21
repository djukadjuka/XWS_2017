using FirmaService;
using System.ServiceModel;
using System.ServiceModel.Channels;


namespace ConsoleClient
{
    public class FirmaClient : ClientBase<IFirmaService>, IFirmaService
    {
        public FirmaClient(Binding binding, EndpointAddress address) : base (binding, address)
		{
        }

        public string GetData(int value)
        {
           return Channel.GetData(value);
        }

        public FakturaResponse SlanjeFakture(Faktura faktura)
        {
            return Channel.SlanjeFakture(faktura);
        }
    }
}
