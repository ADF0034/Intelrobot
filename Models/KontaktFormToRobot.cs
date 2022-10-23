namespace IntelRobotics.Models
{
    public class KontaktFormToRobot
    {
        public int KontaktFormId { get; set; }
        public KontaktForm? kontaktForm { get; set; }
        public Guid RobotId { get; set; }
        public Robot? robot { get; set; }
    }
}
