namespace EntityLayer.Concrete
{
    public class UserMetrics
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int MonthlyDataGb { get; set; }
        public int MonthlySpendTry { get; set; }
        public int LoyaltyYears { get; set; }
    }
}
