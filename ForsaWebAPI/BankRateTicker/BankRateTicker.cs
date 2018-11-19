using ForsaWebAPI.Models;
using ForsaWebAPI.persistance.data;
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



namespace ForsaWebAPI
{
    public class BankRateTicker
    {
        private ForsaEntities db = new ForsaEntities();
        private static Lazy<BankRateTicker> _instance = new Lazy<BankRateTicker>(() => new BankRateTicker(GlobalHost.ConnectionManager.GetHubContext<ForsaHub>().Clients) );
        //ivate Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients;
        private readonly SqlTableDependency<RateOfInterestOfBankModel> _dependency;

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
            var mapper = new ModelToTableMapper<RateOfInterestOfBankModel>().AddMapping(m => m.Id, "Id");
            _dependency = new SqlTableDependency<RateOfInterestOfBankModel>(connectionString: db.Database.Connection.ConnectionString, tableName:  "tblRateOfInterestOfBank", mapper: mapper);
            _dependency.OnChanged += _dependency_OnChanged;
            _dependency.OnError += _dependency_OnError;
            _dependency.Start();
        }

        private void _dependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }

        private void _dependency_OnChanged(object sender, RecordChangedEventArgs<RateOfInterestOfBankModel> e)
        {
            Clients.All.sendBankRate(e.Entity);
        }

        public  List<RateOfInterestOfBankModel> GetBankRate()
        {
            using (db = new ForsaEntities())
            {
                var Ratelist =  db.tblRateOfInterestOfBanks.ToList();

                return Ratelist.Select(f => new RateOfInterestOfBankModel()
                {
                    Id = f.Id,
                    TimePeriodId = f.TimePeriodId,
                    GroupIds = f.GroupIds,
                    RateOfInterest = f.RateOfInterest,
                    IsPublished = f.IsPublished,
                    UserId = f.UserId
                }).ToList();


            }
        }



    }
}