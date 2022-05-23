using System.ComponentModel.DataAnnotations;

namespace ShopifyBackend.Models
{
    public class Inventory
    {
        public Inventory()
        {
            LocationName = "Undefined";
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }    

        [Required(ErrorMessage ="Please enter Quantity")]
        public int Quantity { get; set; }

        
        public Relationship? Relationship { get; set; }

        [Display(Name = "Location")]
        public String? LocationName { get; set; }
    }
}
