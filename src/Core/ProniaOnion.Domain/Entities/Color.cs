namespace ProniaOnion.Domain.Entities
{
    public class Color:BaseNameEntity
    {
        //---Relational
        public ICollection<ProductColor> ProductColors { get; set; }
    }
}
