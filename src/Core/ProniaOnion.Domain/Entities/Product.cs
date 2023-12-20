namespace ProniaOnion.Domain.Entities
{
    public class Product:BaseNameEntity
    {
        public decimal Price { get; set; }
        public string SKU { get; set; } = null!;
        public string? Description { get; set; }
        
        //---Relational
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
