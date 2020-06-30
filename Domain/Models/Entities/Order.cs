using Domain.Models.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public PayWays PayWays { get; set; }
        public double Amount { get; set; }
        public int StatusOrder { get; set; }

        public AppUser AppUser { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}