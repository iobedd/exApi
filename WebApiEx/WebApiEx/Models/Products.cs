using System.ComponentModel.DataAnnotations;

namespace WebApiEx.Models
{
    public class Products
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public  int[] Ratings { get; set; }
        
        public DateTime CreatedOn { get; set; }

    }
}
