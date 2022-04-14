using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Client.Connection
{
    public class Connection
    {

        private INetworkModelGDAContract proxy;

        public INetworkModelGDAContract Proxy
        {
            get
            {
                return proxy;
            }

           private set
            {
                proxy = value;
            }
        }

        public bool Connect()
        {
            try
            {
                var binding = new NetTcpBinding();
                ChannelFactory<INetworkModelGDAContract> factory  = new ChannelFactory<INetworkModelGDAContract>("*");
                proxy = factory.CreateChannel();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
