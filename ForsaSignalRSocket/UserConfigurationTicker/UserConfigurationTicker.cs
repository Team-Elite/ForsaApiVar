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
    public class UserConfigurationTicker
    {
        private forsawebEntities db = new forsawebEntities();
        private static Lazy<UserConfigurationTicker> _instance = new Lazy<UserConfigurationTicker>(() =>
        new UserConfigurationTicker(GlobalHost.ConnectionManager.GetHubContext<UserConfigurationHub>().Clients));
        //ivate Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients;
        private readonly SqlTableDependency<UserModel> _dependency;

        public static UserConfigurationTicker Instance
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

        public UserConfigurationTicker(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients)
        {
            this.Clients = clients;
            var mapper = new ModelToTableMapper<UserModel>().AddMapping(m => m.UserId, "UserId");
            _dependency = new SqlTableDependency<UserModel>(connectionString: db.Database.Connection.ConnectionString, tableName: "tblUser", mapper: mapper);
            _dependency.OnChanged += _dependency_OnChanged;
            _dependency.OnError += _dependency_OnError;
            _dependency.Start();
        }

        private void _dependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }

        private void _dependency_OnChanged(object sender, RecordChangedEventArgs<UserModel> e)
        {
            Clients.All.SendConfiguration(e.Entity);
        }

        public List<UserModel> GetConfiguration()
        {
            using (db = new forsawebEntities())
            {
                var Ratelist = db.tblUsers.ToList();

                return Ratelist.Select(f => new UserModel()
                {
                    UserId = f.UserId,
                    
                }).ToList();


            }
        }
    }
}