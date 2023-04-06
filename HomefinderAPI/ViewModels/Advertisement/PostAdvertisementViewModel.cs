namespace HomefinderAPI.ViewModels.Advertisement
{
    public class PostAdvertisementViewModel
    {
        public int ListPrice { get; set; }
        public int LeaseTypeId { get; set; }
        public int PropertyTypeId { get; set; }
        public int Area { get; set; }
        public string? City { get; set; }
        public int PostalCode { get; set; }
        public string? StreetAddress { get; set; }
    }
}