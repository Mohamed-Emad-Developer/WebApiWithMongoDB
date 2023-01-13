namespace Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public string CategoryId { get; set; }
        public Category? Category { get; set; }

    }

    public class ProductDto
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }


    }
}
