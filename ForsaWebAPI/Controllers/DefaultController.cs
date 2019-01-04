using ForsaWebAPI.persistance.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ForsaWebAPI.Controllers
{
    public class DefaultController : Controller
    {
        private ForsaEntities db = new ForsaEntities();

        // GET: Default
        public ActionResult Index()
        {
          return  View( BankRateTicker.Instance.GetBankRate());
          
        }
    }
}
