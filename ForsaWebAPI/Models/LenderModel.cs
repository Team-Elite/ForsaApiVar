using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class LenderModel
    {
        public int TimePeriodId { get; set; }
        public string TimePeriod { get; set; }
        public string RateOfInterest { get; set; }
    }
}