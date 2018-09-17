using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class LenderSendRequestModel
    {
        public int RequestId { get; set; }
        public int LenderId { get; set; }
        public int BorrowerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfDays { get; set; }
        public string InterestConvention { get; set; }
        public string Payments { get; set; }
        public bool IsRequestAccepted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int RequestCreatedBy { get; set; }
    }
}