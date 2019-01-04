using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class PagesModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}