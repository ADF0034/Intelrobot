namespace IntelRobotics.Models
{
    public class Users
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public IList<string>? role { get; set; }
    }
}
