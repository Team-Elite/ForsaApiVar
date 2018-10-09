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
        public string LenderName { get; set; }
        public string BorrowerName { get; set; }
        public string InterestConventionName { get; set; }
        public string PaymentsName { get; set; }
        public string LenderEmailId { get; set; }
        public string BorrowerEmailId { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsRejected { get; set; }
        public decimal? RateOfInterest { get; set; }
        public string MessageForForsa { get; set; }
        public bool IsMessageSentToForsa { get; set; }

    }
}