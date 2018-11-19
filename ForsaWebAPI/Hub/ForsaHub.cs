using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ForsaWebAPI.Models;
using Microsoft.AspNet.SignalR;

namespace ForsaWebAPI
{
    public class ForsaHub : Hub
    {
        private readonly BankRateTicker _bankRateTicker;

        public ForsaHub() :
            this(BankRateTicker.Instance)
        {

        }

        public ForsaHub(BankRateTicker bankRateTicker )
        {
            _bankRateTicker = bankRateTicker;
        }
        public void SendBankRate(List<RateOfInterestOfBankModel> bankRateInterests)
        {
            Clients.All.broadcastbankrate(bankRateInterests);
        }

        public List<RateOfInterestOfBankModel> GetBankRate()
        {
            return _bankRateTicker.GetBankRate();
        }
        public override Task OnConnected()
        {
            return (base.OnConnected());
        }
    }
}