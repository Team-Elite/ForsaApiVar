using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class RateOfInterestOfBankModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Boolean IsPublished { get; set; }
        public int TimePeriodId { get; set; }
        public int bankId { get; set; }
        public int IsSelected { get; set; }
        public decimal RateOfInterest { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int ModifiedBy { get; set; }
        public string GroupIds { get; set; }
        public string TimePeriod { get; set; }
        public bool IsDoubleTapped { get; set; }
        public decimal RateOfInterest2 { get;set; }
        public decimal RateOfInterest3 { get; set; }
    }
}