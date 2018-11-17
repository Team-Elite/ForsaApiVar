using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class BorrowerModel
    {
        public Nullable<long> SrNo { get; set; }
        public int UserId { get; set; }
        public string Bank { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public Nullable<int> Count { get; set; }
    }
}