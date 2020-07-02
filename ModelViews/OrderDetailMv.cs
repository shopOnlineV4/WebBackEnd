using System;

namespace ModelViews
{
    public class OrderDetailMv
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public Guid OrderId { get; set; }
        public int Quality { get; set; }

        public OrderMv Oder { get; set; }
        public TypeProductMv TypeProduct { get; set; }

    }
}