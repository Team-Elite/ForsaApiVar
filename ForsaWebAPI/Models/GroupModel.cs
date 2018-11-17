using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class GroupModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupName2 { get; set; }
        public bool? RateVisible { get; set; }
    }
}