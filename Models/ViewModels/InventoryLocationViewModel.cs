namespace ShopifyBackend.Models.ViewModels
{
    public class InventoryLocationViewModel
    {
        public Inventory Inventory { get; set; }

        public Location Location { get; set; }  

        public  List<Location> Locations { get; set; }  

    }
}
