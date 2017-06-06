using System;
using System.Collections.Generic;


namespace PurchasePool.Common.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name {get;set;}
        public DateTime DateCreate { get; set; }
        public string Description { get; set; }
        public string WebLink { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
