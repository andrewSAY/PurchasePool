using System;

namespace PurchasePool.Data.EF.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
