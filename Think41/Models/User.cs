namespace Think41.Models
{
    public class User
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public byte Age { get; set; }
        public string Gender { get; set; }
        public string State { get; set; }
        public string Street_Address { get; set; }
        public string Postal_Code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Traffic_Source { get; set; }
        public DateTime Created_At { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
