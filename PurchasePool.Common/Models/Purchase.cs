using System;

namespace PurchasePool.Common.Models
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public string PurchaseName {get;set;}
        public DateTime DateCreate { get; set; }
        public string Details { get; set; }
        public decimal PurchaseTotalSum { get; set; }
    }
}
