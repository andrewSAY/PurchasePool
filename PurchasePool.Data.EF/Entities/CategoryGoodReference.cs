
namespace PurchasePool.Data.EF.Entities
{
    public class CategoryGoodReference: Entity
    {        
        public virtual Category Category { get; set; }
        public virtual Good Good { get; set; }
    }
}
