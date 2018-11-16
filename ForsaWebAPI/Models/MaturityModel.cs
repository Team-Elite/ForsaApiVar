using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class MaturityModel
    {
        public int RequestId { get; set; }
        public int LenderId { get; set; }
        public string Lender { get; set; }
        public int BorrowerId { get; set; }
        public string Borrower { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int NoOfDays { get; set; }
        public string InterestConvention { get; set; }
        public string Payments { get; set; }
        public bool IsRequestAccepted { get; set; }
        public Nullable<int> RequestCreatedBy { get; set; }
        public Nullable<decimal> RateOfInterestOfferred { get; set; }
        public Nullable<System.DateTime> OfferredOn { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsRejected { get; set; }
        public string MessageForForsa { get; set; }
        public Nullable<bool> IsMessageSentToForsa { get; set; }
    }
}