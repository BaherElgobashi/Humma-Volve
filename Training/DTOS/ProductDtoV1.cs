namespace Training.DTOS
{
    public class ProductDtoV1
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string BrandName { get; set; } = null!;
    }
}
