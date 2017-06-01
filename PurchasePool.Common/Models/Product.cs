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
        public decimal WebLink { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
