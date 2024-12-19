namespace insight.Models
{
   
    public class Note
    {
        public string Id { get; set; }
        public string UserId { get; set; }  = string.Empty;
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime? Reminder { get;set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;

    }
}
