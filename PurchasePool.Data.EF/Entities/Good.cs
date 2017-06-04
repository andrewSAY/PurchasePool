using System.Collections.Generic;

namespace PurchasePool.Data.EF.Entities
{
    public class Good: Entity
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebLink { get; set; }
        ICollection<CategoryGoodReference> CategoriesRefernces { get; set; }
    }
}
