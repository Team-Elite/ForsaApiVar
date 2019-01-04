using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class DocFileModel
    {
        public int userId { get; set; }
        public int docId { get; set; }
        public string docName { get; set; }
        public int calledFrom { get; set; }
        public string docPathWithName { get; set; }
        public string type { get; set; }

        public enum DocUploadCalledFrom { BankUserProfile, LenderUserProfile }
    }
}