using System.ComponentModel.DataAnnotations;

namespace ShopifyBackend.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }

        [Display(Name = "Subscriptions")]
        public List<Relationship>? Relationships { get; set; }

    }
}
