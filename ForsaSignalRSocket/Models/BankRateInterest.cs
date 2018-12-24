using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaSignalRSocket
{
    public class UserMode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsPublished { get; set; }
        public int TimePeriodId { get; set; }
        public Nullable<decimal> MinValue { get; set; }
        public Nullable<decimal> MaxValue { get; set; }
        public Nullable<decimal> BaseCurve { get; set; }
        public Nullable<decimal> FractionRate { get; set; }
        public Nullable<decimal> RateOfInterest { get; set; }
        public string GroupIds { get; set; }
        public Nullable<decimal> FractionRate2 { get; set; }
        public Nullable<decimal> RateOfInterest2 { get; set; }
        public Nullable<decimal> FractionRate3 { get; set; }
        public Nullable<decimal> RateOfInterest3 { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    }
}