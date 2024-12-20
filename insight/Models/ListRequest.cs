namespace insight.Models
{

    public class ListRequest
    {
        public bool All { get; set; } = false;
        public bool Today { get; set; } = false;
        public bool ThisWeek { get; set; } = false;
        public bool ThisMonth { get; set; } = false;
    }
}
