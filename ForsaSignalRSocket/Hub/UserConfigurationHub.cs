using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ForsaSignalRSocket
{
    public class UserConfigurationHub: Hub
    {
        private readonly UserConfigurationTicker _bankRateTicker;

        public UserConfigurationHub() :
            this(UserConfigurationTicker.Instance)
        {

        }

        public UserConfigurationHub(UserConfigurationTicker bankRateTicker)
        {
            _bankRateTicker = bankRateTicker;
        }
        public void SendConfiguration(List<UserMode> bankRateInterests)
        {
            Clients.All.broadcastbankrate(bankRateInterests);
        }

        public List<UserModel> GetBankRate()
        {
            return _bankRateTicker.GetConfiguration();
        }
        public override Task OnConnected()
        {
            return (base.OnConnected());
        }
    }
}