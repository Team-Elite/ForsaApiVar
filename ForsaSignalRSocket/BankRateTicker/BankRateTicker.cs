using ForsaSignalRSocket.Data;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.EventArgs;



namespace ForsaSignalRSocket
{
    public class BankRateTicker
    {
        private forsawebEntities db = new forsawebEntities();
        private static Lazy<BankRateTicker> _instance = new Lazy<BankRateTicker>(() => new BankRateTicker(GlobalHost.ConnectionManager.GetHubContext<ForsaHub>().Clients) );
        //ivate Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients;
        private readonly SqlTableDependency<UserMode> _dependency;

        public static BankRateTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public BankRateTicker(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients)
        {
            this.Clients = clients;
            var mapper = new ModelToTableMapper<UserMode>().AddMapping(m => m.Id, "Id");
            _dependency = new SqlTableDependency<UserMode>(connectionString: db.Database.Connection.ConnectionString, tableName:  "tblRateOfInterestOfBank", mapper: mapper);
            _dependency.OnChanged += _dependency_OnChanged;
            _dependency.OnError += _dependency_OnError;
            _dependency.Start();
        }

        private void _dependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }

        private void _dependency_OnChanged(object sender, RecordChangedEventArgs<UserMode> e)
        {
            Clients.All.sendBankRate(e.Entity);
        }

        public  List<UserMode> GetBankRate()
        {
            using (db = new forsawebEntities())
            {
                var Ratelist =  db.tblRateOfInterestOfBanks.ToList();

                return Ratelist.Select(f => new UserMode()
                {
                    Id = f.Id,
                    TimePeriodId = f.TimePeriodId,
                    GroupIds = f.GroupIds,
                    RateOfInterest = f.RateOfInterest.Value,
                    IsPublished = f.IsPublished,
                    UserId = f.UserId
                }).ToList();


            }
        }



    }
}