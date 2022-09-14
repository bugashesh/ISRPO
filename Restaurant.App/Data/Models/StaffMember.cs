namespace Restaurant.App.Data.Models
{
    public class StaffMember
    {
        public int? Id { get; set; }
        public int? PositionId { get; set; }
        public string FullName { get; set; }
        public int? Passport { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Flat { get; set; }
        public int? Age { get; set; }
    }
}
