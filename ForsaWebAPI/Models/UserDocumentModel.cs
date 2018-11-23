namespace ForsaWebAPI.Controllers.Models
{
    public class DocumentModel
    {
        public int userId { get; set; }
        public int docId { get; set; }
        public string docName { get; set; }
        public int calledFrom { get; set; }
        public string docPathWithName { get; set; }
        public string type { get; set; }
    }
}