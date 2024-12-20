namespace insight.Models
{

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
       public string Password { get; set; }
    }


    public class UserVm
    {

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
