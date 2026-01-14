using System;

namespace EntityLayer.Concrete
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }

    }
}
