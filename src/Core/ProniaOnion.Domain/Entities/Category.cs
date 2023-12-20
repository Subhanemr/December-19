

namespace ProniaOnion.Domain.Entities
{
    public class Category : BaseNameEntity
    {
        //---Relational
        public ICollection<Product>? Products { get; set; }
    }
}
