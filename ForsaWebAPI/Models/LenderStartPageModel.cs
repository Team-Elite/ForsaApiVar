using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class LenderStartPageModel
    {
        public int LenderStartPageId { get; set; }
        public int PageId { get; set; }
        public int UserId { get; set; }
        public bool IsStartPage { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string LenderStartPage { get; set; }
    }
}