namespace Training.Entities
{
    public class ProductBrand
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // To allow Soft Delete, we can add a property to indicate whether the product is deleted or not.
        public bool IsDeleted { get; set; } = false;
    }
}
