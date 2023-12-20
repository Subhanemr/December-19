namespace ProniaOnion.Domain.Entities
{
    public class ProductColor:BaseEntity
    {        
        //---Relational
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int ColorId { get; set; }
        public Color Color { get; set; } = null!;
    }
}
