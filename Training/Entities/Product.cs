namespace Training.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }


        // To allow Soft Delete, we can add a property to indicate whether the product is deleted or not.
        public bool IsDeleted { get; set; } = false;
    }
}
