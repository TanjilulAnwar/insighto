using System.Dynamic;

namespace insight.Models
{
    public class GlobalResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic data { get; set; } = new ExpandoObject();
        public GlobalResponse(string message, bool success= true, dynamic data=null)
        {
            this.Success = success;
            this.Message = message;
            this.data = data;
        }
    }
}
