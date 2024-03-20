using System.ComponentModel.DataAnnotations;

namespace WebApiEx.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int MonthlyIncome { get; set; }
        public string OwnerName { get; set; }
        public DateTime ActiveSince { get; set; }
        
        //public Store(Guid id, string name, string country, string city, int monthlyIncome, string ownerName, DateTime activeSince) 
        //{
        //    Id = id;
        //    Name = name;
        //    Country = country;
        //    City = city;
        //    MonthlyIncome = monthlyIncome;
        //    OwnerName = ownerName;
        //    ActiveSince = activeSince;
        //}


        
    }
}
