using System.Collections.Generic;

namespace PurchasePool.Data.EF.Entities
{
    public class Category: Entity
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        ICollection<CategoryGoodReference> GoodRefrences { get; set; }
    }
}
